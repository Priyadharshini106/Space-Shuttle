using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenCanvasGUI : MonoBehaviour
{
    public static HomeScreenCanvasGUI homeScreenCanvasGUI;
    // Start is called before the first frame update
    public int HighScore = 0;
    TextMeshProUGUI HighscoreTxt;
    void Awake()
    {
        homeScreenCanvasGUI = this;
        HighscoreTxt = transform.Find("Score").GetComponent<TextMeshProUGUI>();
       gameObject.transform.Find("Play").GetComponent<Button>().onClick.AddListener(()=>OnPlay());
    }
    void OnEnable()
    {
        
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighscoreTxt.text = HighScore.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))

    }
    void OnPlay()
    {
        gameObject.SetActive(false);
        MajorComponent.component.GameScreen.SetActive(true);
        MajorComponent.component.rocket.SetActive(true);
        MajorComponent.component.Comet.SetActive(true);
    }
}
