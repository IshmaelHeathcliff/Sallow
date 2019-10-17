using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public InputAxis MoveController { get; } = new InputAxis();
    public InputButton Pause { get; } = new InputButton(KeyCode.Escape);
    public InputButton Confirm { get; } = new InputButton(KeyCode.Return);
    public InputButton Attack { get; } = new InputButton(KeyCode.Z);
    public InputButton AttackWithWeapon { get; } = new InputButton(KeyCode.X);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        MoveController.Get();
        Pause.Get();
        Confirm.Get();
        Attack.Get();
        AttackWithWeapon.Get();
    }

    public void ReleaseControl()
    {
        MoveController.Enabled = false;
        Pause.Enabled = false;
        Confirm.Enabled = false;
        Attack.Enabled = false;
        AttackWithWeapon.Enabled = false;
    }

    public void GainControl()
    {
        MoveController.Enabled = true;
        Pause.Enabled = true;
        Confirm.Enabled = true;
        Attack.Enabled = true;
        AttackWithWeapon.Enabled = true;
    }
}

public abstract class InputComponent
{
    public bool Enabled { get; set; } = true;

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
        if (!Enabled)
        {
            Down = false;
            Up = false;
            Held = false;
            return;
        }
        
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
        if (!Enabled)
        {
            Horizontal = 0f;
            Vertical = 0f;
            return;
        }
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        Moving = Mathf.Abs(Horizontal) + Mathf.Abs(Vertical) > 0;
    }
}
