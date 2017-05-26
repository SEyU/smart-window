using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {
	[SerializeField] Gradient nightDayColor;
	[SerializeField] Gradient nightDayFogColor;
	[SerializeField] float dayAtmosphereThickness = 0.4f;
	[SerializeField] float nightAtmosphereThickness = 0.87f;
	[SerializeField] Transform stars;
	[SerializeField] float lightIntensity;
	[SerializeField] Light mainLight;
	[SerializeField] Light inLight;
	[SerializeField] Light outLight;

	Material skyMat;
	float curLightIntensity;
	bool isLightOn;

	void Start () 
	{
		skyMat = RenderSettings.skybox;
		isLightOn = false;
		outLight.enabled = false;
		inLight.enabled = false;
	}
	
	void Update () 
	{
		stars.transform.rotation = transform.rotation;

		if(lightIntensity != curLightIntensity)
		{
			if(curLightIntensity < lightIntensity)
				curLightIntensity++;
			else
				curLightIntensity--;

			mainLight.intensity = (curLightIntensity / 100) + .1f;
			mainLight.color = nightDayColor.Evaluate(curLightIntensity / 100);
			RenderSettings.ambientLight = mainLight.color;	

			RenderSettings.fogColor = nightDayFogColor.Evaluate(curLightIntensity / 100);
			float i = ((dayAtmosphereThickness - nightAtmosphereThickness) * (1f - (curLightIntensity / 100))) + nightAtmosphereThickness;
			skyMat.SetFloat("_AtmosphereThickness", i);		

		}

		if (curLightIntensity <= 35 && !isLightOn)
		{
			outLight.enabled = true;
			inLight.enabled = true;
			isLightOn = !isLightOn;
		}
		else if(curLightIntensity > 35 && isLightOn)
		{
			outLight.enabled = false;
			inLight.enabled = false;
			isLightOn = !isLightOn;
		}

	}

	void OnEnable()
	{
		EventManager.onUpdateOuterLight += SetLightIntensity;
	}

	void OnDisable()
	{
		EventManager.onUpdateOuterLight -= SetLightIntensity;
	}

	void SetLightIntensity(float val)
	{
		lightIntensity = val;
	}


}
