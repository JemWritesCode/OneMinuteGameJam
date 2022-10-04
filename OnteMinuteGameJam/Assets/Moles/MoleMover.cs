using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMover : MonoBehaviour
{
    [SerializeField] MoleManager moleManager;
    [SerializeField] GameObject manager;
    [SerializeField] UpDown updown;

    private void Awake()
    {
        manager = GameObject.Find("MoleManager");
        moleManager = manager.GetComponent<MoleManager>();
        updown = GetComponent<UpDown>();
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

    private void MovePumpkinToRandomSpot()
    {
        if (moleManager.placeableTilesPositionList.Count > 0)
        {
            var random = new System.Random();
            int randomIndex = random.Next(moleManager.placeableTilesPositionList.Count);
            Vector3 randomPosition = moleManager.placeableTilesPositionList[randomIndex];
            Vector3 molePosition = new Vector3(randomPosition.x, -0.479f, randomPosition.z);
            transform.position = molePosition;
            updown.moleGoesUpAndDown(1f); //hardcoded one second for up and down but may want to vary this at some point
        }
    }
}
