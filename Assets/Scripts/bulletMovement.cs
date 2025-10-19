using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float bulletSpeed=2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewpos = Camera.main.WorldToViewportPoint(transform.position);
        if(viewpos.y > 0.9f)
            gameObject.SetActive(false);

        this.transform.position += Vector3.up* bulletSpeed *Time.deltaTime;
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (RocketMovement.GameOver) return;
        if (other.tag == "Comet")
        {
            Instantiate(RocketMovement.rocketMovement.BulletHit, transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameScreenGUI.gameScreenGUI.ScoreIncrement(true);
            // GameScreenGUI.gameScreenGUI.GameScore += (int)(1000 * transform.localScale.x* Time.deltaTime);
        }
    }
}
