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
    public InputAxis moveController = new InputAxis();
    public InputButton pause = new InputButton(KeyCode.Escape);
    public InputButton confirm = new InputButton(KeyCode.Return);
    public InputButton attack = new InputButton(KeyCode.A);
    public InputButton attackWithWeapon = new InputButton(KeyCode.S);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        moveController.Get();
        pause.Get();
        confirm.Get();
        attack.Get();
        attackWithWeapon.Get();
    }
}
