using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Circuitwiremaker : MonoBehaviour
{
    public GameObject WireObject, TempWireObject;
    public bool mouseUp;

    public void CreateWire(Transform point){
        Camera.main.gameObject.GetComponent<PanZoom>().canzoom = false;
        //if wire exists
        if(TempWireObject){
            //set parent to output and set wire variable input/output
            if(point.gameObject.tag=="output"){
                if(TempWireObject.GetComponent<VoltWire>().Output){
                    return;
                }
                TempWireObject.GetComponent<VoltWire>().Output = point.gameObject;
                TempWireObject.transform.SetParent(point.parent);
            }else{
                if(TempWireObject.GetComponent<VoltWire>().Input){
                    return;
                }
                TempWireObject.GetComponent<VoltWire>().Input = point.gameObject;
            }
            point.gameObject.GetComponent<CircuitWireable>().wire = TempWireObject;
            //set second point for wire
            TempWireObject.GetComponent<LineRenderer>().SetPosition(1, (Vector2)point.position);
            TempWireObject = null;
            Camera.main.gameObject.GetComponent<PanZoom>().canzoom = true;
        //if wire does not exist
        }else{
            //create wire and set first position
            TempWireObject = Instantiate(WireObject, Vector3.zero, Quaternion.identity);
            TempWireObject.GetComponent<LineRenderer>().SetPosition(0, (Vector2)point.position);
            //set parent to output and set wire variable input/output
            if(point.gameObject.tag=="output"){
                if(TempWireObject.GetComponent<VoltWire>().Output){
                    return;
                }
                TempWireObject.GetComponent<VoltWire>().Output = point.gameObject;
                TempWireObject.transform.SetParent(point.parent);
            }else{
                if(TempWireObject.GetComponent<VoltWire>().Input){
                    return;
                }
                TempWireObject.GetComponent<VoltWire>().Input = point.gameObject;
            }
            point.gameObject.GetComponent<CircuitWireable>().wire = TempWireObject;
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
            TempWireObject.GetComponent<LineRenderer>().SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
