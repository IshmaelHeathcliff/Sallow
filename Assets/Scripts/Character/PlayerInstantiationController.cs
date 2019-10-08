
using UnityEngine;

public class PlayerInstantiationController : InstantiationController
{
    public float arrowOffset = 0.5f;

    protected override void Awake()
    {
        base.Awake();
    }

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
        Vector3 arrowPosition = transform.position + (Vector3)PlayerCharacter.FaceDirection * arrowOffset;
        float arrowRotation;
        switch (PlayerCharacter.FaceDirection.x * 10 + PlayerCharacter.FaceDirection.y)
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
        Instantiate(toInstantiate[_toInstantiateIndex["arrow"]], arrowPosition, arrowInstanceQuaternion);
    }
}