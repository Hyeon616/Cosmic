using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DarkenCamera : MonoBehaviour
{
    public Light flashlight;
    private Camera cam;
    private Material spotlightMaterial;
    private PostProcessVolume postProcessVolume;
    private Bloom bloom;

    void Start()
    {
        cam = GetComponent<Camera>();
        spotlightMaterial = new Material(Shader.Find("Custom/SpotlightShader"));
        spotlightMaterial.SetColor("_SpotColor", new Color(1, 1, 1, 1)); // 손전등의 색상

        // 포스트 프로세싱 볼륨 설정
        postProcessVolume = FindObjectOfType<PostProcessVolume>();
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out bloom);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (flashlight != null && flashlight.enabled)
        {
            // 손전등 효과 렌더링
            Graphics.Blit(src, dest, spotlightMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
