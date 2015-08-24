using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public void PlayAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
