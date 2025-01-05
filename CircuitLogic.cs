using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CircuitLogic : MonoBehaviour
{
    public bool Battery, Light, Resistor, Switch;
    public GameObject Input1, Input2;
    public GameObject[] Output;
    public Sprite On, Off, switchOn, switchOff;
    public Image shine;
    public TMP_InputField resistance;
    private bool Spawned = true;
    public bool didLogic;
    public bool powered;
    public bool splitToggle = false;
    public float speed = 0;
    public bool switched;

    void OnEnable(){
        speed = PlayerPrefs.GetFloat("speed");
    }

    void Start(){
        speed = PlayerPrefs.GetFloat("speed");
    }

    public void SwitchValue(){
        GameObject Canvas = transform.root.gameObject;
        if(Canvas.GetComponent<CircuitDragobject>().dragged_object!=transform){
            switched = !switched;
        }
        if(switched){
            GetComponent<Image>().sprite = switchOn;
        }else GetComponent<Image>().sprite = switchOff;
    }
    public bool DoLogic(GameObject battery = null, bool fromOutput = false){
        float amps = 0;
        float ohms = 0;
        if(Input1.GetComponent<CircuitWireable>().wire){
            amps = Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Amps;
            ohms = Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Ohms;
        }
        if(Light){
            if((Input1.GetComponent<CircuitWireable>().wire && Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().On)){
                if(PowerWire(true, amps, ohms, battery)&&amps<=3){
                    gameObject.GetComponent<Image>().sprite = On;
                    shine.color = new Color(shine.color.r, shine.color.g, shine.color.b, amps/5f/2f);
                }else {
                    gameObject.GetComponent<Image>().sprite = Off;
                    shine.color = new Color(shine.color.r, shine.color.g, shine.color.b, 0f);
                }
                return PowerWire(true, amps, ohms, battery);
            }else{
                shine.color = new Color(shine.color.r, shine.color.g, shine.color.b, 0f);
                gameObject.GetComponent<Image>().sprite = Off;
                return PowerWire(false, amps, ohms, battery);
            }
            
        }
        if(Battery){
            if(battery==gameObject){
                return true;
            }else{
                if((Input1.GetComponent<CircuitWireable>().wire && Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().On)){
                    return PowerWire(true, amps, ohms, battery);
                }
            }
        }
        if(Resistor){
            if((Input1.GetComponent<CircuitWireable>().wire && Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().On)){
                if(resistance.text!=""){
                    amps = (amps*ohms)/(ohms+float.Parse(resistance.text));
                }
                return PowerWire(true, amps, ohms, battery);
            }else return PowerWire(false, 0, 0, battery);
        }
        if(Switch){
            if(switched&&Input1.GetComponent<CircuitWireable>().wire && Input1.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().On){
                return PowerWire(true, amps, ohms, battery);
            }else{
                PowerWire(false, 0, 0, battery);
                return false;
            }
        }
        return false;
    }

    public bool PowerWire(bool power, float amps, float ohms, GameObject battery){
        if(speed == 0){
            powered = power;
            foreach(GameObject output in Output){
                if(output.GetComponent<CircuitWireable>().wire&&!didLogic){
                    return output.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Activate(power, amps, ohms, battery);
                    didLogic = false;
                }
            }
        }else{
            StartCoroutine(PowerWireTime(power, speed));
        }
        return false;
    }

    public IEnumerator PowerWireTime(bool power, float speed){
        yield return new WaitForSeconds(Mathf.Pow(10, speed)/10000);
        powered = power;
        foreach(GameObject output in Output){
            if(output.GetComponent<Wireable>().wire&&!didLogic){
                if(output.GetComponent<Wireable>().wire.GetComponent<wire>().On!=power){
                    didLogic = true;
                    output.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(power, 0);
                    didLogic = false;
                }
            }
        }
    }

    void FixedUpdate(){
        didLogic = false;
    }
}
