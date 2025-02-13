using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager instance;

    public Volume volume;
    public CanvasGroup canvas;

    private ColorAdjustments colorGrading;
    private Vignette vignette;

    private bool isFade;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetComponent();
    }

    public void SetComponent()
    {
        volume = FindObjectOfType<Volume>();
        canvas = FindObjectOfType<CanvasGroup>();

        volume.profile.TryGet(out colorGrading);
        volume.profile.TryGet(out vignette);
    }

    public IEnumerator FadeInOut(float second, bool isIn)
    {
        Color targetColor = isIn ? Color.black : Color.white;
        float curTime = 0;
        float t = 0;

        while (t < 1f)
        {
            curTime += Time.fixedDeltaTime;
            t = curTime / second;
            colorGrading.colorFilter.Interp(colorGrading.colorFilter.value, targetColor, t);
            canvas.alpha = Mathf.Lerp(canvas.alpha, Convert.ToInt32(!isIn), t);
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator VignetteInOut(float second, float targetValue)
    {
        float curTime = 0;
        float t = 0;
        while (t < 1f)
        {
            curTime += Time.fixedDeltaTime;
            t = curTime / second;
            vignette.intensity.Interp(vignette.intensity.value, targetValue, t);
            yield return new WaitForFixedUpdate();
        }
    }
}