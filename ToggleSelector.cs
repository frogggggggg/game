using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSelector : MonoBehaviour
{
    public Toggle[] toggles;
    public string pref;
    public void DoToggle(GameObject obj){
        foreach(Toggle toggle in toggles){
            if(toggle.gameObject != obj.transform.parent.gameObject){
                toggle.SetIsOnWithoutNotify(false);
            }
        }
        PlayerPrefs.SetString(pref, obj.GetComponent<Image>().color.ToString());
        Start();
    }

    void Start(){
        foreach(Toggle toggle in toggles){
            if(toggle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color.ToString() == PlayerPrefs.GetString(pref)){
                toggle.SetIsOnWithoutNotify(true);
            }
        }
    }
}
