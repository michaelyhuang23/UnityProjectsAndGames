using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fieldfluid : MonoBehaviour
{
    // Start is called before the first frame update

    public float forceStrength=1f;
    public void StartEmit(Vector3 direction,Vector3 velo){
      gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
      gameObject.GetComponent<Rigidbody>().AddForce(direction*forceStrength);
      transform.rotation=Quaternion.Euler(0,Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg,0);
      Destroy(gameObject,20);
    }




}
