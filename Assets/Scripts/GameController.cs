using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public GameObject powerupPrefab;
	public Vector3 spawnValues;
	public int hazardCount;
	private int hazardsKilled;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;


	private int score;
	private bool gameOver;
	private bool restart;

	void Start()
	{
		gameOver = false;
		gameOverText.text = "";
		restart = false;
		restartText.text = "";

		score = 0;
		UpdateScore();
		StartCoroutine(	SpawnWaves() );

		hazardsKilled =0;
	}

	void Update()
	{
		if (restart)
			if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
				Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			hazardsKilled = 0;
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddHazardsKilled(Transform hazardTransform)
	{
		hazardsKilled++;
		if (hazardsKilled == hazardCount)
		{
			Instantiate(powerupPrefab, hazardTransform.position, hazardTransform.rotation);
			hazardsKilled = 0;
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
