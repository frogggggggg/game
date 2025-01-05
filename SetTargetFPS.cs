using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    void FixedUpdate(){
        if(PlayerPrefs.GetFloat("fps")!=null){
            Application.targetFrameRate = (int)PlayerPrefs.GetFloat("fps");
        }else Application.targetFrameRate = 60;
    }
}
