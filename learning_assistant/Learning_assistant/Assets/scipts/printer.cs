using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;



public class printer : MonoBehaviour {

	int vertex=5000;
	float left=-20;
	float right=20;
	public float window_width=400;
	public float window_height=450;
	string a="1";
	string b="0";
	string c="0";
	string d="0";
	bool[] functions=new bool[9]{false,false,false,false,false,false,false,false,false};
	string[] function_name=new string[9]{"linear","quadratic","cubic","inverse","exponent","log","Sin","Cos","Tan"};
	bool printing=false;
	float[] parameter_=new float[4];
	float x_=0f;
	string str="";
	Transform[] clones=new Transform[9]{null,null,null,null,null,null,null,null,null};
	public Transform pointer_;
	GUIStyle black_style=new GUIStyle();
	void Awake(){
		printing=false;
		black_style.normal.textColor=new Color(0,0,0,1);
		for(int i=0;i<9;i++){
			clones[i]=Instantiate(pointer_);
			clones[i].gameObject.SetActive(false);
		}
	}
	void print_function(){
		parameter_[0]=float.Parse(a);
		parameter_[1]=float.Parse(b);
		parameter_[2]=float.Parse(c);
		parameter_[3]=float.Parse(d);
		printing=true;
		for(int i=0;i<9;i++){
			if(functions[i]){
				transform.GetChild(i).GetComponent<prints>().prints_function(vertex,left,right,parameter_);
			}
		}
	}
	float get_y(float x,int i){
		if(i==0)return x*parameter_[0]+parameter_[1];
		if(i==1)return parameter_[0]*Mathf.Pow(x,2)+parameter_[1]*x+parameter_[2];
		if(i==2)return parameter_[0]*Mathf.Pow(x,3)+parameter_[1]*Mathf.Pow(x,2)+parameter_[2]*x+parameter_[3];
		if(i==3 && x!=0)return parameter_[0]/x;
		if(i==4)return Mathf.Pow(parameter_[0],x)+parameter_[1];
		if(i==5 && x>0)return Mathf.Log(x,parameter_[0])+parameter_[1];
		if(i==6)return parameter_[0]*Mathf.Sin(x)+parameter_[1];
		if(i==7)return parameter_[0]*Mathf.Cos(x)+parameter_[1];
		if(i==8)return parameter_[0]*Mathf.Tan(x)+parameter_[1];
		return Mathf.Infinity;
	}
	void OnGUI(){
		if(!printing){
		GUI.Window(0,new Rect(0,0,window_width,window_height),print_function_window,"print_function");
		}else{
			GUILayout.BeginVertical();
			if(GUILayout.Button("setting")){ 
			    printing=false;
				for(int i=0;i<9;i++){
					clones[i].gameObject.SetActive(false);
				}
				x_=0;
				gameObject.BroadcastMessage("Destroy_");
			}else{
				GUILayout.Label("Insert x:",black_style);
				str=GUILayout.TextField(str,10);
				if(str!="" && str!="-")
			x_=float.Parse(str);
			for(int i=0;i<9;i++){
				if(functions[i]){
					float y_=get_y(x_,i);
						if(!float.IsInfinity(y_) && !float.IsNaN(y_)){
						clones[i].gameObject.SetActive(true);
					    clones[i].position=new Vector3(x_,y_,0);
							GUILayout.Label("function "+function_name[i]+" y : "+y_,black_style);
					}
				}
			}
		}
		}
	}
	void print_function_window(int id){
		GUILayout.BeginVertical();
		GUILayout.Label("Vertex Number：");
		vertex=int.Parse(GUILayout.TextField(vertex.ToString(),10));
		GUILayout.Label("Interval Left:");
		left=float.Parse(GUILayout.TextField(left.ToString(),10));
		GUILayout.Label("Interval right:");
		right=float.Parse(GUILayout.TextField(right.ToString(),10));
		GUILayout.Label("parameter a:");
		a=GUILayout.TextField(a,10);
		GUILayout.Label("parameter b:");
		b=GUILayout.TextField(b,10);
		GUILayout.Label("parameter c:");
		c=GUILayout.TextField(c,10);
		GUILayout.Label("parameter d:");
		d=GUILayout.TextField(d,10);
		GUILayout.BeginHorizontal();
		functions[0]=GUILayout.Toggle(functions[0],"linear");
		functions[1]=GUILayout.Toggle(functions[1],"quadratic");
		functions[2]=GUILayout.Toggle(functions[2],"cubic");
		functions[3]=GUILayout.Toggle(functions[3],"inverse");
		functions[4]=GUILayout.Toggle(functions[4],"exponent");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		functions[5]=GUILayout.Toggle(functions[5],"log");
		functions[6]=GUILayout.Toggle(functions[6],"Sin");
		functions[7]=GUILayout.Toggle(functions[7],"Cos");
		functions[8]=GUILayout.Toggle(functions[8],"Tan");
		GUILayout.EndHorizontal();
		if(GUILayout.Button("print!")){
			print_function();
		}
		GUILayout.EndVertical();
	}
}
