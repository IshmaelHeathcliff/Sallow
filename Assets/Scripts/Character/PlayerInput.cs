﻿using UnityEngine;

namespace Character
{
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
}
