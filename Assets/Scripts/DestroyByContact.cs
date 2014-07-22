using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	GameObject otherGameObject;
	
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if (gameController == null)
			Debug.Log("Cannot find 'GameController' script");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Boundary")
			return;

		if (other.tag == "Level")
			return;

		if (other.tag == "Enemy")
			return;

		if (other.tag == "Missile")
		{
			gameController.PresentOptions();
			//hit = gameController.ChoiceCorrect();
			otherGameObject = other.gameObject;
			gameController.DestroyOnHit = DestroyOnHit;

			return;
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		Instantiate(explosion, transform.position, transform.rotation);
		gameController.AddScore(scoreValue);
		gameController.AddHazardsKilled(transform);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}

	void DestroyOnHit()
	{
		Debug.Log("DestroyOnHit");
		Instantiate(explosion, transform.position, transform.rotation);
		gameController.AddScore(scoreValue);
		gameController.AddHazardsKilled(transform);
		Destroy(otherGameObject);
		Destroy(gameObject);
	}
}
