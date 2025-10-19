using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreenGUI : MonoBehaviour
{
    public static GameScreenGUI gameScreenGUI;
    public int GameScore;
    GameObject[] LifeCount;
    TextMeshProUGUI Score;
    private Coroutine scoreCoroutine;
    GameObject PauseScreen;
    // Start is called before the first frame update
    void Awake()
    {
        gameScreenGUI = this;
        GameScore = 0;
        LifeCount = new GameObject[3];
        int i = 0;
        GameObject Life = gameObject.transform.Find("LifeCount").gameObject;
        foreach (Transform child in Life.transform)
        {
            LifeCount[i] = child.gameObject;
            i++;
        }
        Score = gameObject.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        PauseScreen = gameObject.transform.Find("PauseBG").gameObject;
        Score.text = "0";
    }
    void OnEnable()
    {
        GameScore = 0;
        Score.text = GameScore.ToString();
        int i = 0;
        GameObject Life = gameObject.transform.Find("LifeCount").gameObject;
        foreach (Transform child in Life.transform)
        {
            LifeCount[i] = child.gameObject;
            LifeCount[i].SetActive(true);
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseScreen.activeInHierarchy)
            {
                Time.timeScale = 1;
                PauseScreen.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                PauseScreen.SetActive(true);
            }
        }
    }

    public void ScoreIncrement(bool hitObject = false)
    {
        int score = GameScore;
        int increment = hitObject ? 1000 : 10;
        GameScore += (int)(Time.deltaTime * BulletSpawning.bulletSpawning.bulletSpeed * increment);

        if (scoreCoroutine != null)
            StopCoroutine(scoreCoroutine);
        scoreCoroutine = StartCoroutine(ScoreCounter(score));

    }
    IEnumerator ScoreCounter(int score)
    {
        while (score < GameScore)
        {
            score++;
            Score.text = score.ToString();
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void DecreasingLifeCount()
    {
        for (int i = 3; i > 0; i--)
        {
            if (LifeCount[i - 1].activeInHierarchy)
            {
                LifeCount[i - 1].SetActive(false);
                break;
            }

        }
        if (!LifeCount[0].activeSelf)
            RocketMovement.GameOver = true;
    }


    public void ReturnToHome()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        BulletSpawning.bulletSpawning.Disablebullet();
        CometPoolingAndSpawning.cometPoolingAndSpawning.DisablingComet();
        MajorComponent.component.rocket.SetActive(false);
        MajorComponent.component.Comet.SetActive(false);
        MajorComponent.component.GameScreen.SetActive(false);
        MajorComponent.component.HomeScreen.SetActive(true);
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }
}
