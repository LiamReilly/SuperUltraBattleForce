using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxText : MonoBehaviour
{
    public void Update()
    {
        gameObject.SetActive(GameManager.VisibleHitboxes);
    }
}
