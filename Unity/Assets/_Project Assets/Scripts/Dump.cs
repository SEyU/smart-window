using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;

public class Dump : MonoBehaviour {
	[SerializeField] float percentage = 30f;
	//[SerializeField] Slider slider;
	[SerializeField] Text text;
	[SerializeField] float scaleTest = 1f;
	[SerializeField] Transform shade;
	
	Vector3 originalScale;
	private MqttClient4Unity client;
	public string brokerHostname = "192.168.0.102";
	public int brokerPort = 1883;
	public string topic = null;
	public string topic2 = null;
	private Queue msgq = new Queue();
	string lastMessage = null;
	int temp = 0;

	Vector3 openPos;
	Vector3 closePos;
	Vector3 totalLength;
	Vector3 curPos;
	float curPercent = 100;
	float velocity = 1f;
	float speed = 1f;



	float startTime;
	float animationTime = 2f;

	void Start () 
	{
		// if (brokerHostname != null) {
		// 	Connect ();
		// 	topic = "SmartWindow/MCUINT/#";
		// 	topic2 = "SmartWindow/MCUEXT/#";
		// 	client.Subscribe(topic);
		// 	client.Subscribe(topic2);
		// }

		openPos = new Vector3(11.36f, 2.81f, 5.77f);
		closePos = new Vector3(7.985f, 3.714f, 5.77f);
		originalScale = shade.localScale;
		scaleTest = originalScale.x;
		scaleTest = 1f;
		curPos = shade.position;
		totalLength = closePos - openPos;
		curPercent = percentage;
		startTime = Time.time;
		
		//orgPos = shade.position;
	}

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
	
	// Update is called once per frame
	void Update () 
	{
		//shade.localScale = new Vector3(originalScale.x, scaleTest, originalScale.z);
		//text.text = string.Format("{0}ºC", scaleTest);
		
		// while (client.Count() > 0) {
		// 	//string s = client.Receive();
		// 	MqttMsgPublishEventArgs args = client.ReceiveEvent();
			
		// 	string strData = string.Empty;
		// 	for(int i = 0; i < args.Message.Length; i++)
		// 		strData += ((char)args.Message[i] - '0').ToString();

		// 	//msgq.Enqueue(s);
		// 	Debug.Log("Client" + client.ClientId + "mensaje :" + strData);
		// 	Debug.Log("topic :" +args.Topic);
		// 	//if (BitConverter.IsLittleEndian)
    	// 	//	Array.Reverse(args.Message);
		// 	//temp = BitConverter.ToInt32(args.Message, 0);
		// 	temp = Convert.ToInt32(strData);
		// }
		// shade.localScale = new Vector3(originalScale.x, temp, originalScale.z);
		// text.text = string.Format("{0}ºC", temp);
		if(curPercent != percentage)
			Test();
	}

	void Connect()
	{
		client = new MqttClient4Unity(brokerHostname, brokerPort, false, null);
		//string clientId = Guid.NewGuid().ToString();
		client.Connect("Unity", "", "");
	}

	void Test()
	{
		//shade.localScale = new Vector3(originalScale.x, scaleTest, originalScale.z);
		//Vector3 curPos = shade.transform.position;
		//Vector3 toPos = new Vector3(orgPos.x + scaleTest, shade.transform.position.y , shade.transform.position.z);
		//float angle = Mathf.MoveTowards(shade.transform.position.x, shade.transform.position.x + scaleTest, Time.deltaTime);
		//shade.transform.position =  Vector3.MoveTowards(curPos, toPos, 1f * Time.deltaTime);
		//shade.transform.lolocalPosition += Vector3.left * scaleTest;
		
		//shade.transform.eulerAngles()

		//toPos = new Vector3(orgPos.x + scaleTest, shade.position.y , shade.position.z);
		//shade.position = toPos * 1f * Time.deltaTime;

		//Debug.Log("Percentage: " + scaleTest);
		
		//Vector3 target = (openPos - closePos) * (percentage/100);
		//Vector3 target = Vector3.Lerp(closePos, openPos, percentage/100);
		//while(curPercent != percentage)
		
			//Vector3 target = calculateDistance();
			//shade.position = Vector3.MoveTowards(shade.position, target, speed * Time.deltaTime);
			Debug.Log("Percentage: " + curPercent);
			if(curPercent < percentage)
				curPercent++;
			else
				curPercent--;


			//curPercent = (curPercent < percentage)? curPercent++ : curPercent--;
			shade.position = Vector3.Lerp(closePos, openPos, (curPercent)/100);
			
		
		//float t = (Time.time - startTime) / animationTime;
		//shade.position = Vector3.Lerp(closePos, openPos, Mathf.SmoothStep(curPercent, percentage/100, t));

		//curPercent = percentage;
		//Debug.Log("Position: " + shade.position);
		//shade.position += -shade.right * 1f * Time.deltaTime * scaleTest;

		text.text = string.Format("{0}ºC", scaleTest);
	}

	Vector3 calculateDistance()
	{
		return Vector3.Lerp(closePos, openPos, percentage/100);
	}
}
