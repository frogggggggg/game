using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnComponent : MonoBehaviour
{
    public Transform Canvas;
    public GameObject Component;
    public Slider OutputSlider;
    public KeyCode pressedChar;
    void Update(){
        if(Input.GetKeyDown(pressedChar)){
            Spawn();
        }
    }
    public void Spawn(){
        GameObject SpawnedComponent = Instantiate(Component, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        SpawnedComponent.transform.SetParent(Canvas);
        if(SpawnedComponent.GetComponent<Dragable>()){
            SpawnedComponent.GetComponent<Dragable>().DetectDrag();
        }else SpawnedComponent.GetComponent<CircuitDragable>().DetectDrag();

        if(SpawnedComponent.GetComponent<DefineOutputs>()){
            SpawnedComponent.GetComponent<DefineOutputs>().outputAmount = (int)OutputSlider.value;
        }

    }
}
