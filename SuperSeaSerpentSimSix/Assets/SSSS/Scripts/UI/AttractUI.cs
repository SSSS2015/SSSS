using UnityEngine;
using System.Collections;

public class AttractUI : MonoBehaviour
{
    public void OnPlayClicked()
	{
		AudioController.Instance.PlayMenuConfirmSfx ();
        FindObjectOfType<Attract>().StartGameClicked();
    }

    public void OnCreditsClicked()
    {
    }

}
