/* SCRIPT PARA CONTROLAR EL SISTEMA DE AUDIO */

using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS E INSTANCIA
    [SerializeField] private AudioSource _bgMusic;
    public static AudioManager Instance;
    public Sound[] Sounds;

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // CREACION DE LA INSTANCIA
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }

    // METODO START
    // Start es un mEtodo llamado una vez antes del primer frame
    private void Start() {

        // INICIAR RUTINA DE MUSICA
        StartCoroutine(RestoreMusicVolume());

    }

    /* METODOS DEL SISTEMA DE MUSICA */
    
    // RUTINA PARA RESTABLECER EL VOLUMEN DE LA MUSICA
    private IEnumerator RestoreMusicVolume()
    {

        yield return new WaitForSeconds(20);

        _bgMusic.Play();

    }


}
