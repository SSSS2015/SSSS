using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private static CameraController sInstance;
	public static CameraController Instance { get { return sInstance; } }

	public Transform mCameraPivot;
	public Transform mCameraTrack;

	public Rigidbody mLookAtTarget;

	public float mCameraPivotSpeed = 1.0f;
	public float mCameraTrackSpeed = 1.0f;

	protected float mCurrentRotation;
	protected float mDesiredRotation;

	protected float mCurrentPosition;
	protected float mDesiredPosition;

	public void Awake()
	{
		if(sInstance != null)
		{
			Destroy(this);
			return;
		}

		sInstance = this;
	}

	public void Start()
	{
		mCurrentPosition = mCameraTrack.localPosition.y;
		mCurrentRotation = mCameraPivot.eulerAngles.z;
	}

	public void LookAt(Vector3 worldPos)
	{
		worldPos.z = mCameraPivot.position.z;
		Vector3 dirToPos = worldPos - mCameraPivot.position;
		
		// get desired position
		mDesiredPosition = dirToPos.magnitude;
		
		// get desired rotation
		dirToPos.Normalize();
		float deltaAngle = Vector3.Angle(mCameraPivot.up, dirToPos);
		int leftOrRight = (Vector3.Dot(mCameraPivot.right, dirToPos) >= 0)?-1:1;
		mDesiredRotation = mCurrentRotation + deltaAngle * leftOrRight;
	}

	public void FixedUpdate()
	{
		if(mLookAtTarget != null)
		{
			LookAt(mLookAtTarget.position + mLookAtTarget.velocity*0.33f);
		}

		// lerp towards desired look
		mCurrentRotation = Mathf.Lerp(mCurrentRotation, mDesiredRotation, Time.deltaTime*mCameraPivotSpeed);
		mCurrentPosition = Mathf.Lerp(mCurrentPosition, mDesiredPosition, Time.deltaTime*mCameraTrackSpeed);
		Vector3 rot = mCameraPivot.eulerAngles;
		rot.z = mCurrentRotation;
		mCameraPivot.eulerAngles = rot;
		Vector3 pos = mCameraTrack.localPosition;
		pos.y = mCurrentPosition;
		mCameraTrack.localPosition = pos;
	}
}
