using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputComponent
{
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
    }

    public abstract void Get();
}
public class InputButton : InputComponent
{
    public KeyCode Key;
    public bool Down { get; set; }
    public bool Up { get; set; }
    public bool Held { get; set; }

    public InputButton(KeyCode key)
    {
        Key = key;
    }

    public override void Get()
    {
        Down = Input.GetKeyDown(Key);
        Up = Input.GetKeyUp(Key);
        Held = Input.GetKey(Key);
    }
}

public class InputAxis : InputComponent
{
    public float Horizontal;
    public float Vertical;
    public bool Moving;

    public override void Get()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        Moving = Mathf.Abs(Horizontal) + Mathf.Abs(Vertical) > 0;
    }
}
