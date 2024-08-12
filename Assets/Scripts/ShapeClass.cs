using UnityEngine;
// –одительский абстрактный класс дл€ создани€ фигур
public abstract class ShapeClass
{
    public Color Color { get; set; }
    public abstract Mesh GenerateMesh();
    public abstract Vector3 ColliderSize();

    public void ApplyMesh(GameObject gameObject)
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();

        meshFilter.mesh = GenerateMesh();
        meshRenderer.material.color = Color;
        collider.size = ColliderSize();
    }
}
