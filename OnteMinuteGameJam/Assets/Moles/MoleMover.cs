using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMover : MonoBehaviour
{
    [SerializeField] MoleManager moleManager;
    [SerializeField] GameObject manager;

    private void Awake()
    {
        manager = GameObject.Find("MoleManager");
        moleManager = manager.GetComponent<MoleManager>();
    }

    private void OnEnable()
    {
        if (!manager)
        {
            manager = GameObject.Find("MoleManager");
            moleManager = manager.GetComponent<MoleManager>();
        }
        MovePumpkinToRandomSpot();
    }


    //private void OnEnable()
    //{
        
    //}

    private void MovePumpkinToRandomSpot()
    {
        if (moleManager.placeableTilesPositionList.Count > 0)
        {
            var random = new System.Random();
            int randomIndex = random.Next(moleManager.placeableTilesPositionList.Count);
            transform.position = moleManager.placeableTilesPositionList[randomIndex];
        }
    }
}
