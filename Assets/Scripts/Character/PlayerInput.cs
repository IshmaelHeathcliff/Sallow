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
        MoveController.ReleaseControl();
        Pause.ReleaseControl();
        Confirm.ReleaseControl();
        Attack.ReleaseControl();
        AttackWithWeapon.ReleaseControl();
    }

    public void GainControl()
    {
        MoveController.GainControl();
        Pause.GainControl();
        Confirm.GainControl();
        Attack.GainControl();
        AttackWithWeapon.GainControl();
    }
}

public abstract class InputComponent
{
    public bool Enabled { get; set; } = true;

    public abstract void Get();

    public abstract void ReleaseControl();

    public abstract void GainControl();
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

    public override void ReleaseControl()
    {
        Enabled = false;
    }

    public override void GainControl()
    {
        Enabled = true;
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

    public override void ReleaseControl()
    {
        Enabled = false;
        Moving = false;
    }

    public override void GainControl()
    {
        Enabled = true;
    }
}
