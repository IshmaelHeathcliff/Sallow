
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum TransitionDestinationTag
    {
        A, B, C, D
    }

    [SerializeField] TransitionDestinationTag destinationTag = TransitionDestinationTag.A;
    [SerializeField] GameObject gameObjectTransitioned = null;
    
    public TransitionDestinationTag DestinationTag => destinationTag;
    public GameObject GameObjectTransitioned => gameObjectTransitioned;
}