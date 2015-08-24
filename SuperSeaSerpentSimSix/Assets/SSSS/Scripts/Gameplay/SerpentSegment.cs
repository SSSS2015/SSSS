using UnityEngine;
using System.Collections;

public class SerpentSegment : BaseSeaCreature {
	public Joint mJoint;
	public Serpent mSerpent;
	public Renderer mRenderer;

	public override void Awake()
	{
		base.Awake();
		if(mJoint == null)
		{
			mJoint = GetComponent<Joint>();
		}
		if(mRenderer == null)
		{
			mRenderer = GetComponentInChildren<Renderer>();
		}
	}

	public void AttachTo(GameObject attachTarget)
	{
		mJoint.connectedBody = attachTarget.GetComponent<Rigidbody>();
	}

	public virtual void OnCollisionEnter(Collision c)
	{
		IDamaging damaging = c.collider.GetComponent(typeof(IDamaging)) as IDamaging;
		if(damaging != null)
		{
			damaging.ApplyDamage(mSerpent);
		}
	}
}
