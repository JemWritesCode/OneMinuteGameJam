using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float moleTimeUp = 1f;
    public float moleSpeed = .00001f;

    void Update()
    {
        //StartCoroutine(moleGoesUp(moleTimeUp));


        moleGoesUp(moleTimeUp);

    }

    void moleGoesUp(float timeUp)
    {
        if ( transform.position.y <= -0.051f)
        {
            transform.Translate(0, (.1f * Time.deltaTime), 0); // This works but it doesn't stop at a certain point
        }
    }


    //IEnumerator moleGoesUp(float timeUp)
    //{
    //    float travelPercent = 0f;

    //    Vector3 startPosition = transform.position;
    //    Vector3 endPosition = new Vector3(transform.position.x, -0.051f, transform.position.z);

    //    while (travelPercent < 1f) {
    //        travelPercent += Time.deltaTime * moleSpeed;
    //        transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

}
