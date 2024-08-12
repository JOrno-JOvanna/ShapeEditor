using System.Collections.Generic;
using UnityEngine;

public class Prism : ShapeClass
{
    public float Height { get; set; }
    public int Sides { get; set; }
    public float Radius { get; set; }

    public override Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //÷ентрирование фигуры
        float halfHeight = Height / 2f;

        // «адание оснований
        for (int i = 0; i < Sides; i++)
        {
            float angle = i * Mathf.PI * 2f / Sides;
            float x = Mathf.Cos(angle) * Radius;
            float z = Mathf.Sin(angle) * Radius;
            vertices.Add(new Vector3(x, -halfHeight, z)); // окружность дл€ низа
            vertices.Add(new Vector3(x, halfHeight, z)); // окружность дл€ верха
        }

        // «адание треугольников дл€ правильного отображени€ нормалей оснований
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

        // «адание треугольников дл€ правильного отображени€ нормалей боковых сторон
        for (int i = 1; i < Sides - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i * 2);
            triangles.Add((i + 1) * 2);

            triangles.Add(1);
            triangles.Add((i + 1) * 2 + 1);
            triangles.Add(i * 2 + 1);
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
