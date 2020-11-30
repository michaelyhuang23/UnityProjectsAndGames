using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class forcer{
	public GameObject forcer_obj;
	public Vector3 forcer_pos;
	public float forcer_q;
	public int id;
}
public class draw_eletrical_lines : MonoBehaviour {
	public float line_length=0.1f;
	public float k=9000000000f;
	public Vector3 force=Vector3.zero;
	public forcer[] forcers=new forcer[100];
	int index=0;
	Vector3 force1=Vector3.zero;
	Vector3 force2=Vector3.zero;
	Vector3 position;
	int i=0;
	void Awake(){
		position=transform.position;
		index=0;
		i=0;
	}
	public void awaken(GameObject obj){
		forcer current_forcer=new forcer();
		current_forcer.forcer_obj=obj;
		current_forcer.forcer_pos=obj.transform.position;
		current_forcer.forcer_q=obj.GetComponent<force_field_creator>().q;
		current_forcer.id=obj.GetComponent<force_field_creator>().id;
		forcers[index]=current_forcer;
		index++;
	}
	void OnDrawGizmos(){
		if(i>=index)return;
		print(index);
		float q=forcers[i].forcer_q;
		Vector3 pos=forcers[i].forcer_pos;
		int id=forcers[i].id;if(i<index-1)i++;
		Vector3 vector=Vector3.Normalize(position-pos);
		float r=Vector3.Distance(pos,position);
		if(id==1)
			force1=q/(r*r)*k*vector;
		if(id==2)
			force2=q/(r*r)*k*vector;
		force=force1+force2;
		Vector3 draw_vector=vector*(q/Mathf.Abs(q))*line_length;
		Vector3 end_pos=position+draw_vector;
        Gizmos.DrawLine(position,end_pos);
		position=end_pos;
	}
}
