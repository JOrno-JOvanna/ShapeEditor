using UnityEngine;
using UnityEngine.UI;

public class SlideNum : MonoBehaviour
{
    private InputField inp;
    private Text text;

    public void Start()
    {
        inp = transform.GetChild(transform.childCount - 2).GetComponent<InputField>();

        text = transform.GetChild(transform.childCount - 1).GetComponent<Text>();
    }

    public void Update()
    {
        text.text = transform.GetComponent<Slider>().value.ToString();
    }

    public void OnEndEdit()
    {
        transform.GetComponent<Slider>().value = float.Parse(inp.text);
    }
}
