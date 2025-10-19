using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScolling : MonoBehaviour
{
    MeshRenderer BG_Renderer;
    public float speed =2f;

    // Start is called before the first frame update
    void Start()
    {
        BG_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        BG_Renderer.material.mainTextureOffset = new Vector2(Time.time * speed,Time.time*speed);
    }
}
