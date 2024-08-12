using System;
using UnityEngine;
using UnityEngine.UI;
// Интерфейс для реализации логики создания фигуры
public interface IShapeFactory
{
    ShapeClass CreateShape(int type, Transform panel);
}
// Класс реализации паттерна Factory
public class ShapeFactory : IShapeFactory
{
    public ShapeClass CreateShape(int type, Transform panel)
    {
        ShapeClass shape = null;

        switch (type)
        {
            case 0:
                shape = new Parallelepiped
                {
                    Width = panel.GetChild(0).GetChild(0).GetComponent<Slider>().value,
                    Height = panel.GetChild(0).GetChild(1).GetComponent<Slider>().value,
                    Depth = panel.GetChild(0).GetChild(2).GetComponent<Slider>().value
                };

                break;
            case 1:
                shape = new Sphere
                {
                    Radius = panel.GetChild(1).GetChild(0).GetComponent<Slider>().value,
                    Sectors = Mathf.RoundToInt(panel.GetChild(1).GetChild(1).GetComponent<Slider>().value)
                };
                break;
            case 2:
                shape = new Prism
                {
                    Height = panel.GetChild(2).GetChild(0).GetComponent<Slider>().value,
                    Sides = Mathf.RoundToInt(panel.GetChild(2).GetChild(1).GetComponent<Slider>().value),
                    Radius = panel.GetChild(2).GetChild(2).GetComponent<Slider>().value
                };
                break;
            case 3:
                shape = new Capsule
                {
                    Height = panel.GetChild(3).GetChild(0).GetComponent<Slider>().value,
                    Sides = Mathf.RoundToInt(panel.GetChild(3).GetChild(1).GetComponent<Slider>().value),
                    Radius = panel.GetChild(3).GetChild(2).GetComponent<Slider>().value
                };
                break;
            default:
                throw new ArgumentException($"Неподдерживаемый тип фигуры: {type}");
        }

        return shape;
    }
}
