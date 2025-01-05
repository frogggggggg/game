using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleWIthCamera : MonoBehaviour
{
    public float ScaleX, ScaleY;
    void FixedUpdate(){
        if(ScaleX > 0){
            transform.localScale = new Vector2(-(Camera.main.orthographicSize/2)*ScaleX, transform.localScale.y);
        }
        if(ScaleY > 0){
            transform.localScale = new Vector2(transform.localScale.x, -(Camera.main.orthographicSize/2)*ScaleY);
        }
        //transform.localScale = -(Vector2.one+(Camera.main.orthographicSize/2)*new Vector2(ScaleX, ScaleY));
    }
}
