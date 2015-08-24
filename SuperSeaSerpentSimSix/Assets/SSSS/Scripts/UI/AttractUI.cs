using UnityEngine;
using System.Collections;

public class AttractUI : MonoBehaviour
{
    public void OnPlayClicked()
    {
        FindObjectOfType<Attract>().StartGameClicked();
    }

    public void OnCreditsClicked()
    {
    }

}
