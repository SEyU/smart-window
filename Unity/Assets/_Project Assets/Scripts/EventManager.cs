using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	public delegate void UpdateDataDelegate(float amt);
	public static UpdateDataDelegate onUpdateInnerTemp;
	public static UpdateDataDelegate onUpdateInnerHumidity;
	public static UpdateDataDelegate onUpdateShadeState;
	public static UpdateDataDelegate onUpdateWindowState;
	public static UpdateDataDelegate onUpdateOuterTemp;
	public static UpdateDataDelegate onUpdateOuterWind;
	public static UpdateDataDelegate onUpdateOuterLight;

	public static void UpdateInnerTemp(float temp)
	{
		Debug.Log("Updating inner temp");
		if(onUpdateInnerTemp != null)
			onUpdateInnerTemp(temp);
	}

	public static void UpdateInnerHumidity(float val)
	{
		Debug.Log("Updating inner humidity");
		if(onUpdateInnerHumidity != null)
			onUpdateInnerHumidity(val);
	}

	public static void UpdateShadeState(float val)
	{
		Debug.Log("Updating shade state");
		if(onUpdateShadeState != null)
			onUpdateShadeState(val);
	}

	public static void UpdateWindowState(float val)
	{
		Debug.Log("Updating window state");
		if(onUpdateWindowState != null)
			onUpdateWindowState(val);
	}

	public static void UpdateOuterTemp(float temp)
	{
		Debug.Log("Updating outer temp");
		if(onUpdateOuterTemp != null)
			onUpdateOuterTemp(temp);
	}

	public static void UpdateOuterWind(float val)
	{
		Debug.Log("Updating outer wind");
		if(onUpdateOuterWind != null)
			onUpdateOuterWind(val);
	}

	public static void UpdateOuterLight(float val)
	{
		Debug.Log("Updating outer light");
		if(onUpdateOuterLight != null)
			onUpdateOuterLight(val);
	}
}
