using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prints : MonoBehaviour {
	public int id;
	public Vector3[] positions_;
	public Transform decided_printer;
	Transform n;
	Vector3[] linear(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*x+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] quadratic(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Pow(x,2)+parameters[1]*x+parameters[2];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] cubic(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Pow(x,3)+parameters[1]*Mathf.Pow(x,2)+parameters[2]*x+parameters[3];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] inverse(int vertex,float left,float right,float[] parameters){
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
		return positions;
	}
	Vector3[] exponent(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=Mathf.Pow(parameters[0],x)+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] log(int vertex,float left,float right,float[] parameters){
		if(left<=0) left=0.001f;
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=Mathf.Log(x,parameters[0])+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] Sin(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Sin(x)+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] Cos(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Cos(x)+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	Vector3[] Tan(int vertex,float left,float right,float[] parameters){
		float x=left;
		float y=0;
		float xd=(right-left)/(vertex-1);
		Vector3[] positions=new Vector3[vertex];
		for(int i=0;i<vertex;i++){
			x=left+xd*i;
			y=parameters[0]*Mathf.Tan(x)+parameters[1];
			positions[i]=new Vector3(x,y,0);
		}
		return positions;
	}
	public void prints_function(int vertex,float left,float right,float[] parameters){
		if(id==0) positions_=linear(vertex,left,right,parameters);
		if(id==1) positions_=quadratic(vertex,left,right,parameters);
		if(id==2) positions_=cubic(vertex,left,right,parameters);
		if(id==4) positions_=exponent(vertex,left,right,parameters);
		if(id==5) positions_=log(vertex,left,right,parameters);
		if(id==6) positions_=Sin(vertex,left,right,parameters);
		if(id==7) positions_=Cos(vertex,left,right,parameters);
		if(id==3){
			if(left<0){
			n=Instantiate(decided_printer);
			n.transform.parent=transform;
			n.GetComponent<devided_print1>().inverse(vertex,left,0,parameters);
			n=Instantiate(decided_printer);
			n.transform.parent=transform;
			n.GetComponent<devided_print1>().inverse(vertex,0,right,parameters);
		    }else positions_=inverse(vertex,left,right,parameters);
		}
		if(id==8){
			int current_vertex=(int)(vertex/((int)((right-left)/(Mathf.PI/2))+1));
			float start=(int)(left/(Mathf.PI/2))*(Mathf.PI/2);
			float end=(int)(right/(Mathf.PI/2))*(Mathf.PI/2);
			if(start!=end){
				n=Instantiate(decided_printer);
				n.transform.parent=transform;
				n.GetComponent<devided_print1>().Tan(current_vertex,left,start-0.01f,parameters);
				n=Instantiate(decided_printer);
				n.transform.parent=transform;
				n.GetComponent<devided_print1>().Tan(current_vertex,end+0.01f,right,parameters);
			if((end-start)>1){
			        int j=(int)(start/(Mathf.PI/2));
		        	do{
				        int k=j+1;	
						n=Instantiate(decided_printer);
						n.transform.parent=transform;
						n.GetComponent<devided_print1>().Tan(current_vertex,j*Mathf.PI/2+0.01f,k*Mathf.PI/2-0.01f,parameters);
						j=k;
					}while(j<(int)(end/(Mathf.PI/2)));
			}
			}else Tan(vertex,left,right,parameters);
		}
		if(positions_!=null){
		gameObject.GetComponent<LineRenderer>().positionCount=vertex;
			gameObject.GetComponent<LineRenderer>().SetColors(Color.blue,new Color(0,181/255,1));
		gameObject.GetComponent<LineRenderer>().SetPositions(positions_);
		}
	}
	public void Destroy_(){
		gameObject.GetComponent<LineRenderer>().positionCount=0;
	}
}
