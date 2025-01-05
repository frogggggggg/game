using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Wiremaker : MonoBehaviour
{
    public GameObject WireObject, TempWireObject;
    public bool mouseUp;

    public void CreateWire(Transform point){
        Camera.main.gameObject.GetComponent<PanZoom>().canzoom = false;
        //if wire exists
        if(TempWireObject){
            //set parent to output and set wire variable input/output
            if(point.gameObject.tag=="output"){
                if(TempWireObject.GetComponent<wire>().Output){
                    return;
                }
                TempWireObject.GetComponent<wire>().Output = point.gameObject;
                TempWireObject.transform.SetParent(point.parent);
            }else{
                if(TempWireObject.GetComponent<wire>().Input){
                    return;
                }
                TempWireObject.GetComponent<wire>().Input = point.gameObject;
            }
            point.gameObject.GetComponent<Wireable>().wire = TempWireObject;
            //set second point for wire
            SetSecondPoint(point.position);
            TempWireObject = null;
            Camera.main.gameObject.GetComponent<PanZoom>().canzoom = true;
        //if wire does not exist
        }else{
            //create wire and set first position
            TempWireObject = Instantiate(WireObject, Vector3.zero, Quaternion.identity);
            SetFirstPoint(point.position);
            //set parent to output and set wire variable input/output
            if(point.gameObject.tag=="output"){
                if(TempWireObject.GetComponent<wire>().Output){
                    return;
                }
                TempWireObject.GetComponent<wire>().Output = point.gameObject;
                TempWireObject.transform.SetParent(point.parent);
            }else{
                if(TempWireObject.GetComponent<wire>().Input){
                    return;
                }
                TempWireObject.GetComponent<wire>().Input = point.gameObject;
            }
            point.gameObject.GetComponent<Wireable>().wire = TempWireObject;
        }
        if(point.parent.gameObject.GetComponent<Logic>()){
            point.parent.gameObject.GetComponent<Logic>().DoLogic(0, true);
        }
    }

     
 

    void Update(){
        if(Input.GetMouseButtonUp(0)&&TempWireObject){
            mouseUp = true;
        }
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()&&mouseUp)
        {
            if(TempWireObject){
                Camera.main.gameObject.GetComponent<PanZoom>().canzoom = true;
                mouseUp = false;
                Destroy(TempWireObject);
            }
        }
        if(TempWireObject != null){
            SetSecondPoint((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void SetFirstPoint(Vector2 pos){
        TempWireObject.transform.position = pos;
    }
    void SetSecondPoint(Vector2 pos){
        TempWireObject.transform.right = -((Vector2)TempWireObject.transform.position-pos);
        TempWireObject.transform.localScale = new Vector3(Vector2.Distance(pos, TempWireObject.transform.position), TempWireObject.transform.localScale.y, 1);
    }
}
