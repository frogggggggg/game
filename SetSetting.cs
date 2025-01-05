using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSetting : MonoBehaviour
{
    public Slider slider;
    public string pref;
    public float SetIfNotThere;
    void Start(){
        if(PlayerPrefs.GetFloat(pref) == null){
            PlayerPrefs.SetFloat(pref, SetIfNotThere);
        }
        slider.value = PlayerPrefs.GetFloat(pref);
    }
}
