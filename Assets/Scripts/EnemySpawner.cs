using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private float spawnRateModifier = 2f;
    [SerializeField] private float spawnRateMaxDev = 2f;
    [SerializeField] private float spawnRateMinDev = 0.5f;
    //[SerializeField] public int currLevel = 1;

    private float counter = 0f;
    private float nextSpawn;


    //TODO: desired spawning state is a weighted system that uses the provided enemy prefabs
    //      which divides the enemys into tiers based on index of the array. will spawn enemies
    //      at a calculated rate based on index, the calculation rates will change based on the 
    //      wave number

    private void Start()
    {
        nextSpawn = spawnRateModifier * UnityEngine.Random.Range(spawnRateMinDev, spawnRateMaxDev);
    }


    // Update is called once per frame
    void Update()
    {
        if (counter >= nextSpawn)
        {
            Spawn();
            nextSpawn = spawnRateModifier * UnityEngine.Random.Range(spawnRateMinDev, spawnRateMaxDev);
            counter = 0f;
        }
        else
            counter += Time.deltaTime;
    }

    private void Spawn()
    {
        float location = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        Instantiate(enemyPrefabs[0], new Vector3(transform.position.x, transform.position.y + location), Quaternion.identity, transform);
        //TODO: CALCULATE Z LOCATION FOR LAYERING
    }
}
