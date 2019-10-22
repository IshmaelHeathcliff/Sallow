
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController _instance;

    public static SceneController Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<SceneController>();

            if (_instance != null) return _instance;
            var obj = new GameObject("SceneController");
            _instance = obj.AddComponent<SceneController>();

            return _instance;
        }
    }


    Scene _currentScene;
    // TransitionDestination.TransitionDestinationTag _restartDestinationTag = TransitionDestination.TransitionDestinationTag.A;
    public bool Transitioning { get; private set; }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _currentScene = SceneManager.GetActiveScene();
        // _restartDestinationTag = TransitionDestination.TransitionDestinationTag.A;
    }

    public void StartTransition(TransitionDeparture transitionDeparture)
    {
        StartCoroutine(Transition(transitionDeparture));
    }
    IEnumerator Transition(TransitionDeparture transitionDeparture)
    {
        Transitioning = true;
        
        PlayerInput.Instance.ReleaseControl();
        yield return UIManager.Instance.FadeSceneOut(UIManager.FadeType.Sallow);
        PersistentDataManager.Instance.SaveAllData();
        PersistentDataManager.Instance.ClearPersistables();
        yield return SceneManager.LoadSceneAsync(transitionDeparture.NextSceneName);
        PersistentDataManager.Instance.LoadAllData();
        TransitionDestination destination = GetDestination(transitionDeparture.DestinationTag);
        SetDestinationGameObjectPosition(destination);
        yield return UIManager.Instance.FadeSceneIn();
        PlayerInput.Instance.GainControl();

        Transitioning = false;

    }

    TransitionDestination GetDestination(TransitionDestination.TransitionDestinationTag destinationTag)
    {
        TransitionDestination[] destinations = FindObjectsOfType<TransitionDestination>();
        foreach (TransitionDestination destination in destinations)
        {
            if (destination.DestinationTag == destinationTag)
            {
                return destination;
            }
        }
        
        Debug.LogWarning("Scene transition destination not found.");
        return null;
    }

    void SetDestinationGameObjectPosition(TransitionDestination destination)
    {
        if (destination == null)
        {
            Debug.LogWarning("Destination position not set.");
            return;
        }
        
        Transform gameObjectTransform = destination.GameObjectTransitioned.transform;
        Transform destinationTransform = destination.transform;
        gameObjectTransform.position = destinationTransform.position;
        gameObjectTransform.rotation = destinationTransform.rotation;
    }

}