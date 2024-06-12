using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BlackOverlayController : MonoBehaviour
{
    public Light flashlight;
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        UpdateVignette();
    }

    void Update()
    {
        UpdateVignette();
    }

    void UpdateVignette()
    {
        if (flashlight.enabled)
        {
            vignette.intensity.value = 1f;
            vignette.smoothness.value = 1f;
            vignette.roundness.value = 1f;
            vignette.color.value = new Color(0, 0, 0, 0.96f); // 거의 검은색
        }
        else
        {
            vignette.intensity.value = 0f;
        }
    }
}
