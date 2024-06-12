using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMaskController : MonoBehaviour
{
    public Light flashlight;
    public Transform character;
    public float flashlightRange = 10f;

    private Renderer maskRenderer;
    private MaterialPropertyBlock propBlock;

    void Start()
    {
        maskRenderer = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        UpdateMask();
    }

    void UpdateMask()
    {
        if (flashlight.enabled)
        {
            // 손전등 위치와 방향에 따라 마스크의 알파 값을 조정
            float distanceToLight = Vector3.Distance(flashlight.transform.position, character.position);
            float alpha = Mathf.Clamp01((flashlightRange - distanceToLight) / flashlightRange);
            maskRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Alpha", alpha);
            maskRenderer.SetPropertyBlock(propBlock);
        }
        else
        {
            // 손전등이 꺼져 있으면 마스크를 완전히 검게 설정
            maskRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Alpha", 1f);
            maskRenderer.SetPropertyBlock(propBlock);
        }
    }
}
