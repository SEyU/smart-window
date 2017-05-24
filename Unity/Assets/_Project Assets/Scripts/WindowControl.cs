using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControl : MonoBehaviour {
	[SerializeField] Transform leftLeaf;
	[SerializeField] Transform rightLeaf;
	[SerializeField] float windowState;
	float curWindowState;

	void Start () 
	{
		windowState = 0f;
		curWindowState = 0f;
	}
	
	void Update () 
	{
		if(curWindowState != windowState)
			MoveWindow();
	}

	void OnEnable()
	{
		EventManager.onUpdateWindowState += SetWindowState;
	}

	void OnDisable()
	{
		EventManager.onUpdateWindowState -= SetWindowState;
	}

	void SetWindowState(float val)
	{
		windowState = val;
	}

	void MoveWindow()
	{
		// Right close at -90º - Left close at 90º (on the Y axis)

		if(curWindowState < windowState)
				curWindowState++;
			else
				curWindowState--;


		float leftRot = (-1)*(0.90f * curWindowState) + 90f;
		leftLeaf.rotation = Quaternion.Euler(0, leftRot, 0);

		float rightRot = (-1)*(-0.90f * curWindowState) - 90f;
		rightLeaf.rotation = Quaternion.Euler(0, rightRot, 0);
	}
}
