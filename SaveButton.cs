using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject saveObject;
    public Transform content;
    public GameObject SaveActionBar;

    void Start(){
        for(int i = 0; i<300; i++){
            if(PlayerPrefs.HasKey("Save"+i)){
                string name = "Save"+i;
                string visualName = PlayerPrefs.GetString(name).Split(char.Parse("|"))[1];
                CreateSaveObject(PlayerPrefs.GetString(name), name, visualName);
            }
        }
    }
    public void GetSaveCode(){
        string saveCode = Canvas.GetComponent<SaveCircuit>().SaveComponents();
        
        string name = "";
        int count = 0;
        while(true){
            if(!PlayerPrefs.HasKey("Save"+count)){
                name = "Save"+count;
                saveCode = saveCode+name;
                PlayerPrefs.SetString(name, saveCode);
                break;
            }
            count++;
        }
        
        CreateSaveObject(saveCode, name, name);
    }

    void CreateSaveObject(string saveCode, string name, string visualName){
        GameObject SpawnedSaveObject = Instantiate(saveObject, content);
        SpawnedSaveObject.GetComponent<SaveData>().SaveActionBar = SaveActionBar;
        SpawnedSaveObject.GetComponent<SaveData>().Save = saveCode;
        SpawnedSaveObject.name = name;
        SpawnedSaveObject.GetComponent<SaveData>().visualName = visualName;

        content.gameObject.GetComponent<FormatContent>().Format();
    }
}
