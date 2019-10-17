using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageBearer : MonoBehaviour, IDataPersistable
{
    [SerializeField] int maxHealth = 1;
    [SerializeField] bool invincibleAfterDamage = true;
    [SerializeField] float invincibleTime = 3f;
    [SerializeField] DataInfo dataInfo;
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    public bool InvincibleAfterDamage
    {
        get => invincibleAfterDamage;
        set => invincibleAfterDamage = value;
    }
    public float InvincibleTime
    {
        get => invincibleTime;
        set => invincibleTime = value;
    }
    public int CurrentHealth { get; set; }
    public DataInfo DataInfo
    {
        get => dataInfo;
        set => dataInfo = value;
    }


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
    
    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    void Start()
    {
        PersistentDataManager.Instance.Register(this);
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

    public Data SaveData()
    {
        var data = new Data<int>(CurrentHealth);
        return data;
    }

    public void LoadData(Data data)
    {
        CurrentHealth = ((Data<int>)data).Data1;
    }
}
