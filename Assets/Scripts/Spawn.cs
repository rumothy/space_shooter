using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
	public GameObject enemy;
	public Transform enemySpawn;
	private bool spawned = false;

	void SpawnEnemies()
	{
		Instantiate(enemy, enemySpawn.position, enemySpawn.rotation);
//		Debug.Log("Spawned Enemy");
		spawned = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Boundary" && !spawned)
			SpawnEnemies();
	}
}

