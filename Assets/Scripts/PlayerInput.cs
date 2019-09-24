using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance;
    public static PlayerInput Instance { get; private set; }
    public InputButton leftArrow = new InputButton(KeyCode.LeftArrow);
    public InputButton rightArrow = new InputButton(KeyCode.RightArrow);
    public InputButton downArrow = new InputButton(KeyCode.DownArrow);
    public InputButton upArrow = new InputButton(KeyCode.UpArrow);
    public InputButton pause = new InputButton(KeyCode.Escape);
    public InputButton confirm = new InputButton(KeyCode.Return);
    public InputButton attack = new InputButton(KeyCode.A);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
