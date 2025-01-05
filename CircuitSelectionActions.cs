using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSelectionActions : MonoBehaviour
{
    public Transform selection, copy, delete;

    public void RemoveSelection(){
        GameObject Canvas = transform.root.gameObject;
        if(Canvas.GetComponent<CircuitDragobject>().dragged_object!=transform){
            List<Transform> childList = new List<Transform>();
            foreach(Transform child in transform){
                if(child!=selection&&child!=copy&&child!=delete){
                    childList.Add(child);
                }
            }
            foreach(Transform child in childList){
                child.SetParent(transform.parent);
            }
            Destroy(gameObject);
        }
    }
}
