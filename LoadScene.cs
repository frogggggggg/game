using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static string tutorial = "";
    public GameObject FadeOut;
    public string Tutorial;
    public bool win;

    void Start(){
        if(Tutorial!=""&&PlayerPrefs.GetInt(Tutorial)==1){
            gameObject.GetComponent<Image>().color = Color.green;
        }
    }
    public void Load(){
        if(win){
            PlayerPrefs.SetInt(tutorial, 1);
        }
        if(Tutorial!=""){
            tutorial = Tutorial;
        }
        FadeOut.SetActive(true);
    }
}
