using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wire : MonoBehaviour
{
    public GameObject Output, Input;
    public bool On;
    public Transform linePart;
    
    public void SetPositions(){
        transform.position = Output.transform.position;
        transform.localScale = new Vector3(Vector2.Distance(Input.transform.position, transform.position), transform.localScale.y, 1);
        transform.right = -(Output.transform.position-Input.transform.position);
    }

    public void Activate(bool power, int count){
        On = power;
        if(power){
            linePart.GetComponent<SpriteRenderer>().color = Color.red;
        }else{
            linePart.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if(Input){
            Input.transform.parent.gameObject.GetComponent<Logic>().DoLogic(count);
        }
    }
}
