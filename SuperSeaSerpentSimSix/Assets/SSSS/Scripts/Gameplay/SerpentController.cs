using UnityEngine;
using System.Collections;

public class SerpentController : MonoBehaviour {
	public Serpent mSerpent;

	protected Plane mControlPlane;
	protected Rect mScreenArea;

	public void Awake()
	{
		mControlPlane = new Plane(Vector3.forward, Vector3.zero);
		mScreenArea = new Rect(0, 0, Screen.width, Screen.height);
	}

	public void Start()
	{
		if(mSerpent == null)
		{
			mSerpent = GetComponent<Serpent>();
		}
	}

	// Update is called once per frame
	public void Update () {

		Vector3 mousePos = Input.mousePosition;
		if(mScreenArea.Contains(mousePos))
		{
			Ray camRay = Camera.main.ScreenPointToRay(mousePos);
			float dist;
			if(mControlPlane.Raycast(camRay, out dist))
			{
				Vector3 desiredPos = camRay.GetPoint(dist);
				mSerpent.MoveToward(desiredPos);
			}
		}
		/*
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist;
		if(mControlPlane.Raycast(camRay, out dist))
		{
			Vector3 desiredPos = camRay.GetPoint(dist);
			mSerpent.MoveToward(desiredPos);
		}
		*/
	}
}
