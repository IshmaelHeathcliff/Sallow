using System.Collections.Generic;
using UnityEngine;

public abstract class InstantiationController : MonoBehaviour
{
    [SerializeField] GameObject[] toInstantiate = new GameObject[1];
    protected Dictionary<string, GameObject> ToInstantiate = new Dictionary<string, GameObject>();

    protected virtual void Awake()
    {
        foreach (GameObject o in toInstantiate)
        {
            AddToInstantiate(o);
        }
    }

    public abstract void InstantiateGameObject(string objectName);

    public void AddToInstantiate(GameObject instance)
    {
        ToInstantiate.Add(instance.ToString().Split(' ')[0], instance);
    }
}