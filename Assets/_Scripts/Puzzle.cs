using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TFM-UNIR/New Puzzle")]
public class Puzzle : ScriptableObject
{
    public enum Type {
        None,
        Object,
        Number,
        OneWay
    }
}
