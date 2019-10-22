    
using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum FadeType
    {
        Sallow, Loading, GameOver
    }
    
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

    [SerializeField] CanvasGroup loadingCanvas = new CanvasGroup();
    [SerializeField] CanvasGroup sallowCanvas = new CanvasGroup();
    [SerializeField] CanvasGroup gameOverCanvas = new CanvasGroup();
    [SerializeField] float fadingTime = 0.2f;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeSceneOut(FadeType fadeType)
    {
        CanvasGroup canvasGroup;
        switch (fadeType)
        {
            case FadeType.GameOver:
                canvasGroup = gameOverCanvas;
                break;
            case FadeType.Loading:
                canvasGroup = loadingCanvas;
                break;
            case FadeType.Sallow:
                canvasGroup = sallowCanvas;
                break;
            default:
                canvasGroup = sallowCanvas;
                break;
        }
        
        yield return StartCoroutine(Fading(1f, canvasGroup));
    }
    
    public IEnumerator FadeSceneIn()
    {
        CanvasGroup canvasGroup;
        if (loadingCanvas.alpha > 0.1f)
        {
            canvasGroup = loadingCanvas;
        }
        else if (gameOverCanvas.alpha > 0.1f)
        {
            canvasGroup = gameOverCanvas;
        }
        else
        {
            canvasGroup = sallowCanvas;
        }

        yield return StartCoroutine(Fading(0f, canvasGroup));
    }

    IEnumerator Fading(float targetAlpha, CanvasGroup canvasGroup)
    {
        float fadingSpeed = Mathf.Abs(targetAlpha - canvasGroup.alpha) / fadingTime;
        while(!Mathf.Approximately(targetAlpha, canvasGroup.alpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, 
                fadingSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }
}