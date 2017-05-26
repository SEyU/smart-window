using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


/*
	Interior:
		SmartWindow/MCUINT/Clima/Temperatura
		SmartWindow/MCUINT/Clima/Humedad

	Exterior:
		SmartWindow/MCUEXT/Toldo/Estado (0-100)(0 cerrado)
		SmartWindow/MCUEXT/Ventana/Estado (0-100)(0 cerrado)
		SmartWindow/MCUEXT/Clima/Temperatura 
		SmartWindow/MCUEXT/Clima/VelViento
		SmartWindow/MCUEXT/Clima/Iluminacion (0-100)
*/

public class SimulationManager : MonoBehaviour {
	[SerializeField] string brokerHostname = "192.168.0.102";
	[SerializeField] int brokerPort = 1883;
	private MqttClient4Unity client;
	private string innerTempTopic;
	private string innerHumidityTopic;
	private string shadeStateTopic;
	private string windowStateTopic;
	private string outerTempTopic;
	private string outerWindTopic;
	private string outerLightTopic;

	void Start () 
	{
		if (!string.IsNullOrEmpty(brokerHostname)) {
			Connect ();
			innerTempTopic = "SmartWindow/MCUINT/Clima/Temperatura";
			innerHumidityTopic = "SmartWindow/MCUINT/Clima/Humedad";
			shadeStateTopic = "SmartWindow/MCUEXT/Toldo/Estado";
			windowStateTopic = "SmartWindow/MCUEXT/Ventana/Estado";
			outerTempTopic = "SmartWindow/MCUEXT/Clima/Temperatura";
			outerWindTopic = "SmartWindow/MCUEXT/Clima/VelViento";
			outerLightTopic = "SmartWindow/MCUEXT/Clima/Iluminacion";

			client.Subscribe(innerTempTopic);
			client.Subscribe(innerHumidityTopic);
			client.Subscribe(shadeStateTopic);
			client.Subscribe(windowStateTopic);
			client.Subscribe(outerTempTopic);
			client.Subscribe(outerWindTopic);
			client.Subscribe(outerLightTopic);
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(client != null)
			ReadTopics();
	}

	void Connect()
	{
		client = new MqttClient4Unity(brokerHostname, brokerPort, false, null);
		client.Connect("Unity", "", "");
	}

	void ReadTopics()
	{
		while (client.Count() > 0) 
		{
			MqttMsgPublishEventArgs args = client.ReceiveEvent();
			
			string strData = string.Empty;
			for(int i = 0; i < args.Message.Length; i++)
				strData += ((char)args.Message[i] - '0').ToString();
			float val = float.Parse(strData);
			
			if(args.Topic == innerTempTopic)
				EventManager.UpdateInnerTemp(val);
			else if(args.Topic == innerHumidityTopic)
				EventManager.UpdateInnerHumidity(val);
			else if(args.Topic == shadeStateTopic)
				EventManager.UpdateShadeState(val);
			else if(args.Topic == windowStateTopic)
				EventManager.UpdateWindowState(val);
			else if(args.Topic == outerTempTopic)
				EventManager.UpdateOuterTemp(val);
			else if(args.Topic == outerLightTopic)
				EventManager.UpdateOuterLight(val);
			else if(args.Topic == outerWindTopic)
				EventManager.UpdateOuterWind(val);
			else
				Debug.Log("This topic is not registered!!!: " + args.Topic);
		}
	}
}
