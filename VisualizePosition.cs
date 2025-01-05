using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualizePosition : MonoBehaviour
{
    public Transform location;
    public TextMeshProUGUI text;
    void FixedUpdate()
    {
        text.text = Vector2Int.RoundToInt(location.position).ToString();
    }
}
