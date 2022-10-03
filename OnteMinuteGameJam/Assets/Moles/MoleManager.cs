using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    // for all the pumpkins in the mole list
    // pick a random pumpkin
    // make that random pumpkin go up and down

    // nactually I need spawn ocoordinates that roughly work for all the different things that we might spawn instead
    // and then spawn the pumpkin right there on the fly and it goes up. This will also mean less objects on the screen at once, we just spawn the pumpkins as needed

    // spawner grid
    //spawn pumpkins at random intervals
    // there's different color pumpkins and there's a bomb pumkin that could be the pumpkin that you try to avoid

    // i could rearrange the level so it's more of a square, zoom out the camera  bi tand make it spawn on coorindates instead of bespoke positioning 

    // you make an object pool and you just change the position of the pumpkins each time you enable it
    // //and it'll also reset it? or maybe hve to do the resetting manually?

    [SerializeField] GameObject basicMole;
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimeInterval = 1f;

    GameObject[] molePool;

    List<Vector2> placeableTilesList = new List<Vector2>();

    void Start()
    {
        PopulatePool();
        BuildPlaceableTilesArray();
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        molePool = new GameObject[poolSize];

        for (int i = 0; i < molePool.Length; i++)
        {
            molePool[i] = Instantiate(basicMole, transform);
            molePool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true) //jem todo til the timer runs out?
        {
            EnableObjectInPool();
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
                molePool[i].GetComponent<UpDown>().moleGoesUpAndDown(1f);
                return;
            }
        }
    }


    public void BuildPlaceableTilesArray()
    {
        // placeableTilesList.Add() //add the tile coordinate vector2
    }
}
