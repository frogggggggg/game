using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CircuitDragable : MonoBehaviour
{
    public void DetectDrag()
    {
        GameObject Canvas = transform.root.gameObject;
        Canvas.GetComponent<CircuitDragobject>().dragged_object = transform;
        SetZoom(false);
    }

    public void SetZoom(bool zoom){
        Camera.main.gameObject.GetComponent<PanZoom>().canzoom = zoom;
    }

    public void StopDrag()
    {
        GameObject Canvas = transform.root.gameObject;
        if(Canvas.GetComponent<CircuitDragobject>().dragged_object==transform){
            Canvas.GetComponent<CircuitDragobject>().dragged_object = null;
            SetZoom(true);
        }
    }

    public void Clone(){
        GameObject Canvas = transform.root.gameObject;
        GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity);
        clone.transform.SetParent(Canvas.transform);
        Canvas.GetComponent<CircuitDragobject>().dragged_object = clone.transform;
        SetZoom(false);
    }
}
