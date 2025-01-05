using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAll : MonoBehaviour
{
    public Transform Canvas;
    public void Clear(){
        foreach(Transform child in Canvas){
            if (child.gameObject.tag != "tutorial"){
                Destroy(child.gameObject);
            }
        }
    }
}
