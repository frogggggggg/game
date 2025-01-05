using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Sprite On, Off;
    public Slider slider;
    public float timer;
    public bool swapValue;

    public void SwitchPower(){
        GameObject Canvas = transform.root.gameObject;
        if(Canvas.GetComponent<Dragobject>().dragged_object!=transform){
            if(gameObject.GetComponent<Image>().sprite == On){
                gameObject.GetComponent<Image>().sprite = Off;
            }else{
                gameObject.GetComponent<Image>().sprite = On;
            }
        }

        if(transform.GetChild(0).gameObject.GetComponent<Wireable>().wire){
            if(gameObject.GetComponent<Image>().sprite == On){
                swapValue = true;
                transform.GetChild(0).gameObject.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(true, 0);
            }else{
                swapValue = false;
                transform.GetChild(0).gameObject.GetComponent<Wireable>().wire.GetComponent<wire>().Activate(false, 0);
            }
        }
    }

    void FixedUpdate(){
        if(slider.value != 0){
            timer -= Time.deltaTime;
            if (timer < 0){
                timer = 1.1f-slider.value/5;
                swapValue = !swapValue;
                SwitchPower();
            }
        }
    }

    public void SliderValueChange(){
        timer = 1-slider.value;
    }

}
