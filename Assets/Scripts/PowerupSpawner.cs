using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs = null;
    [SerializeField] private int[] spawnFrequency = null;

    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 0f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    // Start is called before the first frame update
    private void Start()
    {
        EnemySpawner.enemySpawner.nextWave += CheckSpawnNewPowerup;
    }

    private void OnDisable()
    {
        EnemySpawner.enemySpawner.nextWave -= CheckSpawnNewPowerup;
    }

    private void CheckSpawnNewPowerup(int waveNum)
    {
        for(int i = 0; i < spawnFrequency.Length; i++)
        {
            if (waveNum % spawnFrequency[i] == 0)
                SpawnPowerup(powerupPrefabs[i]);
        }
    }

    private void SpawnPowerup(GameObject newWeapon)
    {
        Debug.Log("spawning powerup");
        float x = UnityEngine.Random.Range(minX, maxX);
        float y = UnityEngine.Random.Range(minY, maxY);
        Instantiate(newWeapon);
        newWeapon.transform.position = new Vector2(x, y);
    }
}
