using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class DamageTrigger : MonoBehaviour
{
    public float offset;
    public Vector2 size;
    public int damage = 1;
    public LayerMask damageLayer;

    [Serializable]
    public class DamageHitEvent : UnityEvent<DamageTrigger, DamageBearer>
    {
    }

    [Serializable]
    public class DamageNotHitEvent : UnityEvent<DamageTrigger>
    {
    }

    public DamageHitEvent onDamageHit;
    public DamageNotHitEvent onDamageNotHit;


    bool _canDamage;
    Animator _animator;
    Collider2D[] _attackOverlapResults = new Collider2D[10];

    static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnableDamage()
    {
        _canDamage = true;
    }

    public void DisableDamage()
    {
        _canDamage = false;
    }

    void FixedUpdate()
    {
        if (!_canDamage) return;
        
        float horizontal = _animator.GetFloat(FaceDirectionX);
        float vertical = _animator.GetFloat(FaceDirectionY);
        var faceDirection = new Vector2(horizontal, vertical);
        Vector2 centerPosition = (Vector2)transform.position + faceDirection * offset;
        
        _attackOverlapResults = Physics2D.OverlapBoxAll(centerPosition, size, 0f, damageLayer);
        if (_attackOverlapResults.Length == 0) return;
        foreach (Collider2D hit in _attackOverlapResults)
        {
            var damageBearer = hit.GetComponent<DamageBearer>();
            if (damageBearer)
            {
                onDamageHit.Invoke(this, damageBearer);
                damageBearer.TakeDamage(this);
            }
            else
            {
                onDamageNotHit.Invoke(this);
            }
        }
    }
}
