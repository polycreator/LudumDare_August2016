using UnityEngine;
using System.Collections.Generic;

enum EnemyDirection {
	Forward,
	Reverse
}

public class EnemyController : MonoBehaviour {
	public Transform enemy;
	public Transform[] spawnPoints;

	const float EnemyInterval = 7.0f;
	const float EnemyMoveMpS = 3.0f;

	float nextEnemyTime;
	List<GameObject> enemies = new List<GameObject>();
	List<GameObject> enemyNextWP = new List<GameObject>();
	List<EnemyDirection> enemyDirection = new List<EnemyDirection>();

	void Start () {
		nextEnemyTime = Time.time + 1.0f;
	}

	private GameObject pickRandomSpawnPoint() {
		return spawnPoints[0].gameObject;
	}

	private void spawnEnemyAt(GameObject spawnPoint) {
		var nextEnemy = ((Instantiate(enemy, spawnPoint.transform.localPosition, Quaternion.identity)) as Transform).gameObject;
		enemies.Add(nextEnemy);
		enemyDirection.Add(EnemyDirection.Forward);
		var spawnProps = spawnPoint.GetComponent<EnemyWaypoint>();
		enemyNextWP.Add(spawnProps.next);
	}

	private void selectNextWaypoint(int enemyIndex) {
		var curTarget = enemyNextWP[enemyIndex];
		if (curTarget) {
			var wpProps = curTarget.GetComponent<EnemyWaypoint>();
			var curDir = enemyDirection[enemyIndex];
			if (curDir == EnemyDirection.Forward) {
				if (wpProps.next != null) {
					enemyNextWP[enemyIndex] = wpProps.next;
				}
				else {
					enemyDirection[enemyIndex] = EnemyDirection.Reverse;
					enemyNextWP[enemyIndex] = wpProps.previous;
				}
			}
			else {
				if (wpProps.previous != null) {
					enemyNextWP[enemyIndex] = wpProps.previous;
				}
				else {
					enemyDirection[enemyIndex] = EnemyDirection.Forward;
					enemyNextWP[enemyIndex] = wpProps.next;
				}
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