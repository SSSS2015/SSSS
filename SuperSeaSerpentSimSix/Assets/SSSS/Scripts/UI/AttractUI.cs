using UnityEngine;
using System.Collections;

public class AttractUI : MonoBehaviour
{
    public void OnPlayClicked()
	{
		AudioController.Instance.PlayMenuConfirmSfx ();
		AudioController.Instance.ToInGameSnapshot ();
        FindObjectOfType<Attract>().StartGameClicked();
    }

    public void OnCreditsClicked()
    {
    }

}
