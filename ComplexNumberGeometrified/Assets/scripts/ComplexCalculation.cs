using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexCalculation : MonoBehaviour
{
    int numberOfVectors=0;
    Vector2[] vectors=new Vector2[100];
    Vector2[] calculatedVectors=new Vector2[100];
    string[] real=new string[100];
    string[] imaginary=new string[100];
    string[] calculations=new string[100];
    int numberOfCalculated=0;
    Vector2 result;
    public GameObject vectorGraph;
    GUIStyle labelStyle = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
      labelStyle.normal.textColor = Color.red;
      result=Vector2.zero;
        for(int i=0;i<100;i++){
          vectors[i]=Vector2.zero;
          calculatedVectors[i]=Vector2.zero;
          calculations[i]="Please Enter Calculations";
          real[i]="Enter Real";
          imaginary[i]="Enter Imaginary";
        }
    }


    void Calculate(){
      for(int i=0;i<100;i++){
        calculatedVectors[i]=Vector2.zero;
        if(imaginary[i]=="" || real[i]=="" || imaginary[i]=="Enter Imaginary" || real[i]=="Enter Real"){
          vectors[i]=Vector2.zero;
          continue;
        }
        vectors[i].y=float.Parse(imaginary[i]);
        vectors[i].x=float.Parse(real[i]);
      }
      for(int i=0;i<numberOfVectors;i++){
        switch (calculations[i]){
          case "Please Enter Calculations":
            continue;
          case "":
            continue;
          case "+":
            if(calculatedVectors[i]!=Vector2.zero)
              calculatedVectors[i+1]=vectors[i+1]+calculatedVectors[i];
            else
              calculatedVectors[i+1]=vectors[i+1]+vectors[i];
            numberOfCalculated=i+1;
            break;
          case "-":
            if(calculatedVectors[i]!=Vector2.zero)
              calculatedVectors[i+1]=calculatedVectors[i]-vectors[i+1];
            else
              calculatedVectors[i+1]=vectors[i]-vectors[i+1];
            numberOfCalculated=i+1;
            break;
          case "*":
            if(calculatedVectors[i]!=Vector2.zero)
              calculatedVectors[i+1]=new Vector2(calculatedVectors[i].x*vectors[i+1].x-calculatedVectors[i].y*vectors[i+1].y,calculatedVectors[i].y*vectors[i+1].x+calculatedVectors[i].x*vectors[i+1].y);
            else
              calculatedVectors[i+1]=new Vector2(vectors[i].x*vectors[i+1].x-vectors[i].y*vectors[i+1].y,vectors[i].y*vectors[i+1].x+vectors[i].x*vectors[i+1].y);
            numberOfCalculated=i+1;
            break;
          case "/":
            float magnitude=vectors[i+1].x*vectors[i+1].x+vectors[i+1].y*vectors[i+1].y;
            if(calculatedVectors[i]!=Vector2.zero)
              calculatedVectors[i+1]=new Vector2((calculatedVectors[i].x*vectors[i+1].x+calculatedVectors[i].y*vectors[i+1].y)/magnitude,(calculatedVectors[i].y*vectors[i+1].x-calculatedVectors[i].x*vectors[i+1].y)/magnitude);
            else
              calculatedVectors[i+1]=new Vector2((vectors[i].x*vectors[i+1].x+vectors[i].y*vectors[i+1].y)/magnitude,(vectors[i].y*vectors[i+1].x-vectors[i].x*vectors[i+1].y)/magnitude);
            numberOfCalculated=i+1;
            break;
          default:
           break;
        }
      }
      result=calculatedVectors[numberOfCalculated];
      graphVectors();
    }
    void graphVectors(){
      for(int i=0;i<100;i++){
        if(vectors[i]!=Vector2.zero){
          GameObject n=Instantiate(vectorGraph);
          n.transform.parent=transform;
          n.GetComponent<Arrow>().drawVector(vectors[i],false);
        }
        if(calculatedVectors[i]!=Vector2.zero){
          GameObject n=Instantiate(vectorGraph);
          n.transform.parent=transform;
          n.GetComponent<Arrow>().drawVector(calculatedVectors[i],true);
        }
      }
    }
    void ClearAll(){
      gameObject.BroadcastMessage("ClearVector",SendMessageOptions.DontRequireReceiver);
      for(int i=0;i<numberOfVectors;i++){
        vectors[i]=Vector2.zero;
        calculatedVectors[i]=Vector2.zero;
      }
      result=Vector2.zero;
      numberOfCalculated=0;
    }
    void OnGUI(){
      GUILayout.BeginVertical();
      if(GUILayout.Button("Quit"))
        Application.Quit();
      GUILayout.Space(30);
      numberOfVectors=int.Parse(GUILayout.TextField(numberOfVectors.ToString()));
      GUILayout.BeginHorizontal();
      GUILayout.BeginVertical();
      for(int i=0;i<numberOfVectors;i++){
        if(i%12==0){
          GUILayout.EndVertical();
          GUILayout.BeginVertical();
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        real[i]=GUILayout.TextField(real[i]);
        GUILayout.Space(20);
        imaginary[i]=GUILayout.TextField(imaginary[i]);
        GUILayout.Space(20);
        calculations[i]=GUILayout.TextField(calculations[i]);
        GUILayout.EndHorizontal();
      }
      GUILayout.EndVertical();
      GUILayout.EndHorizontal();
      GUILayout.Space(20);
      if(GUILayout.Button("Calculate")){
        Calculate();
      }
      GUILayout.Space(20);
      if(GUILayout.Button("Clear")){
        ClearAll();
      }
      GUILayout.Space(20);
      GUILayout.Label("Answer: "+result,labelStyle);
      GUILayout.EndVertical();


    }
}
