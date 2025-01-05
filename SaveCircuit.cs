using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCircuit : MonoBehaviour
{
    public List<Transform> components;
    public string componentData, wireData, circuitData;
    
    public GameObject wire, and_gate, or_gate, not_gate, splitter_gate, switch_gate, light_gate;

    void ClearData(){
        circuitData = "";
        wireData = "";
        componentData = "";
        components.Clear();
    }
    public string SaveComponents(){
        ClearData();

        foreach(Transform child in transform){
            if(TagToNumber(child.gameObject.tag)!=-1){
                components.Add(child);
            }
        }
        int count = 0;
        foreach(Transform child in components){
            ObjectToData(child, count);
            count++;
        }
        circuitData=componentData+wireData+"|";
        return circuitData;
    }

    void ObjectToData(Transform component, int count){
        string EncodedComponent = "";

        EncodedComponent+=TagToNumber(component.gameObject.tag);

        EncodedComponent+=":"+count;

        string Position = ((Vector2)component.transform.position).ToString();
        EncodedComponent+=":"+Position.Replace("(", "").Replace(")", "").Replace(" ", "");

        //slider value if switch
        if(TagToNumber(component.gameObject.tag) == 5){
            EncodedComponent+=":"+component.gameObject.GetComponent<Switch>().slider.value+";";
        }else EncodedComponent+=":"+component.gameObject.GetComponent<Logic>().Output.Count+";";

        componentData+=EncodedComponent;

        foreach(Transform child in component){
            if(child.gameObject.tag == "wire"){
                WireToData(child);
            }
        }
    }

    void WireToData(Transform wire){
        GameObject OutputComponent = wire.gameObject.GetComponent<wire>().Output.transform.parent.gameObject;
        GameObject InputComponent = wire.gameObject.GetComponent<wire>().Input.transform.parent.gameObject;
        string OutputNumber = components.IndexOf(OutputComponent.transform).ToString();
        string InputNumber = components.IndexOf(InputComponent.transform).ToString();
        string EncodedWire = "0"+":"+OutputNumber+","+wire.gameObject.GetComponent<wire>().Output.transform.GetSiblingIndex()+":"+InputNumber+","+wire.gameObject.GetComponent<wire>().Input.transform.GetSiblingIndex()+";";
        wireData+=EncodedWire;
    }

    int TagToNumber(string tag){
        if(tag == "and"){
            return 1;
        }
        if(tag == "or"){
            return 2;
        }
        if(tag == "not"){
            return 3;
        }
        if(tag == "splitter"){
            return 4;
        }
        if(tag == "switch"){
            return 5;
        }
        if(tag == "light"){
            return 6;
        }
        return -1;
    }

    public void LoadComponents(string saveData){
        ClearData();

        string[] splitComponents = saveData.Split(char.Parse(";"));

        foreach(string component in splitComponents){
            string[] data = component.Split(char.Parse(":"));
            if(component!=""){
                if(int.Parse(data[0])>0){
                    string[] position = data[2].Split(char.Parse(","));
                    GameObject SpawnedComponent = Instantiate(NumberToGameObject(int.Parse(data[0])), new Vector2(float.Parse(position[0]), float.Parse(position[1])), Quaternion.identity);
                    
                    //ClosestObjectSelector.objects.Add(SpawnedComponent);
                    //ClosestObjectSelector.objects.Add(SpawnedComponent.GetComponent<Logic>().Input1);
                    //ClosestObjectSelector.objects.Add(SpawnedComponent.GetComponent<Logic>().Input2);
                    //foreach(GameObject output in SpawnedComponent.GetComponent<Logic>().Output){
                    //    ClosestObjectSelector.objects.Add(output);
                    //}

                    SpawnedComponent.transform.SetParent(transform);
                    components.Add(SpawnedComponent.transform);

                    if((TagToNumber(SpawnedComponent.tag) == 5) && data.Length>3){
                        SpawnedComponent.gameObject.GetComponent<Switch>().slider.value = float.Parse(data[3]);
                    }else{
                        if(SpawnedComponent.gameObject.GetComponent<DefineOutputs>()){
                            SpawnedComponent.gameObject.GetComponent<DefineOutputs>().outputAmount = (int)float.Parse(data[3])-2;
                            SpawnedComponent.gameObject.GetComponent<DefineOutputs>().Activate();
                        }
                    }

                }else{
                    string[] outputData = data[1].Split(char.Parse(","));
                    string[] inputData = data[2].Split(char.Parse(","));
                    GameObject Output = components[int.Parse(outputData[0])].gameObject;
                    GameObject Input = components[int.Parse(inputData[0])].gameObject;

                    Input.transform.GetChild(int.Parse(inputData[1])).GetComponent<Wireable>().DetectClick();
                    Output.transform.GetChild(int.Parse(outputData[1])).GetComponent<Wireable>().DetectClick();                    
                    
                }
            }
        }
    }

    GameObject NumberToGameObject(int number){
        if(number == 1){
            return and_gate;
        }
        if(number == 2){
            return or_gate;
        }
        if(number == 3){
            return not_gate;
        }
        if(number == 4){
            return splitter_gate;
        }
        if(number == 5){
            return switch_gate;
        }
        if(number == 6){
            return light_gate;
        }
        return null;
    }
    
}
