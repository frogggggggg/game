using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{
    public GameObject Canvas;

    public void Delete(){
        if(Canvas.GetComponent<Dragobject>().dragged_object){
            foreach(Transform child in Canvas.GetComponent<Dragobject>().dragged_object.GetComponentsInChildren<Transform>()){
                if(child.gameObject.tag == "input"){
                    if(child.GetComponent<Wireable>().wire){
                        Destroy(child.GetComponent<Wireable>().wire);
                    }
                }
                if(child.gameObject.tag == "output"){
                    if(child.GetComponent<Wireable>().wire){
                        child.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(false, 0);
                    }
                }
            }
            Destroy(Canvas.GetComponent<Dragobject>().dragged_object.gameObject);
            Camera.main.gameObject.GetComponent<PanZoom>().canzoom = true;
        }
    }
}
