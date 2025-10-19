using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometPoolingAndSpawning : MonoBehaviour
{
    public static CometPoolingAndSpawning cometPoolingAndSpawning;
    GameObject CometPrefab;
    public int comet_poolsize = 5;
    public List<GameObject> CometPool;
    float cometStartTime = 0.3f, cometShootTime;
    public Sprite[] asteroids;
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("CometPooling");
        cometPoolingAndSpawning = this;
        CometPool = new List<GameObject>();
        for (int index = 0; index < 6; index++)
        {
            CometPrefab = Resources.Load<GameObject>("Prefabs/Asteroid" + index);
            for (int i = 0; i < comet_poolsize; i++)
            {
                GameObject Comet = Instantiate(CometPrefab);
                CometPool.Add(Comet);
                Comet.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RocketMovement.GameOver) return;
        cometShootTime += Time.deltaTime;
        if (cometShootTime > cometStartTime)
        {
            cometStartTime = ResetCometStartTime();
            cometShootTime = 0;
            GameObject comet = GetCometFromPool();
            comet.SetActive(true);
            comet.transform.localScale = cometSize();
            comet.GetComponent<CometMoving>().cometSpeed = cometSpeed(comet.transform.localScale.x);
            SpriteRenderer cometRenderer = comet.GetComponent<SpriteRenderer>();
            Bounds cometBounds = cometRenderer.bounds;
            comet.transform.position = new Vector3(Mathf.Clamp(GetRandomCometXPos(), RocketMovement.rocketMovement.screenBounds.x * -1 + cometBounds.size.x / 2, RocketMovement.rocketMovement.screenBounds.x - cometBounds.size.x / 2), transform.position.y, transform.position.z);



        }
    }
    float GetRandomCometXPos()
    {
        return Random.Range(-5, 5);
    }
    int ResetCometStartTime()
    {
        return Random.Range(1, 3);
    }
    Vector3 cometSize()
    {
        float randomSize = Random.Range(0.5f, 1f);
        return new Vector3(randomSize, randomSize, randomSize);
    }

    float cometSpeed(float size)
    {
        return Random.Range(3, 5);
    }
    GameObject GetCometFromPool()
    {
        foreach (GameObject comet in CometPool)
        {
            if (!comet.activeInHierarchy)
                return comet;

        }
        GameObject Comet = Instantiate(CometPrefab);
        CometPool.Add(Comet);
        Comet.SetActive(false);
        return Comet;
    }


    public void DisablingComet()
    {
        foreach (GameObject comet in CometPool)
            comet.SetActive(false);
    }

}
