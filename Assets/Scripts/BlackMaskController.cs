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
            // ������ ��ġ�� ���⿡ ���� ����ũ�� ���� ���� ����
            float distanceToLight = Vector3.Distance(flashlight.transform.position, character.position);
            float alpha = Mathf.Clamp01((flashlightRange - distanceToLight) / flashlightRange);
            maskRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Alpha", alpha);
            maskRenderer.SetPropertyBlock(propBlock);
        }
        else
        {
            // �������� ���� ������ ����ũ�� ������ �˰� ����
            maskRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_Alpha", 1f);
            maskRenderer.SetPropertyBlock(propBlock);
        }
    }
}
