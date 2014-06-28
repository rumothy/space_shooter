using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour 
{
	public float verticalSpeed;

	void FixedUpdate()
	{
		renderer.material.mainTextureOffset = new Vector2(0f,
		                                                  Time.time * verticalSpeed);
	}
}
