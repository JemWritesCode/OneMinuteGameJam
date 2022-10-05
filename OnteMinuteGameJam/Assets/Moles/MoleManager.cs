using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    //idea todo there's different color pumpkins and there's a bomb pumkin that could be the pumpkin that you try to avoid

    [SerializeField] GameObject basicMole;

    [Tooltip("There are 100 squares on the board, so that's the max.")]
    [SerializeField] [Range(0, 100)] int poolSize = 5;
    
    [SerializeField] [Range(0.1f, 30f)] float spawnTimeInterval = 1f;
    public List<Tiles> placeableTiles = new List<Tiles>();

    GameObject[] molePool;

    public int EnemiesSpawnedCount { get; private set; }
    public event EventHandler<Vector3> OnMoleUpDownEnd;

    void Start()
    {
        EnemiesSpawnedCount = 0;
        PopulatePool();
        BuildPlaceableTilesArray();
        StartCoroutine(SpawnEnemy());

        InvokeRepeating("reduceSpawnInterval", 1, 1);
    }

    public void InvokeMoleUpDownEnd(Vector3 molePosition) {
      OnMoleUpDownEnd?.Invoke(this, molePosition);
    }

    private void reduceSpawnInterval()
    {
        if (spawnTimeInterval >= .5f)
        {
            spawnTimeInterval -= .09f;
        }
    }

    private void PopulatePool()
    {
        molePool = new GameObject[poolSize];

        for (int i = 0; i < molePool.Length; i++)
        {
            molePool[i] = Instantiate(basicMole, transform);
            molePool[i].SetActive(false);
            molePool[i].tag = "Mole";
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true) //jem todo til the timer runs out?
        {
            EnableObjectInPool();
            EnemiesSpawnedCount++;
            yield return new WaitForSeconds(spawnTimeInterval); //jemtodo this is probably where I change the intervals of spawning to vary for difficulty?
        }
    }


    private void EnableObjectInPool()
    {
        for (int i = 0; i < molePool.Length; i++)
        {
            if (molePool[i].activeInHierarchy == false)
            {
                molePool[i].SetActive(true);
               // molePool[i].GetComponent<UpDown>().moleGoesUpAndDown(1f);
                return;
            }
        }
    }


    public void BuildPlaceableTilesArray()
    {
        Tiles[] allTilesArray = (Tiles[])GameObject.FindObjectsOfType(typeof(Tiles));

        foreach(Tiles tile in allTilesArray)
        {
            if (tile.IsPlaceable == true)
                placeableTiles.Add(tile); 
        }
    }
}
