using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour
{
    public GameObject awakeObject;
    void Awake(){
        awakeObject.SetActive(true);
    }
}
