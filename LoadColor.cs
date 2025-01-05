using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadColor : MonoBehaviour
{
    public string pref;
    void OnLevelWasLoaded(){
        string colorString = PlayerPrefs.GetString(pref);
        colorString = colorString.Replace(",", "").Replace(")", "").Replace("RGBA(", "");
        Color myColor = new Color(float.Parse(colorString.Split(' ')[0]), float.Parse(colorString.Split(' ')[1]), float.Parse(colorString.Split(' ')[2]));
        
        GetComponent<Camera>().backgroundColor = myColor;
    }
    void Start()
    {
        OnLevelWasLoaded();
    }
}
