using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrons : MonoBehaviour {
	public double PlateCharge;
	public double NegativePlateX;
	public double PositivePlateX;
	public double K=90000000000;
	public double e=0.00000000000000000016;
	//public double m=0.00000000000000000000000000000091;
	public double m=1e-07;
	public double acceleration;
	public double velocity;
	public double force;
	public float resistance;
	public float dialationInActivatingCollion;
	// Use this for initialization
	void Start () {
		velocity = 0;
		StartCoroutine (disableTrigger());
	}
	IEnumerator disableTrigger(){
		yield return new WaitForSeconds(dialationInActivatingCollion);
		gameObject.GetComponent<SphereCollider> ().isTrigger = false;
		print ("begin: " + gameObject.GetComponent<Rigidbody> ().velocity.x);
	}
	// Update is called once per frame
	double applyPlateForce(){
		double currentForce=0;
		double negaX=NegativePlateX - transform.position.x;
		double posiX = transform.position.x - PositivePlateX;
		if(negaX!=0)
		currentForce += K * e * PlateCharge / (negaX * negaX);
		if(posiX!=0)
		currentForce+=K * e * PlateCharge / (posiX * posiX);
		return currentForce;
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
		if (collider.tag == "resistor")
			gameObject.GetComponent<Rigidbody> ().drag = resistance;
		
	}
	void OnTriggerExit(Collider collider){
		if (collider.tag == "resistor")
			gameObject.GetComponent<Rigidbody> ().drag = 0;
	}
}
