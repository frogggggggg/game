using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveData : MonoBehaviour
{

    public GameObject SaveActionBar;
    public string visualName;
    public TextMeshProUGUI name;
    public string Save;
    

    void Start(){
        name.text = visualName;
    }

    public void LoadSave(){
        SaveActionBar.GetComponent<SaveActions>().selectedSave = gameObject;
        SaveActionBar.GetComponent<SaveActions>().UpdateActions();
    }
}
