using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPref : MonoBehaviour
{
    public string pref;
    
    public void setPref(System.Single vol){
        PlayerPrefs.SetFloat(pref, vol);
    }
}
