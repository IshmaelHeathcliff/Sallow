using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerBehaviourInfo : MonoBehaviour
{
    static PlayerBehaviourInfo _instance;

    public static PlayerBehaviourInfo Instance { get; private set; }

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
        DontDestroyOnLoad(this);
    }
}
