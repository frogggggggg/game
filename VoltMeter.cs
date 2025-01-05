using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoltMeter : MonoBehaviour
{
    public GameObject RedPoint, BlackPoint;
    public LineRenderer RedLine, BlackLine;
    public TMP_Text Amps;
    
    public bool clicked;
    public GameObject clickedObject;
    void LateUpdate()
    {
        if(clickedObject){
            RedPoint.transform.position = clickedObject.GetComponent<CircuitLogic>().Input1.transform.position;
            BlackPoint.transform.position = clickedObject.GetComponent<CircuitLogic>().Output[0].transform.position;
            if(clickedObject.GetComponent<CircuitLogic>().Output[0].GetComponent<CircuitWireable>().wire){
                Amps.text = (Mathf.Round(clickedObject.GetComponent<CircuitLogic>().Output[0].GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Amps*100)*.081f).ToString();
            }
        }else{
            Amps.text = "---";
        }
        RedLine.SetPosition(0, RedLine.gameObject.transform.localPosition);
        RedLine.SetPosition(1, RedPoint.transform.localPosition);
        BlackLine.SetPosition(0, BlackLine.gameObject.transform.localPosition);
        BlackLine.SetPosition(1, BlackPoint.transform.localPosition);

        if(clicked&&ClickedObject.clickedObject!=null&&ClickedObject.clickedObject!=gameObject){
            clicked = false;
            clickedObject = ClickedObject.clickedObject;
        }
    }

    public void StartFind(){
        GameObject Canvas = transform.root.gameObject;
        if(Canvas.GetComponent<CircuitDragobject>().dragged_object!=transform){
            clicked = true;
        }
    }
}
