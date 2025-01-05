using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public GameObject output;
    void FixedUpdate(){
        if(output.GetComponent<CircuitWireable>().wire){
            output.GetComponent<CircuitWireable>().wire.GetComponent<VoltWire>().Activate(true, 5, 1, gameObject);
        }
    }
}
