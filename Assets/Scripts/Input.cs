using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton
{
    public KeyCode Key;
    public bool Down { get; set; }
    public bool Up { get; set; }
    public bool Held { get; set; }
    
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
    }

    public InputButton(KeyCode key)
    {
        Key = key;
    }

    public void Get()
    {
        Down = Input.GetKeyDown(Key);
        Up = Input.GetKeyUp(Key);
        Held = Input.GetKey(Key);
    }
}

public class InputAxis
{
    public float Horizontal;
    public float Vertical;
    public bool Moving;
    
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
    }

    public void Get()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
            if (Mathf.Abs(Horizontal) + Mathf.Abs(Vertical) > 0)
        {
            Moving = true;
        }
        else
        {
            Moving = false;
        }
    }
}
