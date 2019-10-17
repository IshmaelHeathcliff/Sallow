
using System;
using UnityEngine;

public class TransitionDeparture : MonoBehaviour
{
    [SerializeField] string nextSceneName = "";
    [SerializeField] TransitionDestination.TransitionDestinationTag destinationTag = TransitionDestination.TransitionDestinationTag.A;

    public string NextSceneName => nextSceneName;
    public TransitionDestination.TransitionDestinationTag DestinationTag => destinationTag;

    public GameObject gameObjectToBeTransitioned;
    void OnTriggerEnter2D(Collider2D other)
    {
        throw new NotImplementedException();
    }
}