using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour, IDamaging {
	public float mGravity = 9.8f;

	public float mLifeTime = 10.0f;
	public Rigidbody mRigidbody;

	public float mMovementForce = 500.0f;
	public int mDamagePower = 1;

	public void Awake()
	{
		if(mRigidbody == null)
		{
			mRigidbody = GetComponent<Rigidbody>();
		}

		mRigidbody.isKinematic = true;
		mRigidbody.detectCollisions = false;
	}

	public void FixedUpdate()
	{
		if(!mRigidbody.isKinematic)
		{
			ApplyGravity();
			transform.LookAt(transform.position + mRigidbody.velocity);
			if(transform.position.magnitude < World.Instance.SeaLevel - 0.5f)
			{
				mRigidbody.drag = 2f;
			}
		}
	}

	public void LaunchToward(Vector3 targetPos)
	{
		transform.parent = null;

		mRigidbody.isKinematic = false;
		mRigidbody.detectCollisions = true;

		Vector3 dir = targetPos - transform.position;
		dir.Normalize();
		mRigidbody.AddForce(dir*mMovementForce, ForceMode.Force);
		Destroy(gameObject, mLifeTime);
	}

	public void ApplyDamage(Serpent target)
	{
		target.TakeDamage(mDamagePower);
	}

	public void OnCollisionEnter(Collision c)
	{
		mRigidbody.isKinematic = true;
		mRigidbody.detectCollisions = false;
		transform.parent = c.collider.transform;
	}

	public void ApplyGravity()
	{
		Vector3 gravityDir = transform.position;
		gravityDir.Normalize();
		gravityDir *= -1;
		mRigidbody.AddForce(gravityDir*mGravity, ForceMode.Acceleration);
	}
}
