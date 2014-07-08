using UnityEngine;
using System.Collections;

public class PowerupPickup : MonoBehaviour 
{
	public AudioClip pickupClip;

	private PowerupOptions powerupOptions;
	void Start()
	{
		GameObject powerupOptionsObject = GameObject.FindWithTag("PowerupOptions");
		if (powerupOptionsObject != null)
			powerupOptions = powerupOptionsObject.GetComponent<PowerupOptions>();
		if (powerupOptions == null)
			Debug.Log("Cannot find 'PowerupOptions' script");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			powerupOptions.PowerupPickedUp();
			Destroy(gameObject);
		}
	}
}
