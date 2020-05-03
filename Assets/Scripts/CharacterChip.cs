using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChip : MonoBehaviour
{
    public float speed;
    
    public RectTransform RedSide;
    public GameObject RedImage;
    public RectTransform BlueSide;
    public GameObject BlueImage;
    public static bool Selected;
    public static string CharacterSelected;
    public static CharacterChip Instance;

    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (!Selected)
        {
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(RedSide, gameObject.transform.position))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Selected = true;
                    CharacterSelected = "Red";
                }
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(BlueSide, gameObject.transform.position))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Selected = true;
                    CharacterSelected = "Blue";
                }
            }
        }
           
    }
}
