using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleDeath : MonoBehaviour
{
    Tiles tileMoleIsOn;

    [SerializeField] ParticleSystem pumpkinSplat;

    [field: SerializeField]
    public ParticleSystem PumpkinExplosion { get; private set; }

    public void KillMole(bool isExploding = false)
    {
        ParticleSystem splat =
            Instantiate(isExploding ? PumpkinExplosion : pumpkinSplat, transform.position, Quaternion.identity);
        Destroy(splat.gameObject, .5f);

        tileMoleIsOn = GetComponent<MoleMover>().randomTile;
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
        gameObject.GetComponent<UpDown>().DeactivateMole(tileMoleIsOn);
    }
}
