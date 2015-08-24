using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour
{
    public GameObject TitlescreenUI;

    private bool mInGame;
    private bool mStartGame;

    private float mTimer = 5.0f;

	// Update is called once per frame
	void Update ()
    {
        if (mInGame)
            return;

        mTimer -= Time.deltaTime;
        if (mTimer < 0)
            mStartGame = true;

        if(mStartGame)
        {
            mStartGame = false;
            StartGame();
        }
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
