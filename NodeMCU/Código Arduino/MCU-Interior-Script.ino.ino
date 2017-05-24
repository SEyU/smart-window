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
#include <SimpleDHT.h>
#include <LiquidCrystal.h>

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

//Configuramos los datos para el sensor de temperatura
int pinDHT11 = 10;
SimpleDHT11 dht11;

byte temperature = 0;
byte humidity = 0;

//Variables que leemos del callback de MQTT
String mqttTemperatura, mqttVelViento;

bool isInterior=true;

LiquidCrystal lcd(5, 4, 0, 2, 14, 12);

void setup() {  
  // set up the LCD's number of columns and rows: 
  lcd.begin(16, 2); 
  
  Serial.begin(115200);

  client.setServer(mqtt_server, mqtt_port);
  client.setCallback(callback);

  // Conectando a la red wifi
  lcd.clear();
  lcd.setCursor(0,0); 
  lcd.print("Conect to ");
  lcd.print(ssid);
  WiFi.begin(ssid, password);
  lcd.setCursor(0,1);
  int cont=0;
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    lcd.print(".");
    if(cont==15){
      lcd.setCursor(0,1);
      lcd.print("                            ");
      lcd.setCursor(0,1);
    }
    cont++;
  }
  lcd.clear();
  lcd.setCursor(0,0);
  lcd.println("WiFi conectado.");

  lcd.setCursor(0,1);
  // Imprimimos la dirección IP
  lcd.println(WiFi.localIP());
  delay(5000);
}

//La funcion loop se ejecuta una y otra vez para siempre
void loop() {

  //Comprobamos la conexión con el servidor MQTT
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  
  // Leemos de DHT11 cada 6 segundos
  long now = millis();
  if (now - lastMsg > 10000) {
    lastMsg = now;
    if (dht11.read(pinDHT11, &temperature, &humidity, NULL)) {
      Serial.print("Read DHT11 failed.");
      return;
    }
    senMsgMqtt("SmartWindow/MCUINT/Clima/Temperatura",String(temperature),50);
    senMsgMqtt("SmartWindow/MCUINT/Clima/Humedad",String(humidity),50);   
      
    if(isInterior){
      lcd.clear();
      lcd.setCursor(0,0); // set the cursor to column 0, line 0
      lcd.print("Temp Int: ");
      lcd.print((int)temperature);
      lcd.print((char)223);
      lcd.print ("C");    
          
      lcd.setCursor(0,1); // set the cursor to column 0, line 1
      lcd.print("Humedad Int: ");
      lcd.print((int)humidity);
      lcd.print("%");
      isInterior=false;
    }else{
      lcd.clear();
      lcd.setCursor(0,0); // set the cursor to column 0, line 0
      lcd.print("Temp Ext: ");
      lcd.print(mqttTemperatura);
      lcd.print((char)223);
      lcd.print ("C");    
          
      lcd.setCursor(0,1); // set the cursor to column 0, line 1
      lcd.print("Viento: ");
      lcd.print(mqttVelViento);
      lcd.print(" Km/h");      
      isInterior=true;
    }
  }
    delay(1000);
}

void senMsgMqtt(String topicIn,String msgIn,int msgLength){
  char message[msgLength];
  char topic[100];
  topicIn.toCharArray(topic, 100);
  msgIn.toCharArray(message, msgLength);
  //Serial.println(topicIn);
  //Serial.println(msgIn);
  //Publicar el mensaje en el broker MQTT
  client.publish(topic, message);
}

void reconnect() {
  while (!client.connected())
  {
    //Serial.println("Intentando conectar con MQTT ...");
    // Creamos un ID cliente aleatorio
    String clientId = "ESP8266Client-";
    clientId += String(random(0xffff), HEX);
    // Intentando conectar
    //if you MQTT broker has clientID,username and password
    //please change following line to    if (client.connect(clientId,userName,passWord))
    if (client.connect(clientId.c_str()))
    {
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print("Connected MQTT");
      //Una vez conectados, nos suscribimos al NodeMCU externo
      client.subscribe("SmartWindow/MCUEXT/Clima/Temperatura"); 
      client.subscribe("SmartWindow/MCUEXT/Clima/VelViento");
       delay(5000);
    } else {
     // Serial.print("ERROR, rc=");
     // Serial.print(client.state());
      //Serial.println("Inténtalo de nuevo en 5 segundos");
      lcd.clear();
      lcd.setCursor(0,0);
      lcd.print("ERROR connect MQTT");
       lcd.setCursor(0,1);
      lcd.print("Intento en 5 seg.");
      //Esperamos 6 segundos antes de volver a intertar
      delay(6000);
    }
  }
} //end reconnect()

void callback(char* topic, byte* payload, unsigned int length)
{
  Serial.println("Comando recibido por mqtt");
  int p = (char)payload[0] - '0';
  
  String strData;
  int i;
    for (i = 0; i < length; i = i + 1) {
    strData += String((char)payload[i] - '0');
  }

  if(String(topic)=="SmartWindow/MCUEXT/Clima/Temperatura"){
    //Serial.println("SmartWindow/MCUEXT/Clima/Temperatura" + payload);
    mqttTemperatura=strData;
  }else if(String(topic)=="SmartWindow/MCUEXT/Clima/VelViento"){
    //Serial.println("SmartWindow/MCUEXT/Ventana/Movimiento" + p);
    mqttVelViento=strData;
  }else{
    //Serial.println("Topic no reconocido:");
    //Serial.println(topic);
  }
  
} //end callback


