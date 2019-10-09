using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerBehaviourInfo : MonoBehaviour
{
    static PlayerBehaviourInfo _instance;

    public static PlayerBehaviourInfo Instance { get; private set; }
    
    [Serializable]
    public class ParameterEvent : UnityEvent<Vector2>
    {}

    // global
    public Vector2 FaceDirection { get; set; }
    public ParameterEvent faceDirectionChanged;

    // PlayerAttack
    [SerializeField] float attackInterval = 0.5f;
    public float AttackInterval
    {
        get => attackInterval;
        set => attackInterval = value;
    }
    
    // PlayerWalk
    [SerializeField] float maxSpeed = 4f;
    [SerializeField] float acceleration = 3f;
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }
    public float Acceleration
    {
        get => acceleration;
        set => acceleration = value;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        FaceDirection = new Vector2(0,-1);
    }
}
