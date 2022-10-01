using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float moleTimeUp = 1f;
    public float moleSpeed = .000001f;

    void Update()
    {
        StartCoroutine(moleGoesUp(moleTimeUp));
    }


    IEnumerator moleGoesUp(float timeUp)
    {
        float travelPercent = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, -0.051f, transform.position.z);

        while (travelPercent < 1f) {
            travelPercent += Time.deltaTime * moleSpeed;
            transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
    }

}
