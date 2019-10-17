
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionDeparture : MonoBehaviour
{
    [SerializeField] string nextSceneName = "";
    [SerializeField] TransitionDestination.TransitionDestinationTag destinationTag = TransitionDestination.TransitionDestinationTag.A;
    [SerializeField] GameObject gameObjectToBeTransitioned = null;

    public string NextSceneName => nextSceneName;
    public TransitionDestination.TransitionDestinationTag DestinationTag => destinationTag;
    public GameObject GameObjectToBeTransitioned => gameObjectToBeTransitioned;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == gameObjectToBeTransitioned)
        {
            SceneController.Instance.StartTransition(this);
        }
    }
}