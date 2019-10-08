using System.Collections.Generic;
using UnityEngine;

public abstract class InstantiationController : MonoBehaviour
{
    public GameObject[] toInstantiate;
    protected Dictionary<string, GameObject> _toInstantiate = new Dictionary<string, GameObject>();

    protected virtual void Awake()
    {
        foreach (GameObject o in toInstantiate)
        {
            _toInstantiate.Add(o.ToString().Split(' ')[0], o);
        }
    }

    public abstract void InstantiateGameObject(string objectName);
}