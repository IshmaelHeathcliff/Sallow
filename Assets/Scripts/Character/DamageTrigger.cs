using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageTrigger : MonoBehaviour, IDataPersistable
{
    [SerializeField] float offset = 0.2f;
    [SerializeField] Vector2 size = new Vector2(0.1f, 0.1f);
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask damageLayer = LayerMask.GetMask();
    [SerializeField] DataInfo dataInfo;

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public DataInfo DataInfo 
    { 
        get => dataInfo;
        set => dataInfo = value; 
    }

    [Serializable] public class DamageHitEvent : UnityEvent<DamageTrigger, DamageBearer>
    {
    }

    [Serializable] public class DamageNotHitEvent : UnityEvent<DamageTrigger>
    {
    }

    public DamageHitEvent onDamageHit;
    public DamageNotHitEvent onDamageNotHit;

    bool _canDamage;
    Collider2D[] _attackOverlapResults = new Collider2D[10];
    
    
    public Vector2 FaceDirection { get; set; }

    void Start()
    {
        PersistentDataManager.Instance.Register(this);
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
        
        Vector2 centerPosition = (Vector2)transform.position + FaceDirection * offset;
    
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

    public Data SaveData()
    {
        return new Data<int>(Damage);
    }

    public void LoadData(Data data)
    {
        Damage = ((Data<int>) data).Data1;
    }
}
