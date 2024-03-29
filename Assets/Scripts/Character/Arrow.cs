﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 6f;
    [SerializeField] bool collectable = false;
    
    Rigidbody2D _rigidbody;
    BoxCollider2D _boxCollider;
    DamageTrigger _damageTrigger;
    Animator _animator;
    Vector2 _faceDirection;
    CollectableArrow _collectableArrow;
    
    static readonly int ArrowBreak = Animator.StringToHash("arrowBreak");

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _damageTrigger = GetComponent<DamageTrigger>();
        _animator = GetComponent<Animator>();
        _collectableArrow = GetComponent<CollectableArrow>();
    }

    void Start()
    {
        if (collectable)
        {
            DisableArrow();
            return;
        }
        Fly();
        SceneLinkedSMB<Arrow>.Initialize(_animator, this);
    }

    void Fly()
    {
        _faceDirection = PlayerCharacter.Instance.FaceDirection;
        _rigidbody.velocity = _faceDirection * arrowSpeed;
        _damageTrigger.FaceDirection = _faceDirection;
        _damageTrigger.EnableDamage();
    }

    public void DisableArrow()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _boxCollider.isTrigger = true;
        _damageTrigger.DisableDamage();
        _collectableArrow.EnableCollectable();
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }

    public void Break()
    {
        _animator.SetTrigger(ArrowBreak);
    }
    
}