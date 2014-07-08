using UnityEngine;
using System.Collections;

public class TransformMover : MonoBehaviour 
{
	public float speed;

	void FixedUpdate()
	{
		float y = transform.position.y + speed;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
