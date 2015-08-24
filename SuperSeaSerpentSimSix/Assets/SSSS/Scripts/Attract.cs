using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour
{
    public GameObject TitlescreenUI;

    private bool mInGame;
    private bool mStartGame;
    private bool mGameOver;
    private Serpent mSerpent;

	// Update is called once per frame
	void Update ()
    {
        if (mGameOver)
            return;

        if (mInGame)
        {
            CheckForGameOver();
            return;
        }

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
        mSerpent = controller.GetComponent<Serpent>();

        GameObject canvas = GameObject.Find("/Canvas-MainUI");
        Transform mainUI = canvas.transform.Find("MainUI");
        mainUI.gameObject.SetActive(true);

        Transform attractUI = canvas.transform.Find("AttractUI");
        attractUI.gameObject.SetActive(false);

        mInGame = true;
    }

    private void CheckForGameOver()
    {
        if(mSerpent.Health == 0)
        {
            // game is over
            GameObject canvas = GameObject.Find("/Canvas-MainUI");
            Transform gameOverUI = canvas.transform.Find("GameOverUI");
            gameOverUI.gameObject.SetActive(true);
        }
    }
}
