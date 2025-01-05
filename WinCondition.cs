using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinCondition : MonoBehaviour
{
    public Sprite onSprite;
    public Image[] lights;
    public GameObject win;
    void FixedUpdate(){
        foreach (Image light in lights){
            if(light.sprite == onSprite){
                win.SetActive(true);
            }else {
                win.SetActive(false);
                break;
            }
        }
    }
}
