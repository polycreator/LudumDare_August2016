using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
	public Transform enemy;
	public Transform[] spawnPoints;

	const float EnemyInterval = 7.0f;
	const float EnemyMoveMpS = 3.0f;
	const int MaxEnemies = 5;

	float nextEnemyTime;
	List<GameObject> enemies = new List<GameObject>();
	List<GameObject> enemyNextWP = new List<GameObject>();

	void Start () {
		nextEnemyTime = Time.time + 1.0f;
	}

	private GameObject pickRandomSpawnPoint() {
		return spawnPoints[0].gameObject;
	}

	private void spawnEnemyAt(GameObject spawnPoint) {
		var nextEnemy = ((Instantiate(enemy, spawnPoint.transform.localPosition, Quaternion.identity)) as Transform).gameObject;
		enemies.Add(nextEnemy);
		var spawnProps = spawnPoint.GetComponent<EnemyWaypoint>();
		int ix = Mathf.RoundToInt(Random.Range(0, (float)spawnProps.next.Length));
		enemyNextWP.Add(spawnProps.next[ix]);
	}

	private void selectNextWaypoint(int enemyIndex) {
		var curTarget = enemyNextWP[enemyIndex];
		if (curTarget) {
			var wpProps = curTarget.GetComponent<EnemyWaypoint>();
			if (wpProps.next != null) {
				int ix = Mathf.RoundToInt(Random.Range(0, (float)wpProps.next.Length));
				enemyNextWP[enemyIndex] = wpProps.next[ix];
			}
			else {
				// TREASURE!
			}
		}
	}

	void Update () {
		// spawn next when it's time
		if (Time.time >= nextEnemyTime) {
			spawnEnemyAt(pickRandomSpawnPoint());
			nextEnemyTime += EnemyInterval;
		}

		// move all
		for (int x = 0; x < enemies.Count; ++x) {
			var enemy = enemies[x];
			var nextWP = enemyNextWP[x];

			var distance = (nextWP.transform.localPosition - enemy.transform.localPosition);

			if (distance.magnitude > 0.1) {
				var dir = distance.normalized;
				var movement = dir * (EnemyMoveMpS * Time.deltaTime);
				enemy.transform.localPosition += movement;
			}
			else {
				enemy.transform.localPosition = nextWP.transform.localPosition;
				selectNextWaypoint(x);
			}

		}
	}
	
}