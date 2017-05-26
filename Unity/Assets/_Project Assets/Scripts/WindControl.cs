using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour {
	[SerializeField] WindZone windZone;
	
	void OnEnable()
	{
		EventManager.onUpdateOuterWind += SetWindSpeed;
	}

	void OnDisable()
	{
		EventManager.onUpdateOuterWind -= SetWindSpeed;
	}

	void SetWindSpeed(float val)
	{
		windZone.windMain = val / 10;
	}
}
