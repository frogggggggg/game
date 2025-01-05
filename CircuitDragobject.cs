using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CircuitDragobject : MonoBehaviour
{
    public Transform dragged_object;

    void Update(){
        if(dragged_object != null){

            dragged_object.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach(Transform child in dragged_object.GetComponentsInChildren<Transform>()) {
                if(child.tag=="wire"){
                    child.gameObject.GetComponent<VoltWire>().SetPositions();
                }
                if(child.tag=="input"&&child.gameObject.GetComponent<CircuitWireable>().wire){
                    child.gameObject.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().SetPositions();
                }
            }
        }
    }
}
