using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGrow : MonoBehaviour
{
    public float setSize;
    public void Enter(){
        transform.localScale = new Vector3(setSize+1, setSize+1, 1);
    }

    public void Exit(){
        transform.localScale = new Vector3(1, 1, 1);
    }
}
