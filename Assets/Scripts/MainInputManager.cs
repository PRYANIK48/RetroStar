using System;
using UnityEngine;

public static class MainInputManager
{
    public static readonly MainInputSystem InputSystem = new MainInputSystem();

    static MainInputManager()
    {
        InputSystem.Enable();
    }
    
    public static void Awake() { }
    public static void Dispose()
    {
        InputSystem.Dispose();
    }
}