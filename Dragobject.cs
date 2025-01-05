using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Dragobject : MonoBehaviour
{
    public Transform dragged_object;

    void Update(){
        if(dragged_object != null){
            Vector2 mousepos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            dragged_object.transform.position = mousepos;

            foreach(Transform child in dragged_object.GetComponentsInChildren<Transform>()) {
                if(child.tag=="wire"){
                    child.gameObject.GetComponent<wire>().SetPositions();
                }
                if(child.tag=="input"&&child.gameObject.GetComponent<Wireable>().wire){
                    child.gameObject.GetComponent<Wireable>().wire.GetComponent<wire>().SetPositions();
                }
            }
        }
    }
}
