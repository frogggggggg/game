using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitWireable : MonoBehaviour
{
    public GameObject Output;
    public GameObject wire;
    public void DetectClick()
    {
        if(wire){
            GameObject input = wire.GetComponent<VoltWire>().Input;
            GameObject output = wire.GetComponent<VoltWire>().Output;
            Destroy(wire);
            output.GetComponent<CircuitWireable>().wire = null;
            input.GetComponent<CircuitWireable>().wire = null;
            if(output.transform.parent.gameObject.GetComponent<CircuitLogic>()){
                output.transform.parent.gameObject.GetComponent<CircuitLogic>().DoLogic();
            }
            if(input.transform.parent.gameObject.GetComponent<CircuitLogic>()){
                input.transform.parent.gameObject.GetComponent<CircuitLogic>().DoLogic();
            }
        }
        GameObject Canvas = transform.root.gameObject;
        Canvas.GetComponent<Circuitwiremaker>().CreateWire(transform);
    }
}
