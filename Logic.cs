using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Logic : MonoBehaviour
{
    public bool And, Or, Not, Light, Splitter;
    public GameObject Input1, Input2;
    public List<GameObject> Output = new List<GameObject>();
    public Sprite On, Off;
    private bool Spawned = true;
    public bool didLogic;
    public bool powered;
    public bool splitToggle = false;
    public float speed = 0;

    void OnEnable(){
        speed = PlayerPrefs.GetFloat("speed");
    }

    void Start(){
        speed = PlayerPrefs.GetFloat("speed");
    }
    public void DoLogic(int count, bool fromOutput = false){
        if(And){
            if(Input1.GetComponent<Wireable>().wire && Input2.GetComponent<Wireable>().wire){
                if(Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On&&Input2.GetComponent<Wireable>().wire.GetComponent<wire>().On){
                    PowerWire(true, count);
                }else PowerWire(false, count);
            }else PowerWire(false, count);
        }
        if(Or){
            if((Input1.GetComponent<Wireable>().wire && Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On)||(Input2.GetComponent<Wireable>().wire && Input2.GetComponent<Wireable>().wire.GetComponent<wire>().On)){
                PowerWire(true, count);
            }else PowerWire(false, count);
        }
        if(Not){
            if(Input1.GetComponent<Wireable>().wire && Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On){
                PowerWire(false, count);
            }else PowerWire(true, count);
        }
        if(Light){
            if((Input1.GetComponent<Wireable>().wire && Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On)){
                gameObject.GetComponent<Image>().sprite = On;
            }else{
                gameObject.GetComponent<Image>().sprite = Off;
            }
        }
        if(Splitter){
            if(!splitToggle){
                if((Input1.GetComponent<Wireable>().wire && Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On)){
                    PowerWire(true, count);
                }else{
                    PowerWire(false, count);
                }
            }else{
                if(!fromOutput&&(Input1.GetComponent<Wireable>().wire && Input1.GetComponent<Wireable>().wire.GetComponent<wire>().On)){
                    PowerWire(!powered, count);
                }
                if(fromOutput){
                    PowerWire(powered, count);
                }
            }
        }
    }

    public void ToggleSplitter(Image image){
        splitToggle = !splitToggle;
        if(splitToggle){
            image.color = Color.red;
        }else{
            image.color = Color.gray;
        }
    }

    public void PowerWire(bool power, int count){
        if(speed == 0&&count<1500){
            powered = power;
            foreach(GameObject output in Output){
                if(output.GetComponent<Wireable>().wire&&!didLogic){
                    if(output.GetComponent<Wireable>().wire.GetComponent<wire>().On!=power){
                        didLogic = true;
                        output.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(power, count+1);
                        didLogic = false;
                    }
                }
            }
        }else{
            StartCoroutine(PowerWireTime(power, speed));
        }
    }

    public IEnumerator PowerWireTime(bool power, float speed){
        yield return new WaitForSeconds(Mathf.Pow(10, speed)/10000);
        powered = power;
        foreach(GameObject output in Output){
            if(output.GetComponent<Wireable>().wire&&!didLogic){
                if(output.GetComponent<Wireable>().wire.GetComponent<wire>().On!=power){
                    didLogic = true;
                    output.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(power, -1);
                    didLogic = false;
                }
            }
        }
    }

    //void FixedUpdate(){
    //    didLogic = false;
    //}
}
