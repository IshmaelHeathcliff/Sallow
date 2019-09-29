using UnityEngine;

public abstract class InputComponent
{
    public bool Enabled { get; } = true;

    public abstract void Get();
}

// [Serializable]
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

// [Serializable]
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
