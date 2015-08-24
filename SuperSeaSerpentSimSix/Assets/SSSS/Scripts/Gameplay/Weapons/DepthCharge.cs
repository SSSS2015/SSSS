using UnityEngine;
using System.Collections;

public class DepthCharge : BaseProjectile {
	public float mDamageRadius = 5.0f;
	public float mExplosionForce = 500.0f;
	public GameObject mExplosionPrefab;

	public void OnDestroy()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, mDamageRadius);
		Serpent serpent = null;
		for(int i = 0, n = colliders.Length; i < n; ++i)
		{
			Collider c = colliders[i];
			if(c.attachedRigidbody != null)
			{
				c.attachedRigidbody.AddExplosionForce(mExplosionForce*c.attachedRigidbody.mass, transform.position, mDamageRadius);
				Debug.DrawLine(transform.position, c.transform.position, Color.red, 60.0f);
			}
			if(serpent == null)
			{
				SerpentSegment segment = c.GetComponentInParent<SerpentSegment>();
				if(segment != null)
				{
					serpent = segment.mSerpent;
				}
			}
		}

		if(serpent != null)
		{
			serpent.TakeDamage(mDamagePower);
		}

		Quaternion rot = Quaternion.LookRotation(transform.position.normalized, Vector3.forward);
		GameObject explosion = Instantiate(mExplosionPrefab, transform.position, rot) as GameObject;
		explosion.transform.localScale = Vector3.one*mDamageRadius;
		Destroy(explosion, 2.0f);
	}

	public override void ApplyDamage (Serpent target)
	{
		// don't apply damage here
	}

	public override void OnCollisionEnter (Collision c)
	{
		// do nothing
	}
}
