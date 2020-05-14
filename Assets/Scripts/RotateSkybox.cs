using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public Material space;
    private Skybox sky;
    public float SkyboxSpeed;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        //sky = GetComponent<Skybox>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        RenderSettings.skybox.SetFloat("Rotation", time * SkyboxSpeed);

    }
}
