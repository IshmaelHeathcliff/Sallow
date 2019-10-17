
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

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    IEnumerator Transition(TransitionDeparture transitionDeparture)
    {
        PlayerInput.Instance.ReleaseControl();
        PersistentDataManager.Instance.SaveAllData();
        PersistentDataManager.Instance.ClearPersistables();
        yield return SceneManager.LoadSceneAsync(transitionDeparture.NextSceneName);
        PersistentDataManager.Instance.LoadAllData();
        PersistentDataManager.Instance.ClearData();
    }

    void GetDestination(TransitionDestination.TransitionDestinationTag destinationTag)
    {}


}