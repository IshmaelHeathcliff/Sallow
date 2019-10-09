using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed;
    
    Rigidbody2D _rigidbody;
    BoxCollider2D _boxCollider;
    DamageTrigger _damageTrigger;
    Animator _animator;
    Vector2 _faceDirection;
    
    static readonly int ArrowBreak = Animator.StringToHash("arrowBreak");

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _damageTrigger = GetComponent<DamageTrigger>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _faceDirection = PlayerCharacter.Instance.FaceDirection;
        _rigidbody.velocity = _faceDirection * arrowSpeed;
        _damageTrigger.FaceDirection = _faceDirection;
        _damageTrigger.EnableDamage();
        SceneLinkedSMB<Arrow>.Initialise(_animator, this);
    }

    public void DisableArrow()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _boxCollider.isTrigger = true;
        _damageTrigger.DisableDamage();
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