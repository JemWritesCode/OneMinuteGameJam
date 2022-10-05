using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleDeath : MonoBehaviour
{
    Tiles tileMoleIsOn;
    public void KillMole()
    {
        tileMoleIsOn = GetComponent<MoleMover>().randomTile;
        gameObject.GetComponent<UpDown>().DeactivateMole(tileMoleIsOn);
        //Debug.Log("Mole died!");

        //jemtodo add effects for death
    }
}
