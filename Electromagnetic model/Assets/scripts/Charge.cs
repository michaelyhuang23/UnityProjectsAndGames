using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fluid;
    private float yRotate=-180;
    public float interval=2;
    public float speed=1;
    public float displace=1;
    // Update is called once per frame
    void moveCharge(){
      if(Input.GetMouseButton(0)){
        Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position=new Vector3(mousePos.x,0,mousePos.z);
      }else{
      gameObject.GetComponent<Rigidbody>().velocity+=new Vector3(Input.GetAxis("Horizontal")*speed,0,Input.GetAxis("Vertical")*speed);
      }
    }
    void FixedUpdate()
    {
        moveCharge();
        Vector3 displacement=new Vector3(Mathf.Sin(yRotate),0,Mathf.Cos(yRotate)).normalized;
        Vector3 direction=new Vector3(Mathf.Cos(yRotate),0,-Mathf.Sin(yRotate));
        GameObject currentFluid=Instantiate(fluid,transform.position+displacement*displace,transform.rotation);

        //if(Mathf.Abs(yRotate)>90){
          //direction.x=-direction.x;
          //direction.z=-direction.z;
        //}
        currentFluid.GetComponent<Fieldfluid>().StartEmit(direction.normalized,gameObject.GetComponent<Rigidbody>().velocity);
        yRotate+=interval;
        if(yRotate>=180){
          yRotate-=360;
        }
    }
}
