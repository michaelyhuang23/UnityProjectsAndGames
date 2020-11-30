using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float scrollWheelSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime,0);
        gameObject.GetComponent<Camera>().orthographicSize+=Input.GetAxis("Mouse ScrollWheel")*scrollWheelSpeed*Time.deltaTime;
    }
}
