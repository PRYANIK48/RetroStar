using System;
using UnityEngine;

public class MainInputManager : MonoBehaviour
{
    public static MainInputSystem InputSystem { private set; get; }

    private void Awake()
    {
        if (InputSystem != null) return;
        
        
        InputSystem = new MainInputSystem();
        InputSystem.Enable();
    }
    public static void Dispose()
    {
        InputSystem.Dispose();
    }

    private void OnApplicationQuit()
    {
        Dispose();
    }
}