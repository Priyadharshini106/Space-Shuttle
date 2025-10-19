using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CometMoving : MonoBehaviour
{
    SpriteRenderer cometRenderer;
    Bounds cometBounds;
    public float cometSpeed =2f;
    // Start is called before the first frame update
    void Start()
    {
        cometRenderer = GetComponent<SpriteRenderer>();
        cometBounds = cometRenderer.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewpos = Camera.main.WorldToViewportPoint(transform.position);
        if((viewpos.y + (cometBounds.size.y/2)) < 0)
            gameObject.SetActive(false);
        this.transform.position += Vector3.down*cometSpeed* Time.deltaTime;
    }
}
