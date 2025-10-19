using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public static RocketMovement rocketMovement;
    //  BulletSpawning bulletSpawning;
    Vector3 RocketPosition;
    bool dragging;
    Vector3 offset;
    public Vector2 screenBounds;
    SpriteRenderer RocketSpriteRender;
    Bounds RocketBound;
    float shootTime;
    public float canShootTime = 0.3f;
    public GameObject rocketExplosion, BulletHit;

    Vector2 startPos, direction;

    public static bool GameOver= false;


    // Start is called before the first frame update
    void Start()
    {
        rocketMovement = this;
        shootTime = 0;
        rocketExplosion = Resources.Load<GameObject>("Prefabs/RocketExplosion").gameObject;
        BulletHit = Resources.Load<GameObject>("Prefabs/BulletHit").gameObject;
        // bulletSpawning = GetComponent<BulletSpawning>();
        RocketSpriteRender = GetComponent<SpriteRenderer>();
        RocketBound = RocketSpriteRender.bounds;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void OnEnable()
    {
        GameOver = false;
        shootTime = 0;
        gameObject.transform.position = new Vector3(0f, -4f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver)
        {
            if (GameScreenGUI.gameScreenGUI.GameScore > HomeScreenCanvasGUI.homeScreenCanvasGUI.HighScore)
                PlayerPrefs.SetInt("HighScore", GameScreenGUI.gameScreenGUI.GameScore);
            GameOverScreen();
            return;
        }
        shootTime += Time.deltaTime;
        Debug.Log("Input mouse " + Input.mousePosition);
#if UNITY_EDITOR
        if (dragging)
        {
            RocketPosition = Input.mousePosition;
            RocketPosition.z = Mathf.Abs(Camera.main.transform.position.z);
            this.transform.position = Camera.main.ScreenToWorldPoint(RocketPosition) + offset;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenBounds.x * -1, screenBounds.x), Mathf.Clamp(transform.position.y, screenBounds.y * -1 + RocketBound.size.y / 2, screenBounds.y - RocketBound.size.y / 2), transform.position.z);
           
        }

#else
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            GameScreenGUI.gameScreenGUI.ScoreIncrement();
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    dragging = true;
                    offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, Mathf.Abs(Camera.main.transform.position.z)));
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one


                    Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Mathf.Abs(Camera.main.transform.position.z)));
                    transform.position = touchWorldPos + offset;

                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenBounds.x * -1, screenBounds.x), Mathf.Clamp(transform.position.y, screenBounds.y * -1 + RocketBound.size.y / 2, screenBounds.y - RocketBound.size.y / 2), transform.position.z);
                   

                    break;

                case TouchPhase.Ended:
                
                    dragging = false;
                    // Report that the touch has ended when it ends
                    // direction = Vector2.zero;
                    break;
            }

        }
        
#endif
        if(dragging)
        {
            if (shootTime > canShootTime)
            {
                
                shootTime = 0;
                BulletSpawning.bulletSpawning.OnShoot();
            }
        }
    }



    void OnMouseDown()
    {
        Vector3 StartPosition = Input.mousePosition;
        StartPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        offset = transform.position - Camera.main.ScreenToWorldPoint(StartPosition);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameOver) return;
        if (other.tag == "Comet")
        {
            Instantiate(rocketExplosion, transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            GameScreenGUI.gameScreenGUI.DecreasingLifeCount();
        }
    }

    void GameOverScreen()
    {
        MajorComponent.component.rocket.SetActive(false);
        MajorComponent.component.Comet.SetActive(false);
        MajorComponent.component.GameScreen.SetActive(false);
        MajorComponent.component.Gameover.SetActive(true);
    }
}
