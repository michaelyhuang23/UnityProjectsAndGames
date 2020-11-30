using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Material materialRed;
    public void drawVector(Vector2 vector_,bool isCalculated){
      transform.position=new Vector3(0,0,0);
      transform.localScale=new Vector3(vector_.magnitude,1,1);
      transform.rotation=Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(vector_.y,vector_.x)*Mathf.Rad2Deg));
      if(isCalculated){
        Renderer[] renderers=gameObject.GetComponentsInChildren<Renderer>();
        for(int i=0;i<renderers.Length;i++){
          renderers[i].material=materialRed;
        }
      }
    }
    public void ClearVector(){
      Destroy(gameObject);
    }
}
