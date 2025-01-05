using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickedObject : MonoBehaviour
{
    public static GameObject clickedObject;

    public void Clicked(){
        clickedObject = gameObject;
        StartCoroutine(wait());
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(.1f);
        clickedObject = null;
    }
}
