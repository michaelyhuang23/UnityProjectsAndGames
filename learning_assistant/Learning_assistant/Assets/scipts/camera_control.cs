using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour {
	public float sensitivity;
	void Update () {
		float movement=Input.GetAxis("Mouse ScrollWheel")*sensitivity*Time.deltaTime;
		Camera.main.orthographicSize-=movement;
	}
}
