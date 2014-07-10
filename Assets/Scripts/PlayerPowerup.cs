using UnityEngine;
using System.Collections;

public class PlayerPowerup : MonoBehaviour 
{
	public float speedIncrease;
	public AudioClip applyPowerupSound;

	private PowerupOptions powerupOptions;
	private PlayerController playerController;
	void Start()
	{
		GameObject powerupOptionsObject = GameObject.FindWithTag("PowerupOptions");
		if (powerupOptionsObject != null)
			powerupOptions = powerupOptionsObject.GetComponent<PowerupOptions>();
		if (powerupOptions == null)
			Debug.Log("Cannot find 'PowerupOptions' script");

		IntializePlayerControllerBehavior();
	}

	void IntializePlayerControllerBehavior()
	{
		playerController = gameObject.GetComponent<PlayerController>();
		if (playerController == null)
			Debug.Log("Cannot find 'PlayerController' script");
	}

	void Update()
	{
		if (Input.GetButton("Fire2") && powerupOptions.CurrentPowerup != "")
		{
			Debug.Log(powerupOptions.CurrentPowerup);
			ApplyPowerup();

			powerupOptions.ResetPowerup();
			AudioSource.PlayClipAtPoint(applyPowerupSound, transform.position);
		}
	}

	void ApplyPowerup()
	{
		if (powerupOptions.CurrentPowerup == "Speed Text")
			playerController.IncreaseSpeed(speedIncrease);
		if (powerupOptions.CurrentPowerup == "Missile Text")
			playerController.EnableMissiles();

	}

}
