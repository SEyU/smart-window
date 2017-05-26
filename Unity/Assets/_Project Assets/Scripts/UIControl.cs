using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	[SerializeField] Text innerTemp;
	[SerializeField] Text innerHumidity;
	[SerializeField] Text outerTemp;
	[SerializeField] Text outerWind;
	[SerializeField] Text outerLight;
	[SerializeField] Text shadeState;
	[SerializeField] Text windowState;

	void OnEnable()
	{
		EventManager.onUpdateInnerTemp += SetInnerTemp;
		EventManager.onUpdateInnerHumidity += SetInnerHumidity;
		EventManager.onUpdateOuterTemp += SetOuterTemp;
		EventManager.onUpdateOuterWind += SetOuterWind;
		EventManager.onUpdateOuterLight += SetOuterLight;
		EventManager.onUpdateShadeState += SetShadeState;
		EventManager.onUpdateWindowState += SetWindowState;
	}

	void OnDisable()
	{
		EventManager.onUpdateInnerTemp -= SetInnerTemp;
		EventManager.onUpdateInnerHumidity -= SetInnerHumidity;
		EventManager.onUpdateOuterTemp -= SetOuterTemp;
		EventManager.onUpdateOuterWind -= SetOuterWind;
		EventManager.onUpdateOuterLight -= SetOuterLight;
		EventManager.onUpdateShadeState -= SetShadeState;
		EventManager.onUpdateWindowState -= SetWindowState;
	}


	void SetInnerTemp(float val)
	{
		innerTemp.text = string.Format("{0} ºC", val.ToString());
	}

	void SetInnerHumidity(float val)
	{
		innerHumidity.text = string.Format("{0} %", val.ToString());
	}

	void SetOuterTemp(float val)
	{
		outerTemp.text = string.Format("{0} ºC", val.ToString());
	}

	void SetOuterWind(float val)
	{
		outerWind.text = string.Format("{0} Km/h", val.ToString());
	}

	void SetOuterLight(float val)
	{
		outerLight.text = string.Format("{0} %", val.ToString());
	}

	void SetShadeState(float val)
	{
		shadeState.text = string.Format("{0} %", val.ToString());
	}

	void SetWindowState(float val)
	{
		windowState.text = string.Format("{0} %", val.ToString());
	}



}
