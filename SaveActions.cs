using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using System;
using System.Threading.Tasks;

public class SaveActions : MonoBehaviour
{
    public SaveCircuit Canvas; 
    public GameObject selectedSave;
    public TMP_InputField text;
    public TextMeshProUGUI size;
    public Transform content;
    #if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void CopyToClipboard(string text);
        [DllImport("__Internal")]
        private static extern string CopyFromClipboard(string callbackName);
    #endif
    public void UpdateActions(){
        text.text = selectedSave.GetComponent<SaveData>().visualName;
        size.text = selectedSave.GetComponent<SaveData>().Save.Split(';').Length + " PARTS";
    }
    #if UNITY_WEBGL
        public void CopySaveText(){
            print("eee");
            TextEditor editor = new TextEditor
            {
                text = selectedSave.GetComponent<SaveData>().Save
            };
            editor.SelectAll();
            editor.Copy();
        }
    #else
        public void CopySaveText(){
            print("eee");
            TextEditor editor = new TextEditor
            {
                text = selectedSave.GetComponent<SaveData>().Save
            };
            editor.SelectAll();
            editor.Copy();
        }

    #endif

    public void DeleteSave(){
        PlayerPrefs.DeleteKey(selectedSave.name);
        Destroy(selectedSave);
        text.text = "";
        StartCoroutine(Format());
    }

    IEnumerator Format(){
        yield return 0;
        content.gameObject.GetComponent<FormatContent>().Format();
    }

    public void LoadSave(){
        Canvas.LoadComponents(selectedSave.GetComponent<SaveData>().Save.Split(char.Parse("|"))[0]);
    }
    #if UNITY_WEBGL
        public void ImportSave(){
            string clipBoard = GUIUtility.systemCopyBuffer;
            Debug.Log(clipBoard);
            Canvas.LoadComponents(clipBoard.Split(char.Parse("|"))[0]);
        }

        public void FinishImportSave(object text){
            string clipBoard = text.ToString();
            Debug.Log(clipBoard);
            Canvas.LoadComponents(clipBoard.Split(char.Parse("|"))[0]);
        }
    #else
        public void ImportSave(){
            string clipBoard = GUIUtility.systemCopyBuffer;
            Debug.Log(clipBoard);
            Canvas.LoadComponents(clipBoard.Split(char.Parse("|"))[0]);
        }
    #endif

    public void ChangeName(){
        string saveData = selectedSave.GetComponent<SaveData>().Save.Split(char.Parse("|"))[0];
        string NewName = text.text;

        PlayerPrefs.DeleteKey(selectedSave.name);
        PlayerPrefs.SetString(selectedSave.name, saveData+"|"+NewName);

        selectedSave.GetComponent<SaveData>().visualName = NewName;
        selectedSave.GetComponent<SaveData>().name.text = NewName;
        selectedSave.GetComponent<SaveData>().Save = saveData+"|"+NewName;
    }
}
