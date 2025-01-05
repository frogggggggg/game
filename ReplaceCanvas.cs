using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceCanvas : MonoBehaviour
{
    void Start()
    {
        if (FadeOut.oldCanvas&&LoadScene.tutorial==""){
            MoveParts(FadeOut.oldCanvas);
        }
    }

    void MoveParts(GameObject workspace){
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }

        List<Transform> childList = new List<Transform>();
        foreach (Transform child in workspace.transform){
            childList.Add(child);
        }
        foreach (Transform child in childList){
            child.SetParent(transform);
        }

        Destroy(workspace);
    }

}
