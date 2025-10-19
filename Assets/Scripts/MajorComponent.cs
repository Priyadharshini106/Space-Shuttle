using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorComponent : MonoBehaviour
{
    public GameObject HomeScreen,GameScreen,rocket,Comet,Gameover;
    public static MajorComponent component;
    // Start is called before the first frame update
    void Start()
    {
        component = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
