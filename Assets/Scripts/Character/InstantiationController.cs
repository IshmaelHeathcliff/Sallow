using System.Collections.Generic;
using UnityEngine;

public abstract class InstantiationController : MonoBehaviour
{
    public GameObject[] toInstantiate;
    protected Dictionary<string, int> _toInstantiateIndex = new Dictionary<string, int>();

    protected virtual void Awake()
    {
        for(var i=0; i < toInstantiate.Length; i++)
        {
            _toInstantiateIndex.Add(toInstantiate[i].ToString().Split(' ')[0], i);
        }
    }

    public abstract void InstantiateGameObject(string objectName);
}