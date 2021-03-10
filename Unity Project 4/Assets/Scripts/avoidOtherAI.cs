using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidOtherAI : MonoBehaviour
{
	public float speed;
	public float minDist;
	public GameObject target;
	public GameObject dolphin;

	private Rigidbody2D body;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, minDist);

		foreach (Collider2D col in colls)
		{
			if (col.gameObject != target)
			{
				Vector2 desired = col.gameObject.transform.position - transform.position;

				float actual = desired.magnitude - minDist;
				if (col.gameObject == dolphin)
				{
					actual *= 3;
				}
				body.AddForce(desired.normalized *
					actual * speed - body.velocity);
			}
		}

	}
}
