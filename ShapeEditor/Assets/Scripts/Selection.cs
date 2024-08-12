using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public Transform panel, dropdown; // panel - ������, �� ������� ���������� �������� ��� ������� ���������� �����; dropdown - ���������� ������ �� ������� �����
    public Slider red, blue, green; // �������� ��� ������� ����� ������
    public Image img; // �������� ��� ������������ ���������� �����

    public Material highlight, select; // ����� ��� ���������� �����

    public float rotationSpeed = 500f;

    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 70f;

    private IShapeFactory shapeFactory;

    private ShapeClass newshape;

    private Material original; // ������������ �������� ��������������� ��� ���������� �������
    private Transform highlighted, selected; // �������������� � ��������� �������
    private RaycastHit hit;


    public void Start()
    {
        shapeFactory = new ShapeFactory();

        ChangeValue();
    }

    public void Update()
    {
        img.color = ColorChange();

        // ���� ������ �� ������ �� �����-���� ������, �� ��������� ��� ������������� �� ����� ���������
        if (highlighted != null)
        {
            highlighted.GetComponent<Renderer>().material = original;
            highlighted = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlighted = hit.transform;

            // ���� �������������� ������ �� �������� ��������� � ����� ���, �� ������������ ���
            if(highlighted.CompareTag("Selectable") && highlighted != selected)
            {
                if(highlighted.GetComponent<MeshRenderer>().material != highlight)
                {
                    original = highlighted.GetComponent<MeshRenderer>().material;
                    highlighted.GetComponent<MeshRenderer>().material = highlight;
                }
            }
            else
            {
                highlighted = null;
            }
        }

        // ���� �������� �� ������ ���� �� �������� �� �����-���� ������, �� ����� ���������
        if(Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (selected != null)
            {
                selected.GetComponent<Renderer>().material = original;
                selected = null;
            }

            // ���� ��� ������� ������� �� ������, �� �� ���������� ���������
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                selected = hit.transform;

                if (selected.CompareTag("Selectable"))
                {
                    if (selected.GetComponent<MeshRenderer>().material != highlight)
                    {
                        selected.GetComponent<MeshRenderer>().material = select;
                    }
                }
                else
                {
                    selected = null;
                }
            }
        }

        // �������� ���������� �������
        if (Input.GetMouseButton(1) && selected != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            selected.transform.Rotate(Vector3.up, -mouseX, Space.World);
            selected.transform.Rotate(Vector3.right, mouseY, Space.World);
        }

        // ��� ������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, Vector3.zero);
            float newDistance = Mathf.Clamp(distance - scrollInput * zoomSpeed, minZoom, maxZoom);
            Camera.main.transform.position = Vector3.zero - Camera.main.transform.forward * newDistance;
        }

        // ����������� ������� �� ��� X
        if (selected != null && Input.GetMouseButton(2))
        {
            float moveX = Input.GetAxis("Mouse X") * Time.deltaTime * 200;
            selected.transform.position += new Vector3(moveX, 0f, 0f);
        }
    }

    // ����� ��� ����� ������������� ���������� �� ������ ���������� �����
    public void ChangeValue()
    {
        for (int i = 0; i < panel.childCount; i++)
        {
            panel.GetChild(i).gameObject.SetActive(false);
        }

        panel.GetChild(dropdown.GetComponent<Dropdown>().value).gameObject.SetActive(true);
    }

    // ����� �������� ������, �������� �� ������ "�������"
    public void CreateShape()
    {
        GameObject newGO = new GameObject();
        newGO.tag = "Selectable";

        newshape = shapeFactory.CreateShape(dropdown.GetComponent<Dropdown>().value, panel);
        newshape.Color = ColorChange();
        newshape.ApplyMesh(newGO);
    }

    // ����� �������� ��������� ������, �������� �� ������ "�������"
    public void DeleteShape()
    {
        if (selected != null)
        {
            Destroy(selected.gameObject);
        }
    }

    // ����� ��� ������� ����� ������, ���� ����������� � ����������� �� �������� ���������
    private Color ColorChange()
    {
        Color color;

        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;

        color.a = 1;

        return color;
    }
}
