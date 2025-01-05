using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour {
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public bool canzoom;
    public bool enabled;
    public Touch touchZero;
    private bool lockTouch = false;
    public int maxsize;
	
	// Update is called once per frame
    void FixedUpdate(){
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized*Camera.main.orthographicSize*.03f);
    }
	void Update () {
        if(canzoom&&enabled){
            if(Input.GetMouseButtonDown(0)){
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if(Input.touchCount == 1&&lockTouch){
                lockTouch = false;
                touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            if(Input.touchCount == 2){
                lockTouch = true;
                touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }else if(Input.GetMouseButton(0)){
                if(canzoom&&touchStart!=Vector3.zero){
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
                }
            }else touchStart = Vector3.zero;
            zoom(Input.GetAxis("Mouse ScrollWheel")*Camera.main.orthographicSize);
        }

        if(transform.position.x>maxsize){
            transform.position = new Vector3(maxsize, transform.position.y, transform.position.z);
        }
        if(transform.position.x<-maxsize){
            transform.position = new Vector3(-maxsize, transform.position.y, transform.position.z);
        }
        if(transform.position.y>maxsize){
            transform.position = new Vector3(transform.position.x, maxsize, transform.position.z);
        }
        if(transform.position.y<-maxsize){
            transform.position = new Vector3(transform.position.x, -maxsize, transform.position.z);
        }
	}

    void zoom(float increment){
        if(canzoom&&enabled){
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }

    public void ZoomToggle(){
        if(!canzoom){
            canzoom = true;
        }else canzoom = false;
    }

    public void EnableToggle(){
        if(!enabled){
            enabled = true;
        }else enabled = false;
    }
}
