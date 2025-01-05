using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeOut : MonoBehaviour
{
    public string SceneName;
    public bool keepCanvas;
    public GameObject canvas;
    public static GameObject oldCanvas;
    public void Fade(){
        if(keepCanvas&&LoadScene.tutorial==""){
            oldCanvas = canvas;
            canvas.SetActive(false);
            DontDestroyOnLoad(canvas);
        }
        if(keepCanvas){
            LoadScene.tutorial="";
        }
        SceneManager.LoadScene(SceneName);
    }
}
