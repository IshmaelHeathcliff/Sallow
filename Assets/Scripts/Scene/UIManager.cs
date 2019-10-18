    
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<UIManager>();
            if (_instance != null) return _instance;
            
            var obj = new GameObject("UIManager");
            _instance = obj.AddComponent<UIManager>();
            return _instance;
        }
    }

    [SerializeField] CanvasGroup loadingCanvas;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn()
    {
        EnableCanvas(loadingCanvas);
    }

    public void FadeOut()
    {
        DisableCanvas(loadingCanvas);
    }

    void EnableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
    }
    
    void DisableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
    }
}