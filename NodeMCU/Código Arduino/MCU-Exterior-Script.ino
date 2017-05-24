#include <PubSubClient.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiAP.h>
#include <ESP8266WiFiGeneric.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266WiFiScan.h>
#include <ESP8266WiFiSTA.h>
#include <ESP8266WiFiType.h>
#include <WiFiClient.h>
#include <WiFiClientSecure.h>
#include <WiFiServer.h>
#include <WiFiUdp.h>

//Configuración de la red Wifi
const char* ssid = "homeserver";
const char* password = "@raspberrypi17";

//Configuramos el broker MQTT
//const char* mqtt_server = "broker.mqtt-dashboard.com";
const char* mqtt_server ="192.168.0.167";

int mqtt_port=1883;
WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;

int pinAnalogInput = A0;    // Entrada analógica
int pinAnemometro = 0;      // Simulación anemómetro
int pinTemperatura = 2;      // Simulador sensor temperatura
int pinFotoRes = 14;      // Fotoresistencia

int pinAbrirVentana=12;
int pinCerrarVentana=13;
int pinLedVentana=15;

int pinAbrirToldo=5;
int pinCerrarToldo=4;
int pinLedToldo=16;

int pinModoManual=9;
int pinLedModoManual=10;


long  temperatura= 0;
long temperaturaOld=0;
long velViento = 0;
long velVientoOld=0;  
long iluminacion = 0;
long iluminacionOld=0; 
int estadoToldo=0; // 0=cerrado|100=abierto
int estadoVentana=0; // 0=cerrada|100=abierta
int consignaToldo=0;
int consignaVentana=0;
long contTimeToldoOld=0;
long contTimeVentanaOld=0;

int mqttTempInt=23;

bool isModoManual=false;

