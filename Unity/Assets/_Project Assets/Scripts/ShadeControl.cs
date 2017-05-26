using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeControl : MonoBehaviour {

	[SerializeField] Transform shade;
	[SerializeField] float shadeState;
	float curShadeState;
	Vector3 openPos;
	Vector3 closePos;

	void Start () 
	{
		shadeState = 0f;
		curShadeState = 0f;
		openPos = new Vector3(11.36f, 2.81f, 5.77f);
		closePos = new Vector3(7.985f, 3.714f, 5.77f);
	}
	
	void Update () 
	{
		if(curShadeState != shadeState)
			MoveShade();
	}

	void OnEnable()
	{
		EventManager.onUpdateShadeState += SetShadeState;
	}

	void OnDisable()
	{
		EventManager.onUpdateShadeState -= SetShadeState;
	}

	void SetShadeState(float val)
	{
		shadeState = val;
	}

	void MoveShade()
	{
		if(curShadeState < shadeState)
				curShadeState++;
			else
				curShadeState--;

		shade.position = Vector3.Lerp(closePos, openPos, (curShadeState)/100);
	}
}
