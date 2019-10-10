
using UnityEngine;

public class PlayerInstantiationController : InstantiationController
{
    public float arrowOffset = 0.5f;
    
    public override void InstantiateGameObject(string objectName)
    {
        switch (objectName)
        {
            case "arrow":
                InstantiateArrow();
                break;
            default:
                break;
        }
    }

    void InstantiateArrow()
    {
        Vector3 faceDirection = PlayerCharacter.Instance.FaceDirection;
        Vector3 arrowPosition = transform.position +  faceDirection * arrowOffset;
        float arrowRotation;
        switch (faceDirection.x * 10 + faceDirection.y)
        {
            case 1f: 
                arrowRotation = 0f;
                break;
            case -1f: 
                arrowRotation = 180f;
                break;
            case 10f:
                arrowRotation = 270f;
                break;
            case -10f:
                arrowRotation = 90f;
                break;
            default:
                arrowRotation = 0f;
                break;
        }

        Quaternion arrowInstanceQuaternion = Quaternion.Euler(new Vector3(0, 0, arrowRotation));
        GameObject arrowInstance = Instantiate(ToInstantiate["arrow"], arrowPosition, arrowInstanceQuaternion);
        arrowInstance.GetComponent<CollectableArrow>().onCollected.AddListener(PlayerCharacter.Instance.CollectArrow);
    }
}