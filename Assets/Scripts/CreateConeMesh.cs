using UnityEngine;

public class CreateConeMesh : MonoBehaviour
{
    public float radius = 5f; // ��ä���� ������
    public int segments = 18; // ��ä���� ���׸�Ʈ ��
    public float angle = 45f; // ��ä���� ����
    public Transform startPoint; // ��ä���� ���� ����
    public Transform character; // ĳ���� Ʈ������

    private Mesh mesh;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;

        GenerateFan();
    }

    public void UpdateFan(float newRadius, float newAngle)
    {
        radius = newRadius;
        angle = newAngle;
        GenerateFan();
    }

    private void GenerateFan()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
        else
        {
            mesh.Clear();
        }

        float angleStep = angle / segments;
        int verticesCount = segments + 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 3];
        Vector2[] uvs = new Vector2[verticesCount];

        vertices[0] = Vector3.zero;
        uvs[0] = new Vector2(0.5f, 0.5f);

        for (int i = 0; i < segments + 1; i++)
        {
            float currentAngle = -angle / 2 + angleStep * i;
            float rad = Mathf.Deg2Rad * currentAngle;
            Vector3 vertex = new Vector3(Mathf.Cos(rad) * radius, 0f, Mathf.Sin(rad) * radius);
            vertices[i + 1] = vertex;
            uvs[i + 1] = new Vector2(vertices[i + 1].x / radius / 2f + 0.5f, vertices[i + 1].z / radius / 2f + 0.5f);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        
    }

    private void Update()
    {
        Vector3 characterRotation = character.eulerAngles;

        if (Mathf.Approximately(characterRotation.y, 90f))
        {
            
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation * Quaternion.Euler(90, 0, 90);
            transform.localScale = Vector3.one; // Reset scale
        }
        else if (Mathf.Approximately(characterRotation.y, 270f))
        {
            
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation * Quaternion.Euler(-90, 0, -90);
            transform.localScale = Vector3.one; // Reset scale
        }
    }

}
