using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireable : MonoBehaviour
{
    public GameObject Output;
    public GameObject wire;
    public void DetectClick()
    {
        if(wire){
            GameObject input = wire.GetComponent<wire>().Input;
            GameObject output = wire.GetComponent<wire>().Output;
            Destroy(wire);
            output.GetComponent<Wireable>().wire = null;
            input.GetComponent<Wireable>().wire = null;
            if(output.transform.parent.gameObject.GetComponent<Logic>()){
                output.transform.parent.gameObject.GetComponent<Logic>().DoLogic(0, true);
            }
            if(input.transform.parent.gameObject.GetComponent<Logic>()){
                input.transform.parent.gameObject.GetComponent<Logic>().DoLogic(0, true);
            }
        }
        GameObject Canvas = transform.root.gameObject;
        Canvas.GetComponent<Wiremaker>().CreateWire(transform);
    }
}
