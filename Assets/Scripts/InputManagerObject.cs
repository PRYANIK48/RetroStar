using System;
using UnityEngine;

public class InputManagerObject : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        MainInputManager.Dispose();
    }

    private void Awake()
    {
        MainInputManager.Awake();
    }
}