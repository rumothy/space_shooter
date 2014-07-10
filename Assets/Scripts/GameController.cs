using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject questionOptionsGameObject;
	private QuestionOptions questionOptions;

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
	public float currentSlowMo;
	public float slowTimeAllowed;
	public bool presentOptions = false;
	public float slowMoFactor;

	void IntializeQuestionOptionsBehavior()
	{
		questionOptions = questionOptionsGameObject.GetComponent<QuestionOptions>();
		if (questionOptions == null)
			Debug.Log("Cannot find 'QuestionOptions' script");
	}

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
		IntializeQuestionOptionsBehavior();
		questionOptions.gameObject.SetActive(false);
	}

	void Update()
	{
		if (restart)
			if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
				Application.LoadLevel(Application.loadedLevel);
		if (presentOptions)
		{
			if (Time.timeScale == 1.0f)
				Time.timeScale = slowMoFactor;
			else
				Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			presentOptions = false;
		}
		if (Time.timeScale == slowMoFactor)
			currentSlowMo += Time.deltaTime;
		if (currentSlowMo > slowTimeAllowed)
		{
			currentSlowMo = 0;
			Time.timeScale = 1.0f;
			questionOptions.gameObject.SetActive(false);

		}
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

	public void PresentOptions()
	{
		if (Time.timeScale == 1.0f)
		{
			presentOptions = true;
			questionOptions.gameObject.SetActive(true);
		}
	}

	public bool ChoiceCorrect()
	{
		return false;
	}

}
