using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float amount;
    void FixedUpdate(){
        transform.Rotate(0, 0, amount);
    }
}
