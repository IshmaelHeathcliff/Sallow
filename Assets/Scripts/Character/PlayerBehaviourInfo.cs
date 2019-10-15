using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerBehaviourInfo : MonoBehaviour, IDataPersistable
{
    public static PlayerBehaviourInfo Instance { get; private set; }

    // global
    
    // PlayerAttack
    [SerializeField] float attackInterval = 0.5f;

    // PlayerAttackWithWeapon
    [SerializeField] int maxArrowCount = 5;

    // PlayerWalk
    [SerializeField] float maxSpeed = 4f;
    [SerializeField] float acceleration = 3f;

    // int Data
    public int MaxArrowCount
    {
        get => maxArrowCount;
        set => maxArrowCount = value;
    }

    // float Data
    public float AttackInterval
    {
        get => attackInterval;
        set => attackInterval = value;
    }

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

    public void LoadData(Data data)
    {
        List<int> intData = ((Data<List<int>, List<float>>) data).Data1;
        List<float> floatData = ((Data<List<int>, List<float>>) data).Data2;
        for (int i = 0; i < intData.Count; i++)
        {
            MaxArrowCount = intData[i];
        }
    }

    public Data SaveData()
    {
        var intData = new List<int>{maxArrowCount};
        var floatData = new List<float>{AttackInterval, MaxSpeed, Acceleration};

        return new Data<List<int>, List<float>> (intData, floatData);

    }
}
