using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
	public Transform enemy;
	public Transform[] spawnPoints;

	const float EnemyInterval = 5.0f;
	const float EnemyMoveMpS = 1.0f;

	float nextEnemyTime;
	List<GameObject> enemies = new List<GameObject>();
	List<GameObject> enemyNextWP = new List<GameObject>();

	void Start () {
		nextEnemyTime = Time.time + 2.0f;
	}

	private GameObject pickRandomSpawnPoint() {
		return spawnPoints[0].gameObject;
	}

	
	
	void Update () {
		// spawn next when it's time
		if (Time.time >= nextEnemyTime) {
			var spawnPoint = pickRandomSpawnPoint();
			var nextEnemy = Instantiate(enemy, spawnPoint.transform.localPosition, Quaternion.identity) as GameObject;
			enemies.Add(nextEnemy);
			var spawnProps = spawnPoint.GetComponent<EnemyWaypoint>();
			print(nextEnemy);
			print(spawnProps.next);
			enemyNextWP.Add(spawnProps.next);
			nextEnemyTime += EnemyInterval * 100.0f;
		}

		// move all
		for (int x = 0; x < enemies.Count; ++x) {
			var enemy = enemies[x];
			var nextWP = enemyNextWP[x];

			var dir = (nextWP.transform.localPosition - enemy.transform.localPosition).normalized;
			var movement = dir * (EnemyMoveMpS * Time.deltaTime);
			enemy.transform.localPosition += movement;
		}
	}
}
