using UnityEngine;
using System.Collections;

public class SerpentController : MonoBehaviour {
	public Serpent mSerpent;
    public bool mAttract = false;

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
	public void Update ()
    {
        if(mAttract)
        {
            AttractModeUpdate();
        }
        else
        {
            GameModeUpdate();
        }
    }

    private void GameModeUpdate()
    {
		Vector3 mousePos = Input.mousePosition;
		//if(mScreenArea.Contains(mousePos))
		//{
			Ray camRay = Camera.main.ScreenPointToRay(mousePos);
			float dist;
			if(mControlPlane.Raycast(camRay, out dist))
			{
				Vector3 desiredPos = camRay.GetPoint(dist);
				mSerpent.MoveToward(desiredPos);

				if(Input.GetMouseButtonDown(0))
				{
					Debug.Log("Attack");
					mSerpent.Attack(desiredPos);
				}
			}
		//}
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

    private void AttractModeUpdate()
    {
        Vector2 polar = World.Instance.GetPolarCoordinate(transform.position);
        polar.y += 10.0f * Mathf.Deg2Rad;
        polar.x = World.Instance.GetSeaLevel(polar.y) - 2.0f;
        Vector3 target = World.Instance.GetWorldCoordinate(polar);
        mSerpent.MoveToward(target);
    }
}
