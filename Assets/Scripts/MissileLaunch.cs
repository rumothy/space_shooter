using UnityEngine;
using System.Collections;

public class MissileLaunch : MonoBehaviour 
{
	public float speed;
	public float launchTime;
	public float thrustTimeFactor;
	public GameObject missileTrail;

	void Start()
	{
		StartCoroutine(MissileThrust());
		StartCoroutine(LaunchMissile());
	}

	IEnumerator MissileThrust()
	{
		yield return new WaitForSeconds(launchTime * thrustTimeFactor);
		missileTrail.SetActive(true);

	}

	IEnumerator LaunchMissile()
	{
		yield return new WaitForSeconds(launchTime);

		rigidbody2D.velocity = transform.up * speed;
	}
}
