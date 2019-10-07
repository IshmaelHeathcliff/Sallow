using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    static PlayerInput _instance;
    public static PlayerInput Instance { get; private set; }
    public InputAxis moveController = new InputAxis();
    public InputButton pause = new InputButton(KeyCode.Escape);
    public InputButton confirm = new InputButton(KeyCode.Return);
    public InputButton attack = new InputButton(KeyCode.Z);
    public InputButton attackWithWeapon = new InputButton(KeyCode.X);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        moveController.Get();
        pause.Get();
        confirm.Get();
        attack.Get();
        attackWithWeapon.Get();
    }
}
