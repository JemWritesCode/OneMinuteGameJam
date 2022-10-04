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
        if (moleManager.placeableTiles.Count > 0)
        {
            var random = new System.Random();
            Tiles randomTile;
            do
            {
                int randomIndex = random.Next(moleManager.placeableTiles.Count);
                randomTile = moleManager.placeableTiles[randomIndex];
            } while (randomTile.tileHasMole); //keep picking a random tile until you find one that doesn't already have a mole
            Vector3 molePosition = new Vector3(randomTile.transform.position.x, -0.479f, randomTile.transform.position.z);
            randomTile.tileHasMole = true;
            transform.position = molePosition;
            updown.moleGoesUpAndDown(1f, randomTile); //hardcoded one second for up and down but may want to vary this at some point
        }
    }
}
