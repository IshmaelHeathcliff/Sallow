using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class CollectableItem : MonoBehaviour
{
    protected List<string> CollectorTags = new List<string>();
    
    protected Collider2D Collider;
    
    protected bool IsCollectable;
    
    [Serializable]
    public class CollectedEvent : UnityEvent
    {}

    public CollectedEvent onCollected;

    public void AddCollectorTag(string collectorTag)
    {
        CollectorTags.Add(collectorTag);
    }
    
    public void RemoveCollectorTag(string collectorTag)
    {
        CollectorTags.Remove(collectorTag);
    }
    
    public void EnableCollectable()
    {
        IsCollectable = true;
    }

    public void DisableCollectable()
    {
        IsCollectable = false;
    }

    protected abstract void Awake();
    
    protected abstract void OnDisable();

    protected abstract void OnTriggerStay2D(Collider2D other);
}