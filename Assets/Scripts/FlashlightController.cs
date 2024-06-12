using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public Transform character;
    public float maxRange = 5f;
    private CreateConeMesh fanMeshScript;
    private int currentAngleIndex = 0;
    private float[] angles = new float[] { 15f, 30f, 45f, 60f, 75f, 90f };
    public Transform fanStartPoint; // ��ä���� ���� ����
    public Renderer coneRenderer; // ������ �޽��� ������
    private Material coneMaterial;
    void Start()
    {
        fanMeshScript = GetComponentInChildren<CreateConeMesh>();
        fanMeshScript.startPoint = fanStartPoint; // ���� ������ ����
        fanMeshScript.character = character; // ĳ���� Ʈ������ ����

        // ������ ���� ����
        flashlight.color = new Color(1f, 0.87f, 0.73f, 0.5f);

        // ������ �޽� ���� ����
        coneMaterial = coneRenderer.material;
        coneMaterial.SetColor("_Color", new Color(1f, 0.87f, 0.73f, 0.5f));
        coneMaterial.SetFloat("_Intensity", 1.0f);

        UpdateFlashlight();
    }

    void Update()
    {
        HandleMouseScroll();
    }

    void HandleMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentAngleIndex = Mathf.Min(currentAngleIndex + 1, angles.Length - 1);
            UpdateFlashlight();
        }
        else if (scroll < 0f)
        {
            currentAngleIndex = Mathf.Max(currentAngleIndex - 1, 0);
            UpdateFlashlight();
        }
    }

    void UpdateFlashlight()
    {
        float newAngle = angles[currentAngleIndex];
        flashlight.spotAngle = newAngle;
        fanMeshScript.UpdateFan(maxRange, newAngle);
    }
}
