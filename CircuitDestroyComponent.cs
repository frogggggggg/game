using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitDestroyComponent : MonoBehaviour
{
    public GameObject Canvas;

    public void Delete(){
        if(Canvas.GetComponent<CircuitDragobject>().dragged_object){
            foreach(Transform child in Canvas.GetComponent<CircuitDragobject>().dragged_object.GetComponentsInChildren<Transform>()){
                if(child.gameObject.tag == "input"){
                    if(child.GetComponent<CircuitWireable>().wire){
                        Destroy(child.GetComponent<CircuitWireable>().wire);
                    }
                }
                if(child.gameObject.tag == "output"){
                    if(child.GetComponent<CircuitWireable>().wire){
                        child.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Activate(false, 0, 0);
                    }
                }
            }
            Destroy(Canvas.GetComponent<CircuitDragobject>().dragged_object.gameObject);
            Camera.main.gameObject.GetComponent<PanZoom>().canzoom = true;
        }
    }
}
