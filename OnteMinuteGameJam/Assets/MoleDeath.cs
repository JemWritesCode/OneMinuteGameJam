using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleDeath : MonoBehaviour
{
    Tiles tileMoleIsOn;
    public void MoleDied()
    {
        tileMoleIsOn = GetComponent<MoleMover>().randomTile;
        gameObject.GetComponent<UpDown>().DeactivateMole(tileMoleIsOn);
        Debug.Log("Mole died!");
    }
}
