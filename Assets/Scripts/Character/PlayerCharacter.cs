﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }
    
    Animator _animator;
    DamageTrigger _attackDamageTrigger;
    CharacterController2D _characterController;
    PlayerBehaviourController _behaviourController;
    PlayerInstantiationController _instantiationController;
    
    [Serializable]
    public class ParameterEvent : UnityEvent<Vector2>
    {}
    public Vector2 FaceDirection { get; set; }
    public ParameterEvent faceDirectionChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
        _attackDamageTrigger = GetComponent<DamageTrigger>();
        _behaviourController = GetComponent<PlayerBehaviourController>();
        _instantiationController = GetComponent<PlayerInstantiationController>();

    }

    void Start()
    {
        SceneLinkedSMB<PlayerCharacter>.Initialise(_animator, this);
    }

    public void EnableAttack()
    {
        StartCoroutine(AttackProcess());

    }

    public void DisableAttack()
    {
        _attackDamageTrigger.DisableDamage();
        
    }

    public void EnableMove()
    {
        _characterController.EnableMove();
    }

    public void DisableMove()
    {
        _characterController.DisableMove();
    }

    public void Instantiate(string objectName)
    {
        _instantiationController.InstantiateGameObject(objectName);
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(0.2f);
        _attackDamageTrigger.EnableDamage();
    }
}
