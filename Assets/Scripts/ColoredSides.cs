using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredSides : MonoBehaviour
{
    private RectTransform rt;
    private Image ThisImage;
    public GameObject Chip1;
    public GameObject Chip2;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        ThisImage = GetComponent<Image>();
        ThisImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rt, Chip1.transform.position) || RectTransformUtility.RectangleContainsScreenPoint(rt, Chip2.transform.position))
        {
            ThisImage.enabled = true;
        }
        else ThisImage.enabled = false;
    }
}
