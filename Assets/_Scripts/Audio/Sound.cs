using UnityEngine;
using System;

[Serializable]
public class Sound : MonoBehaviour
{
    public string SoundName;
    public AudioClip Clip;

    [Range(0f, 100f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;
}
