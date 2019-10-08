using System;
using UnityEngine;
using UnityEngine.Events;

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
    Collider2D[] _attackOverlapResults = new Collider2D[10];

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

        Vector2 faceDirection = PlayerCharacter.FaceDirection;
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
