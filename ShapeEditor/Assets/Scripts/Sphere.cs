using System.Collections.Generic;
using UnityEngine;

public class Sphere : ShapeClass
{
    public float Radius { get; set; }
    public int Sectors { get; set; }

    public override Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Задание вершин для образования двух полукругов, которые складываются в одну сферу
        for (int i = 0; i <= Sectors; i++)
        {
            // Долгота, проходим по вершинам от одного полюса к другому
            float theta = i * Mathf.PI / Sectors;
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int j = 0; j <= Sectors; j++)
            {
                // Широта, проходим по вершинам вдоль экватора сферы
                float phi = j * 2 * Mathf.PI / Sectors;
                float sinPhi = Mathf.Sin(phi);
                float cosPhi = Mathf.Cos(phi);

                Vector3 vertex = new Vector3(Radius * sinTheta * cosPhi, Radius * cosTheta, Radius * sinTheta * sinPhi);
                vertices.Add(vertex);
            }
        }

        // Задание треугольников для правильного отображения нормалей
        for (int i = 0; i < Sectors; i++)
        {
            for (int j = 0; j < Sectors; j++)
            {
                int first = (i * (Sectors + 1)) + j;
                int second = first + Sectors + 1;

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
        return new Vector3(2 * Radius, 2 * Radius, 2 * Radius);
    }
}
