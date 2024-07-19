using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Mob1Prefab;
    public GameObject Mob2Prefab;
    public UIManager UIManager;

    public List<GameObject> spawnPoints;

    public int spawnedMobs;

    private int gameLevel;
    private List<GameObject> mobs;
    private int spawnIndex;
    private bool isGameWon;
    private float timer;
    private float nextSpawnTime;
    private int currentMobIndex;
    private int mobCount;
    

    private void Start()
    {
        gameLevel = PlayerPrefs.GetInt("Level");
        spawnIndex = 0;
        SpawnEnemies();
        timer = 0;
        spawnedMobs = 0;
        isGameWon = false;
        mobCount = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f && spawnedMobs == 0 && !isGameWon)
        {
            UIManager.GameWin();
            isGameWon = true;
        }
        if (timer > nextSpawnTime && mobCount < (gameLevel + 1) * 2)
        {
            nextSpawnTime = Time.time + 5f;
            InstantiateEnemy(mobs[currentMobIndex]);
            currentMobIndex++;
            if (currentMobIndex > mobs.Count - 1)
            {
                currentMobIndex = 0;
            }

            spawnedMobs++;
            mobCount++;
        }
    }

    public void DespawnMob()
    {
        spawnedMobs--;
    }

    private void SpawnEnemies()
    {
        mobs = new List<GameObject>();
        for (int i = 0; i < gameLevel + 1; i++)
        {
            mobs.Add(Mob1Prefab);
            mobs.Add(Mob2Prefab);
        }
    }


    private void InstantiateEnemy(GameObject mob)
    {
        Instantiate(mob, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
        if (spawnIndex <= 6)
        {
            spawnIndex++;
        }
        else
        {
            spawnIndex = 0;
        }

    }
}
