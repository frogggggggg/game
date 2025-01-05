using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefineOutputs : MonoBehaviour
{
    public GameObject outputObject;
    public int outputAmount;
    public List<GameObject> outputList = new List<GameObject>();
    public void Activate(){
        Start();
    }
    void Start()
    {
        outputList.Add(outputObject);
        for(int i = 0; i < outputAmount; i++){
            GameObject newOutput = Instantiate(outputObject);
            newOutput.transform.SetParent(transform);
            outputList.Add(newOutput);
        }
        int count = 0;
        foreach(GameObject output in outputList){
            gameObject.GetComponent<Logic>().Output.Add(output);
            output.transform.localPosition = new Vector3(1, (count+.5f-outputList.Count/2f)*.5f, 0);
            count++;
        }
        Destroy(this);
    }
}
