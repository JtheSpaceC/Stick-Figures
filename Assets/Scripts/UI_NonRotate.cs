using UnityEngine;
using System.Collections;

public class UI_NonRotate : MonoBehaviour {
	
	public bool keepRotation = true;
	public bool keepPosition = true;

	private Quaternion rotation;
	private Vector3 position;

	void Awake()
	{
		rotation = transform.rotation;
		position = transform.localPosition;
	}
	
	void LateUpdate () 
	{
		if(keepRotation)
		transform.rotation = rotation;	

		if (keepPosition)
		transform.localPosition = position;
	}
}
