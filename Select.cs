using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject Canvas, SelectionObject, Selection;
    public bool Selecting;
    public Vector2 Point1, Point2;
    public void StartSelect(){
        Camera.main.gameObject.GetComponent<PanZoom>().enabled = false;
        Selecting = true;
        Selection = null;
    }

    public void PopAll(){
        foreach(Transform child in Canvas.transform){
            if(child.gameObject.tag=="selection"){
                child.gameObject.GetComponent<SelectionActions>().RemoveSelection();
            }
        }
    }

//by far one of the most despicible pieces of code I have created... I MEAN IT WORKS THO sooooooo
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            StartSelect();
        }
        if(Selecting){
            Vector2Int mousePos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(Input.GetMouseButtonDown(0)){
                Point1 = mousePos;
                Selection = Instantiate(SelectionObject, Point1, Quaternion.identity).transform.GetChild(0).gameObject;
                Selection.transform.parent.SetParent(Canvas.transform);
                Selection.transform.position = Point1;
            }
            if(Input.GetMouseButtonUp(0)&&Selection){
                Point2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var rect = Rect.MinMaxRect(Point1.x, Point1.y, Point2.x, Point2.y);
                List<Transform> childList = new List<Transform>();
                foreach(Transform child in Canvas.transform){ 
                    if(Selection.GetComponent<RectTransform>().rect.Contains(Selection.transform.InverseTransformPoint((Vector2)child.position), true)&&child.gameObject.tag != "tutorial"&&child.gameObject.active == true){
                        childList.Add(child);
                    }
                }
                foreach(Transform child in childList){
                    child.SetParent(Selection.transform.parent);
                    child.transform.SetSiblingIndex(0);
                }
                Selection.transform.parent.GetComponent<RectTransform>().pivot = Vector2Int.RoundToInt((Selection.GetComponent<RectTransform>().sizeDelta)*(Vector2)Selection.transform.localScale/2);//for some reason multiplying by 1/2 if other pivotset removed
                Selection.transform.parent.position+=Vector3Int.RoundToInt(((Vector2)Selection.GetComponent<RectTransform>().sizeDelta/2)*(Vector2)Selection.transform.localScale);
                Selecting = false;
                Selection = null;
                Camera.main.gameObject.GetComponent<PanZoom>().enabled = true;
            }
            if(Input.GetMouseButton(0)&&Selection){
                Vector2 selectionSize = Point1-(Vector2)mousePos;
                
                //Selection.transform.parent.GetComponent<RectTransform>().pivot = (Selection.GetComponent<RectTransform>().sizeDelta/2)*(Vector2)Selection.transform.localScale;
                Selection.transform.parent.GetChild(1).transform.localPosition = new Vector2(Mathf.Abs(selectionSize.x), Mathf.Abs(selectionSize.y))/2-selectionSize/2;
                
                //Selection.transform.position = Point1;
                
                Selection.transform.localScale = new Vector2(-selectionSize.x/(Mathf.Abs(selectionSize.x)+.00001f), -selectionSize.y/(Mathf.Abs(selectionSize.y)+.00001f));
                Selection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(selectionSize.x));
                Selection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(selectionSize.y));
                
            }
        }
    }



}
