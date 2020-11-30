using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrons2 : MonoBehaviour {
	public double PlateFieldStrength;
	public double K=90000000000;
	public double e=0.00000000000000000016;
	//public double m=0.00000000000000000000000000000091;
	public double m=1e-07;
	public double acceleration;
	public double velocity;
	public double force;
	public float resistance;
	public float normalResistance;
	public float dialationInActivatingCollion;
	// Use this for initialization
	void Start () {
		velocity = 0;
		StartCoroutine (disableTrigger());
	}
	IEnumerator disableTrigger(){
		yield return new WaitForSeconds(dialationInActivatingCollion);
		gameObject.GetComponent<SphereCollider> ().isTrigger = false;

	}
	// Update is called once per frame
	double applyPlateForce(){
		return PlateFieldStrength*e;
	}
	double applyElectronForce(){
		GameObject[] electrons=GameObject.FindGameObjectsWithTag ("electron");
		GameObject electron;
		double electronForce=0;
		for (int i = 0; i < electrons.Length; i++) {
			electron = electrons [i];
			if (!electron.Equals (gameObject)) {
				double eleX = electron.transform.position.x - transform.position.x;
				if(eleX>0)
					electronForce += K * e * e / (eleX * eleX);
				else if(eleX<0)
					electronForce -= K * e * e / (eleX * eleX);
			}
		}
		return electronForce;
	}
	void Update () {
		force = 0;
		force+=applyPlateForce ();
		force+=applyElectronForce ();
		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(-(float)(force),0,0));
		//acceleration = force / m;
		//transform.Translate (-(float)(velocity*Time.deltaTime+acceleration*Time.deltaTime*Time.deltaTime/2),0f,0f,Space.Self);
		//velocity += acceleration * Time.deltaTime;
	}
	void OnTriggerEnter(Collider collider){
		if (collider.name == "positive plate") {
			print ("end: " + gameObject.GetComponent<Rigidbody> ().velocity.x);
			Destroy (gameObject);
		}
		if (collider.name == "negative plate") {
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		if (collider.tag == "resistor") {
			print ("begin " +collider.name+" :"+ gameObject.GetComponent<Rigidbody> ().velocity.x);
			gameObject.GetComponent<Rigidbody> ().drag = resistance;
		}
		
	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == "resistor")
			gameObject.GetComponent<Rigidbody> ().drag = normalResistance;
	}
}
