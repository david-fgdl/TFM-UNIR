using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgMusic;
    public static AudioManager Instance;


    public Sound[] Sounds;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() {
        // INICIAR RUTINA DE MUSICA
        StartCoroutine(RestoreMusicVolume());
    }

    private IEnumerator RestoreMusicVolume()
    {

        yield return new WaitForSeconds(20);

        _bgMusic.Play();

    }


}
