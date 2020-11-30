using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_field_creator : MonoBehaviour {
	public float q=1;
	public int id=1;
	void Start(){
		GameObject[] gameObjects=GameObject.FindGameObjectsWithTag("test_eletric_charge");
		for(int i=0;i<gameObjects.Length;i++){
			gameObjects[i].SendMessage("awaken",gameObject,SendMessageOptions.RequireReceiver);
		}
	}
}
