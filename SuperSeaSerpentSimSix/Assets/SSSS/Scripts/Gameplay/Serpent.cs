using UnityEngine;
using System.Collections;

public class Serpent : MonoBehaviour {

	public float mSpeedMultiplier = 2.0f;
	public float mGravityAcceleration = 9.8f;

	protected Vector3 mDesiredPos;
	protected Rigidbody mRigidbody;

	public void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}
	}

	public void Start()
	{
		mDesiredPos = transform.position;
	}

	public void FixedUpdate()
	{
		bool isAboveWater = transform.localPosition.magnitude >= 50.0f;
		if(isAboveWater)
		{
			Vector3 gravityDir = transform.position;
			gravityDir.Normalize();
			gravityDir *= -1;
			mRigidbody.AddForce(gravityDir*mGravityAcceleration, ForceMode.Acceleration);
			return;
		}

		Vector3 desiredDir = mDesiredPos - transform.position;
		float distFromDesiredPos = desiredDir.magnitude;
		desiredDir.Normalize ();
		Vector3 desiredVel = desiredDir * distFromDesiredPos * mSpeedMultiplier;// * Time.deltaTime;
		mRigidbody.velocity = Vector3.Lerp(mRigidbody.velocity, desiredVel, Time.deltaTime);
		//mRigidbody.AddForce(vel, ForceMode.VelocityChange);
	}
	
	public void MoveToward(Vector3 worldPos)
	{
		mDesiredPos = worldPos;
	}
}
