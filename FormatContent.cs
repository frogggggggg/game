using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatContent : MonoBehaviour
{
    public GameObject LoadMenu;
    public void Format(){
        float width = LoadMenu.GetComponent<RectTransform>().sizeDelta.x;
        int count = 0;
        foreach(Transform child in transform){
            float childWidth = child.gameObject.GetComponent<RectTransform>().sizeDelta.x*1.25f;
            child.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(((count*childWidth)/width-Mathf.Floor((count*childWidth)/width))*width+childWidth/2, (-Mathf.Floor((count*childWidth)/width))*childWidth-childWidth);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Mathf.Floor((count*childWidth)/width)*childWidth-childWidth+350);
            count++;
        }
    }
}
