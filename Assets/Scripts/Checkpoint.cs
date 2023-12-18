using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint checkPointInstance { get; private set; }

 
    private void Awake()
    {
        checkPointInstance = this;
    }

    public void OpenCraftingMenu()
    {
        Debug.Log("Open Craft Menu");
    }
}
