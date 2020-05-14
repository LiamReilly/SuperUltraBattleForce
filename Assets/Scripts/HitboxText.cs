using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitboxText : MonoBehaviour
{
    public Text me;

    public void Start()
    {
        gameObject.SetActive(true);
        me.text = "";
    }

    public void Update()
    {
        if (GameManager.VisibleHitboxes)
            me.text = "Visible Hitboxes";
        else
            me.text = "";
    }
}
