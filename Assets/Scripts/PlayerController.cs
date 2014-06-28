using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;


	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rigidbody2D.velocity = movement * speed;

		transform.position = new Vector3
		(	
			Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax), 
			0.0f
		);
	}

}
