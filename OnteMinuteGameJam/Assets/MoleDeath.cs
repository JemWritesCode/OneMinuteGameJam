using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleDeath : MonoBehaviour
{
    Tiles tileMoleIsOn;
    public void KillMole()
    {
        tileMoleIsOn = GetComponent<MoleMover>().randomTile;
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
        gameObject.GetComponent<UpDown>().DeactivateMole(tileMoleIsOn);

    }
}
