using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageBearer : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] bool invincibleAfterDamage = true;
    [SerializeField] float invincibleTime = 3f;
    public int MaxHealth => maxHealth;
    public bool InvincibleAfterDamage => invincibleAfterDamage;
    public float InvincibleTime => invincibleTime;
    
    [Serializable] public class DamageEvent : UnityEvent<DamageTrigger, DamageBearer>
    {}
    [Serializable] public class HealEvent : UnityEvent<int, DamageBearer>
    {}
    [Serializable] public class HealthChangeEvent : UnityEvent<DamageBearer>
    {}

    public HealthChangeEvent onHealthChange;
    public DamageEvent onTakeDamage;
    public DamageEvent onDie;
    public HealEvent onHeal;

    public int CurrentHealth { get; set; }

    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(DamageTrigger damageTrigger)
    {
        CurrentHealth -= damageTrigger.Damage;
        onTakeDamage.Invoke(damageTrigger, this);
        onHealthChange.Invoke(this);
        if (CurrentHealth <= 0)
        {
            onDie.Invoke(damageTrigger, this);
        }
    }

    public void TakeHealing(int healing)
    {
        CurrentHealth += healing;
        onHeal.Invoke(healing, this);
        onHealthChange.Invoke(this);
    }
}
