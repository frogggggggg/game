using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public string Tutorial;
    public List<GameObject> bannedParts;
    void Awake(){
        if(LoadScene.tutorial==Tutorial){
            foreach(GameObject part in bannedParts){
                part.SetActive(false);
            }
        }else gameObject.SetActive(false);
    }
}
