using UnityEngine;
using System.Collections;

public delegate object MissileInstantiate(Object prefab, Vector3 vec, Quaternion quat);

[System.Serializable]
public enum LaunchDirection
{
	Left = -1,
	Right = 1
}

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

[System.Serializable]
public class MissileLauncher
{
	public Transform launcherPosition;
	public LaunchDirection launcherDirection;
	public float launchSpeed;
	public float launchSpeedFactor;

	public void Launch(GameObject missileInstance)
	{
		float direction = launcherDirection == LaunchDirection.Left ? -1: 1;
		missileInstance.rigidbody2D.velocity =  new Vector2(launchSpeed * direction , -launchSpeed * launchSpeedFactor);
	}
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public GameObject missilePrefab;
//	public Transform leftMissileLauncher;
//	public Transform rightMissileLauncher;
//	public float launchSpeed;
//	public float launchSpeedFactor;
	public AudioClip missileLaunchClip;
	public AudioClip missileFireClip;
	public float missileLaunchTime;
	public bool missilesEnabled = false;

	public float fireRate;
	private float nextFire;

	public MissileLauncher leftMissileLauncher;
	public MissileLauncher rightMissileLauncher;

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play();

			if (missilesEnabled)
			{
				GameObject leftMissileInstance = Instantiate(missilePrefab, 
				                                             leftMissileLauncher.launcherPosition.position, 
				                                             leftMissileLauncher.launcherPosition.rotation) as GameObject;
				leftMissileLauncher.Launch(leftMissileInstance);

				GameObject rightMissileInstance = Instantiate(missilePrefab, 
				                                              rightMissileLauncher.launcherPosition.position, 
				                                              rightMissileLauncher.launcherPosition.rotation) as GameObject;
//				rightMissileInstance.rigidbody2D.velocity = new Vector2(launchSpeed, -launchSpeed * launchSpeedFactor);
				rightMissileLauncher.Launch(rightMissileInstance);

				AudioSource.PlayClipAtPoint(missileLaunchClip, transform.position, 0.25f);
				StartCoroutine(MissileLauchAudio());
			}

		}
	}

	IEnumerator MissileLauchAudio()
	{
		yield return new WaitForSeconds(missileLaunchTime);
		AudioSource.PlayClipAtPoint(missileFireClip, transform.position);
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

	public void IncreaseSpeed(float value)
	{
		speed += value;
	}

	public void EnableMissiles()
	{
		missilesEnabled = true;
	}

}
