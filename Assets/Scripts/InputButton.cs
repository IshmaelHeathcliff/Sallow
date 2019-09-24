using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton
{
    public bool Down { get; set; }
    public bool Up { get; set; }
    public bool Held { get; set; }
    
    public KeyCode KeyCode;
    public bool Enabled
    {
        get { return _enabled; }
    }

    private bool _enabled = true;

    public InputButton(KeyCode key)
    {
        KeyCode = key;
    }
}
