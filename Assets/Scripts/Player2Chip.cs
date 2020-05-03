using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Chip : MonoBehaviour
{
    public float speed;

    public RectTransform RedSide;
    public GameObject RedImage;
    public RectTransform BlueSide;
    public GameObject BlueImage;
    public static bool Selected;
    public static string CharacterSelected;
    public static Player2Chip Instance;

    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (!Selected)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(BlueSide, gameObject.transform.position))
            {
                if (Input.GetKey(KeyCode.RightShift))
                {
                    Selected = true;
                    CharacterSelected = "Blue";
                }
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(RedSide, gameObject.transform.position))
            {
                if (Input.GetKey(KeyCode.RightShift))
                {
                    Selected = true;
                    CharacterSelected = "Red";
                }
            }

        }

    }


}
