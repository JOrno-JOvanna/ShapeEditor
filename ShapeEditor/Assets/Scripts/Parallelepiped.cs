using UnityEngine;
// Создание параллелепипеда
public class Parallelepiped : ShapeClass
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public override Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[8];
        int[] triangles = new int[36];

        // Центрирование фигуры
        float halfWidth = Width / 2f;
        float halfHeight = Height / 2f;
        float halfDepth = Depth / 2f;

        // Задание вершин
        vertices[0] = new Vector3(-halfWidth, -halfHeight, -halfDepth);
        vertices[1] = new Vector3(halfWidth, -halfHeight, -halfDepth);
        vertices[2] = new Vector3(halfWidth, halfHeight, -halfDepth);
        vertices[3] = new Vector3(-halfWidth, halfHeight, -halfDepth);
        vertices[4] = new Vector3(-halfWidth, halfHeight, halfDepth);
        vertices[5] = new Vector3(halfWidth, halfHeight, halfDepth);
        vertices[6] = new Vector3(halfWidth, -halfHeight, halfDepth);
        vertices[7] = new Vector3(-halfWidth, -halfHeight, halfDepth);

        // Задание треугольников для верного отображения нормалей
        triangles = new int[]
        {
            0, 2, 1, 0, 3, 2,
            2, 3, 4, 2, 4, 5,
            1, 2, 5, 1, 5, 6,
            0, 7, 4, 0, 4, 3,
            5, 4, 7, 5, 7, 6,
            0, 6, 7, 0, 1, 6 
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    public override Vector3 ColliderSize()
    {
        return new Vector3(Width, Height, Depth);
    }
}