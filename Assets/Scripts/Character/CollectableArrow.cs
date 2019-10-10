
using UnityEngine;

public class CollectableArrow : CollectableItem
{
    protected override void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        AddCollectorTag("Player");
    }
    
    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (!IsCollectable || !CollectorTags.Contains(other.gameObject.tag)) return;
        onCollected.Invoke();
        enabled = false;
    }
    
    protected override void OnDisable()
    {
        Destroy(gameObject);
    }
}