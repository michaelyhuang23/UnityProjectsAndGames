using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void OnGUI(){
      if(GUILayout.Button("quit")){
        Application.Quit();
      }
    }
}
