using System.Collections.Generic;
using UnityEngine;

public class Capsule : ShapeClass
{
    public float Height { get; set; }
    public int Sides { get; set; }
    public float Radius { get; set; }

    public override Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Mesh cylinder = CreateCylinder();

        Mesh topsphere = CreateSemiSphere(Radius);
        Mesh bottomsphere = CreateSemiSphere(-Radius);

        // Комбинирование мэшей и выставление их координат
        CombineInstance[] combine = new CombineInstance[3];
        combine[0].mesh = cylinder;
        combine[1].mesh = topsphere;
        combine[2].mesh = bottomsphere;

        combine[0].transform = Matrix4x4.identity;
        combine[1].transform = Matrix4x4.Translate(new Vector3(0, Height / 2, 0));
        combine[2].transform = Matrix4x4.Translate(new Vector3(0, -Height / 2, 0));

        mesh.CombineMeshes(combine);
        mesh.RecalculateNormals();

        return mesh;
    }

    // Создание цилиндра по аналогии кода создания призмы
    private Mesh CreateCylinder()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        float halfHeight = Height / 2f;

        for (int i = 0; i < Sides; i++)
        {
            float angle = i * Mathf.PI * 2f / Sides;
            float x = Mathf.Cos(angle) * Radius;
            float z = Mathf.Sin(angle) * Radius;
            vertices.Add(new Vector3(x, -halfHeight, z));
            vertices.Add(new Vector3(x, halfHeight, z)); 
        }

        for (int i = 0; i < Sides; i++)
        {
            int next = (i + 1) % Sides;
            triangles.Add(i * 2);
            triangles.Add(i * 2 + 1);
            triangles.Add(next * 2 + 1);

            triangles.Add(i * 2);
            triangles.Add(next * 2 + 1);
            triangles.Add(next * 2);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    // Создание полусферы по аналогии кода создания сферы
    private Mesh CreateSemiSphere(float radius)
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Переменная для верного выставления высот полукруглых частей капсулы
        float rad = Height / 2;

        if (radius > 0)
        {
            // Строим полукруг снизу вверх
            for (int i = 0; i <= Sides; i++)
            {
                float theta = i * Mathf.PI / Sides;
                float sinTheta = Mathf.Sin(theta);
                float cosTheta = Mathf.Cos(theta);

                for (int j = 0; j <= Sides; j++)
                {
                    float phi = j * 2 * Mathf.PI / Sides;
                    float sinPhi = Mathf.Sin(phi);
                    float cosPhi = Mathf.Cos(phi);

                    Vector3 vertex = new Vector3(radius * cosTheta * cosPhi, rad * sinTheta, radius * cosTheta * sinPhi);
                    vertices.Add(vertex);
                }
            }
        }
        else
        {
            // Строим полукруг сверху вних
            for (int i = 0; i >= -Sides; i--)
            {
                float theta = i * Mathf.PI / Sides;
                float sinTheta = Mathf.Sin(theta);
                float cosTheta = Mathf.Cos(theta);

                for (int j = 0; j >= -Sides; j--)
                {
                    float phi = j * 2 * Mathf.PI / Sides;
                    float sinPhi = Mathf.Sin(phi);
                    float cosPhi = Mathf.Cos(phi);

                    Vector3 vertex = new Vector3(radius * cosTheta * cosPhi, rad * sinTheta, radius * cosTheta * sinPhi);
                    vertices.Add(vertex);
                }
            }
        }

        for (int i = 0; i < Sides; i++)
        {
            for (int j = 0; j < Sides; j++)
            {
                int first = (i * (Sides + 1)) + j;
                int second = first + Sides + 1;

                triangles.Add(first);
                triangles.Add(first + 1);
                triangles.Add(second);

                triangles.Add(second);
                triangles.Add(first + 1);
                triangles.Add(second + 1);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    public override Vector3 ColliderSize()
    {
        return new Vector3(2 * Radius, Height, 2 * Radius);
    }
}