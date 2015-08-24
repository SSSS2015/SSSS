using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour
{
    public GameObject TitlescreenUI;

    private bool mInGame;
    private bool mStartGame;

	// Update is called once per frame
	void Update ()
    {
        if (mInGame)
            return;

        if(mStartGame)
        {
            mStartGame = false;
            StartGame();
        }
	}

    public void StartGameClicked()
    {
        mStartGame = true;
    }

    private void StartGame()
    {
        SerpentController controller = FindObjectOfType<SerpentController>();
        controller.mAttract = false;

        GameObject canvas = GameObject.Find("/Canvas-MainUI");
        Transform mainUI = canvas.transform.Find("MainUI");
        mainUI.gameObject.SetActive(true);

        Transform attractUI = canvas.transform.Find("AttractUI");
        attractUI.gameObject.SetActive(false);

        mInGame = true;
    }

}
