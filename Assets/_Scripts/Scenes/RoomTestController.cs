using System.Collections;
using UnityEngine;

public class RoomTestController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(RestoreMusicVolume());
    }

    private IEnumerator RestoreMusicVolume()
    {

        yield return new WaitForSeconds(20);

        GetComponent<AudioSource>().Play();

    }

}
