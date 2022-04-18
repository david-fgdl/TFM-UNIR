using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTestController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RestoreMusicVolume());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RestoreMusicVolume()
    {

        yield return new WaitForSeconds(20);

        GetComponent<AudioSource>().Play();

    }

}
