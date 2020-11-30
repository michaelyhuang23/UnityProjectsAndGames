using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devided_print1 : MonoBehaviour {
	public void inverse(int vertex,float left,float right,float[] parameters){
		if(left==0)left=0.001f;
		if(right==0)right=-0.001f;
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]/x+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		gameObject.GetComponent<LineRenderer>().positionCount=vertex;
		gameObject.GetComponent<LineRenderer>().SetPositions(positions);
	}
	public void Tan(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Tan(x)+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		gameObject.GetComponent<LineRenderer>().positionCount=vertex;
		gameObject.GetComponent<LineRenderer>().SetPositions(positions);
	}
	public void Destroy_(){
		Destroy(gameObject);
	}
}
