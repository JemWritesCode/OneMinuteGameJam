using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleDeath : MonoBehaviour
{
    Tiles tileMoleIsOn;
    [SerializeField] ParticleSystem pumpkinSplat;
    public void KillMole()
    {
        ParticleSystem splat = Instantiate(pumpkinSplat, transform.position, Quaternion.identity);
        Destroy(splat, .5f);
        tileMoleIsOn = GetComponent<MoleMover>().randomTile;
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
        gameObject.GetComponent<UpDown>().DeactivateMole(tileMoleIsOn);
    }
}
