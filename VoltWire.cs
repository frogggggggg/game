using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltWire : MonoBehaviour
{
    public GameObject Output, Input;
    public bool On;
    public float Amps, Ohms;
    
    public void SetPositions(){
        gameObject.GetComponent<LineRenderer>().SetPosition(0, Output.transform.position-transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, Input.transform.position-transform.position);
    }

    public bool Activate(bool power, float amps, float ohms, GameObject battery = null){
        if(Input){
            On = power;
            Amps = amps;
            Ohms = ohms;
            if(Input.transform.parent.gameObject.GetComponent<CircuitLogic>().DoLogic(battery)&&On){
                GetComponent<LineRenderer>().SetColors(Color.red*(amps/5f)+Color.black*(5f/amps), Color.red*(amps/5f)+Color.black*(5f/amps));
            }else{
                Amps = 0;
                Ohms = 0;
                On = false;
                GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            }
            return Input.transform.parent.gameObject.GetComponent<CircuitLogic>().DoLogic(battery);
        }else return false;
    }
}
