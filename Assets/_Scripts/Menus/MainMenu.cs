using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play() {
        GameManager.Instance.ChangeState(GameState.Game);
    }
}
