using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    TextMeshProUGUI Score;
    // Start is called before the first frame update
    void Awake()
    {
        Score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        Score.text = GameScreenGUI.gameScreenGUI.GameScore.ToString();
        transform.Find("Home").GetComponent<Button>().onClick.AddListener(()=>ReturnToHome());
        transform.Find("Retry").GetComponent<Button>().onClick.AddListener(()=>OnRetry());

    }

    void OnEnable()
    {
        Score.text = GameScreenGUI.gameScreenGUI.GameScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToHome()
    {
        gameObject.SetActive(false);
        BulletSpawning.bulletSpawning.Disablebullet();
        CometPoolingAndSpawning.cometPoolingAndSpawning.DisablingComet();
        MajorComponent.component.HomeScreen.SetActive(true);
    }

    void OnRetry()
    {
        gameObject.SetActive(false);
        MajorComponent.component.GameScreen.SetActive(true);
        MajorComponent.component.rocket.SetActive(true);
        MajorComponent.component.Comet.SetActive(true);
    }
}
