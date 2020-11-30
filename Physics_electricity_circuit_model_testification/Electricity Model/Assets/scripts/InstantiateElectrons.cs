using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateElectrons : MonoBehaviour {
	public GameObject electron;
	public float x;
	public float time;
	float timer;
	void Awake(){
		timer = time;
		for (float i = -98; i < 98; i += 3.5f) {
			Instantiate (electron, new Vector3 (i,0,0), transform.rotation);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (timer > time) {
			Instantiate (electron, new Vector3 (x, 0, 0), transform.rotation);
			timer = 0;
		}
		timer += Time.deltaTime;
	}
}
