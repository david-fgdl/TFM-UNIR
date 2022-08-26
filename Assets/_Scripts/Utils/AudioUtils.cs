using System.Collections;
// using System.IO;
using System;
using TMPro;
using UnityEngine;

public class AudioUtils : MonoBehaviour
{
    private FileUtils _fileUtils = new FileUtils();

    /* METODOS PARA EL MANEJO DEL AUDIO */

    // METODO PARA CREAR UN LOOP DE MUSICA ADECUADO
    public IEnumerator StartMusicLoop (AudioSource musicClip1, AudioSource musicClip2, float minusTimeBeforeLoop)
    {
        musicClip1.Play();  // Suena el clip original

        yield return new WaitForSeconds(musicClip1.clip.length - minusTimeBeforeLoop);  // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        musicClip2.Play();  // Suena el clip repetido

        yield return new WaitForSeconds(musicClip2.clip.length - minusTimeBeforeLoop);    // Se espera el tiempo adecuado para el loop (variarA en funciOn de los clips)

        StartCoroutine(StartMusicLoop(musicClip1, musicClip2, minusTimeBeforeLoop));  // Se repite la subrutina

    }

    // METODO PARA HACER SONAR EL AUDIO POR CADA LINEA
    public IEnumerator PlayAudioWhenShowFileContent(AudioSource audioOutput, AudioClip [] allAudios, string filePath, float fadeInTime, float onScreenDuration, float fadeOutTime)
    {

        string[] text = _fileUtils.ReadFileLineByLine(filePath);  // Vector que almacena el texto lInea a lInea
        int vectorIterator = 0;

        // SE HACE SONAR CADA CLIP DE AUDIO
        foreach (string line in text)
        {

            audioOutput.clip = allAudios[vectorIterator];
            audioOutput.Play();
            vectorIterator++;

            yield return new WaitForSeconds(fadeInTime + onScreenDuration + fadeOutTime + 1);  // Tiempo de espera entre lIneas

        }

    }
}