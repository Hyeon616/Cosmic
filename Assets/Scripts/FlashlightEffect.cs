using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FlashlightEffect : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Light flashlight;
    private Vignette vignette;

    [Range(0, 1)]
    private float minVignetteIntensity = 0f;
    [Range(0, 1)]
    private float maxVignetteIntensity = 1f;

    void Start()
    {
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            UpdateVignette();
        }
        else
        {
            Debug.LogError("Vignette effect is not found in the PostProcessVolume profile.");
        }
    }

    void Update()
    {
        UpdateVignette();
    }

    void UpdateVignette()
    {
        if (flashlight.enabled)
        {
            float distanceToLight = Vector3.Distance(flashlight.transform.position, transform.position);
            float intensity = Mathf.Lerp(minVignetteIntensity, maxVignetteIntensity, distanceToLight / flashlight.range);
            vignette.intensity.value = intensity;
        }
        else
        {
            vignette.intensity.value = maxVignetteIntensity;
        }
    }
}
