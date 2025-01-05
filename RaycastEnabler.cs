using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ClosestObjectSelector : MonoBehaviour
{
    [SerializeField]
    public Transform parentObject;
    public static List<GameObject> objects = new List<GameObject>(); // List of objects with Image components
    public static List<Vector2> positions = new List<Vector2>(); // Thread-safe copy of positions

    public int closestIndex = -1;
    public int lastClosestIndex = -1;
    private Vector3 mouseWorldPosition;
    private bool isRunning = true;

    private Thread calculationThread;

    void Start()
    {
        calculationThread = new Thread(CalculateClosestObject);
        calculationThread.Start();
    }

    void Update()
    {
        // Update mouse position in world space
        Vector2 screenPosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // Update positions on the main thread

        // Update the raycastTarget state for the closest object
        if (closestIndex != lastClosestIndex)
        {
            if(lastClosestIndex>=objects.Count){
                lastClosestIndex = -1;
            }

            if(closestIndex>=objects.Count){
                closestIndex = -1;
            }
            if (lastClosestIndex != -1 && objects[lastClosestIndex].TryGetComponent(out Image lastImage))
            {
                lastImage.enabled = false;
            }

            if (closestIndex != -1 && objects[closestIndex].TryGetComponent(out Image currentImage))
            {
                currentImage.enabled = true;
            }

            lastClosestIndex = closestIndex;
        }
    }

    void UpdatePositions()
    {
        positions.Clear();
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                positions.Add(obj.transform.position);
            }
        }
    }

    void CalculateClosestObject()
    {
        while (isRunning)
        {
            try
            {
                float closestDistance = float.MaxValue;
                int newClosestIndex = -1;

                for (int i = 0; i < positions.Count; i++)
                {
                    Vector2 objectPosition = positions[i];
                    float distance = Vector2.Distance(mouseWorldPosition, objectPosition);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        newClosestIndex = i;
                    }
                }

                closestIndex = newClosestIndex;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                // Handle case where list size changes during the loop
                closestIndex = -1;
            }

            // Sleep briefly to avoid overloading the CPU
            Thread.Sleep(10);
        }
    }

    void OnDestroy()
    {
        // Stop the thread when the object is destroyed
        isRunning = false;

        if (calculationThread != null && calculationThread.IsAlive)
        {
            calculationThread.Join();
        }
    }
}
