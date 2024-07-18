using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Mob1Prefab;
    public GameObject Mob2Prefab;
    public UIManager UIManager;

    public List<GameObject> spawnPoints;

    public bool finishedSpawning;

    public int spawnedMobs;

    private int gameLevel;
    private List<GameObject> mobs;
    private int spawnIndex;
    private bool isGameWon;
    private float timer;

    private void Start()
    {
        gameLevel = PlayerPrefs.GetInt("Level");
        spawnIndex = 0;
        SpawnEnemies();
        finishedSpawning = false;
        timer = 0;
        spawnedMobs = 1;
        isGameWon = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f && spawnedMobs == 0 && !isGameWon)
        {
            UIManager.GameWin();
            Debug.Log("true");
            isGameWon = true;
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

        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        foreach (GameObject obj in mobs)
        {
            InstantiateEnemy(obj);
            spawnedMobs++;
            yield return new WaitForSecondsRealtime(5f);
        }
        finishedSpawning = true;
    }

    private void InstantiateEnemy(GameObject mob)
    {
        Instantiate(mob, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
        if(spawnIndex <= 6)
        {
            spawnIndex++;
        }
        else
        {
            spawnIndex = 0;
        }

    }
}