void setup() {

  Serial.begin(115200);
  // Declaramos los pines para cada uno de los sensores analógicos
  pinMode(pinAnalogInput, INPUT);

  pinMode(pinTemperatura, OUTPUT);
  pinMode(pinAnemometro, OUTPUT);
  pinMode(pinFotoRes, OUTPUT);
  
  pinMode(pinAbrirVentana, INPUT_PULLUP);
  pinMode(pinCerrarVentana, INPUT_PULLUP);
  pinMode(pinLedVentana, OUTPUT);
  
  pinMode(pinAbrirToldo, INPUT_PULLUP);
  pinMode(pinCerrarToldo, INPUT_PULLUP);
  pinMode(pinLedToldo, OUTPUT);
  digitalWrite(pinLedToldo, LOW);

  pinMode(pinModoManual, INPUT_PULLUP);
  pinMode(pinLedModoManual, OUTPUT);
  
  client.setServer(mqtt_server, mqtt_port);
  client.setCallback(callback);

  // Conectando a la red wifi
  Serial.println();
  Serial.print("Conectando a ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    digitalWrite(pinLedVentana, HIGH);
    delay(300);
    digitalWrite(pinLedVentana, LOW);
    delay(300);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi conectado.");

  // Imprimimos la dirección IP
  Serial.println(WiFi.localIP());
}

//La funcion loop se ejecuta una y otra vez para siempre
void loop() {

  Serial.println("Inicio");

  //Comprobamos la conexión con el servidor MQTT
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  
  // Leemos la velocidad del viento
  digitalWrite(pinAnemometro, HIGH);
  velViento = analogRead(pinAnalogInput);
  //Serial.println(velViento);
  velViento=map(velViento,880,40,0,100);
  if(velViento<0){
    velViento=0;
  }
  // Serial.println(velViento);
  digitalWrite(pinAnemometro, LOW);
  
  // Leemos la temperatura
  digitalWrite(pinTemperatura, HIGH);
  temperatura = analogRead(pinAnalogInput);
  //Serial.println(temperatura);
  temperatura=map(temperatura,900,10,-20,50);
  //Serial.println(temperatura);
  digitalWrite(pinTemperatura, LOW);
  
  //Leemos la lumnosidad
  digitalWrite(pinFotoRes, HIGH);
  iluminacion = analogRead(pinAnalogInput);
  //Serial.println(iluminacion);
  iluminacion=map(iluminacion,900,70,100,0);
   if(iluminacion<0){
    iluminacion=0;
  }
   if(iluminacion>100){
    iluminacion=100;
  }
  //Serial.println(iluminacion);
  digitalWrite(pinFotoRes, LOW);

  //Comprobamos la pulsacion del modo
  if(digitalRead(pinModoManual) == 0){
     digitalWrite(pinLedModoManual, HIGH);
     while(digitalRead(pinModoManual) == 0){
        delay(500);
        digitalWrite(pinLedModoManual, LOW);
        delay(500);
        digitalWrite(pinLedModoManual, HIGH);
     } 
     digitalWrite(pinLedModoManual, LOW);
     if(isModoManual){
      isModoManual=false;
     }else{
      isModoManual=true;
     }
  }

  if(isModoManual){
    if((digitalRead(pinAbrirToldo) == 0)&&consignaToldo<100){
      consignaToldo=consignaToldo+5;
    }else if ((digitalRead(pinCerrarToldo) == 0)&&consignaToldo>0){
      consignaToldo=consignaToldo-5;
    }
    if((digitalRead(pinAbrirVentana) == 0)&&consignaVentana<100){
      consignaVentana=consignaVentana+5;
    }else if ((digitalRead(pinCerrarVentana) == 0)&&estadoVentana>0){
      consignaVentana=consignaVentana-5;
    }
  }else{
    //Comprobaciones para la funcionalidad del toldo
    if(velViento<30){
      //Si la velocidad del viento es menor a 30 usamos la lectura de la iluminación
      consignaToldo=(iluminacion/10)*10;
    }else{
      //Cerramos el toldo por exceso de viento
      consignaToldo=0;
    }
    
    //Comprobaciones para la funcionalidad de la ventana
    if(velViento>50){
      consignaVentana=0;
    }else if((mqttTempInt-3)<temperatura){
      //Abrimos la ventana para que entre el calorcito
      consignaVentana=100;
    }else if((mqttTempInt+3)>temperatura){
      //Abrimos la ventana para que entre fresquito
      consignaVentana=100;
    }else{
      consignaVentana=25;
    }
  }
    ComprobarEstado();
    
  delay(1000);
   Serial.println("Fin");
}

//Funcion para simular el estado de los motores
void ComprobarEstado(){
  long now = millis();

  if(estadoToldo!=consignaToldo){
    if(consignaToldo>estadoToldo){
      estadoToldo=estadoToldo+5;
    }else{
      estadoToldo=estadoToldo-5;
    }
    senMsgMqtt("SmartWindow/MCUEXT/Toldo/Estado",String(estadoToldo),50);
    digitalWrite(pinLedToldo, HIGH);
  }else{
    digitalWrite(pinLedToldo, LOW);
  }

  if(estadoVentana!=consignaVentana){
    if(consignaVentana>estadoVentana){
      estadoVentana=estadoVentana+5;
    }
    else {
      estadoVentana=estadoVentana-5;
    }
    senMsgMqtt("SmartWindow/MCUEXT/Ventana/Estado",String(estadoVentana),50);
    digitalWrite(pinLedVentana, HIGH);
  }else{
    digitalWrite(pinLedVentana, LOW);
  }

  if (now - lastMsg > 5000) {
    lastMsg = now;
    if(temperatura!=temperaturaOld){
      senMsgMqtt("SmartWindow/MCUEXT/Clima/Temperatura",String(temperatura),50);
      temperaturaOld=temperatura;
    } 
  
    
    if(velViento!=velVientoOld){
      senMsgMqtt("SmartWindow/MCUEXT/Clima/VelViento",String(velViento),50);
      velVientoOld=velViento;   
    }
    
    if(iluminacion!=iluminacionOld){
      senMsgMqtt("SmartWindow/MCUEXT/Clima/Iluminacion",String(iluminacion),50);
      iluminacionOld=iluminacion;
    }
    
    senMsgMqtt("SmartWindow/MCUEXT/ModoManual",String(isModoManual),50);
    
  }
  
  if(isModoManual){
    digitalWrite(pinLedModoManual, HIGH);
  }else{
    digitalWrite(pinLedModoManual, LOW);
  }
  
}

void senMsgMqtt(String topicIn,String msgIn,int msgLength){
  char message[msgLength];
  char topic[100];
  topicIn.toCharArray(topic, 100);
  msgIn.toCharArray(message, msgLength);
  //Publicar el mensaje en el broker MQTT
  client.publish(topic, message);
}

void reconnect() {
  while (!client.connected())
  {
    Serial.println("Intentando conectar con MQTT ...");
    // Creamos un ID cliente aleatorio
    String clientId = "ESP8266Client-Ext-";
    clientId += String(random(0xffff), HEX);
    // Intentando conectar
    //if you MQTT broker has clientID,username and password
    //please change following line to    if (client.connect(clientId,userName,passWord))
    if (client.connect(clientId.c_str()))
    {
      Serial.println("connected");
      //Una vez conectados, nos suscribimos al NodeMCU externo
      digitalWrite(pinLedToldo, HIGH);
      delay(1000);
      digitalWrite(pinLedToldo, LOW);
      digitalWrite(pinLedVentana, HIGH);
      delay(1000);
      digitalWrite(pinLedModoManual, HIGH);
      digitalWrite(pinLedVentana, LOW);
      delay(1000);
      digitalWrite(pinLedModoManual, LOW);
      
      client.subscribe("SmartWindow/MCUINT/Clima/Temperatura"); 
    } else {
      Serial.print("ERROR, rc=");
      Serial.print(client.state());
      Serial.println("Inténtalo de nuevo en 5 segundos");
     //Esperamos 6 segundos antes de volver a intertar
      int cont=0;
      while(cont<6){
      digitalWrite(pinLedToldo, HIGH);
      digitalWrite(pinLedVentana, LOW);
      delay(500);
      digitalWrite(pinLedToldo, LOW);
      digitalWrite(pinLedVentana, HIGH);
      delay(500);
      cont++;
      }
      digitalWrite(pinLedToldo, LOW);
      digitalWrite(pinLedVentana, LOW);
    }
  }
} //end reconnect()

void callback(char* topic, byte* payload, unsigned int length)
{
  //Serial.println("Comando recibido por mqtt");
  int p = (char)payload[0] - '0';

  if(String(topic)=="SmartWindow/MCUINT/Toldo/Manual"){
    Serial.println("SmartWindow/MCUINT/Toldo/Manual " + p);
    
  }else if(String(topic)=="SmartWindow/MCUINT/Clima/Temperatura"){
    String strData;
    int i;
    for (i = 0; i < length; i = i + 1) {
      strData += String((char)payload[i] - '0');
    }
    Serial.println("SmartWindow/MCUINT/ModoManual "+ strData);
    mqttTempInt=strData.toInt();
  }
  else{
    Serial.println("Topic no reconocido:");
    Serial.println(topic);
  }
} //end callback
