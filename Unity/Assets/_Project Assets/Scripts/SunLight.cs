using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour {
	[SerializeField] Transform stars;
	[SerializeField] float outerLight;
	[SerializeField] Gradient nightDayColor;

	[SerializeField] float maxIntensity = 3f;
	[SerializeField] float minIntensity = 0f;
	[SerializeField] float minPoint = -0.2f;

	[SerializeField] float maxAmbient = 1f;
	[SerializeField] float minAmbient = 0f;
	[SerializeField] float minAmbientPoint = -0.2f;


	[SerializeField] Gradient nightDayFogColor;
	[SerializeField] AnimationCurve fogDensityCurve;
	[SerializeField] float fogScale = 1f;

	[SerializeField] float dayAtmosphereThickness = 0.4f;
	[SerializeField] float nightAtmosphereThickness = 0.87f;

	[SerializeField] Vector3 dayRotateSpeed;
	[SerializeField] Vector3 nightRotateSpeed;

	float skySpeed = 1;


	Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () 
	{
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () 
	{
		stars.transform.rotation = transform.rotation;

		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) *  fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat("_AtmosphereThickness", i);

		if(dot > 0)
			transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
	}

	void OnEnable()
	{
		EventManager.onUpdateOuterLight += SetOuterLight;
	}

	void OnDisable()
	{
		EventManager.onUpdateOuterLight -= SetOuterLight;
	}

	void SetOuterLight(float val)
	{
		outerLight = val;
	}

}
