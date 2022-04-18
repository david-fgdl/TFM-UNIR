using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class Sound : MonoBehaviour
{
    public string soundName;
    public AudioClip clip;

    [Range(0f, 100f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
}
