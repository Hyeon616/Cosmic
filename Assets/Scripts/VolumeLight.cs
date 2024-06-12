using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeLight : MonoBehaviour
{
    public Light spotlight;
    private Material volumeLightMaterial;

    void Start()
    {
        volumeLightMaterial = new Material(Shader.Find("Custom/VolumeLightShader"));
        volumeLightMaterial.SetColor("_LightColor", spotlight.color);
        volumeLightMaterial.SetFloat("_LightIntensity", spotlight.intensity);
    }

    void OnRenderObject()
    {
        if (spotlight != null && spotlight.enabled)
        {
            volumeLightMaterial.SetPass(0);

            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);
            GL.Color(new Color(spotlight.color.r, spotlight.color.g, spotlight.color.b, 0.5f));
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(0, 1, 0);
            GL.End();
            GL.PopMatrix();
        }
    }
}
