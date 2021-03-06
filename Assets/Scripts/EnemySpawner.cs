﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void NextWave(int waveNum);

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Text waveCount = null;

    [Header("Spawn Properties")]
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private float spawnRateModifier = 2f;
    [SerializeField] private float spawnRateMaxDev = 2f;
    [SerializeField] private float spawnRateMinDev = 0.5f;
    [SerializeField] private float spawnRateStepIncr = 0.1f;
    [SerializeField] private float spawnRateFloor = 0.2f;
    [SerializeField] private int spawnStepIncr = 5;

    public event NextWave nextWave;

    public static EnemySpawner enemySpawner;

    //[SerializeField] public int currLevel = 1;

    private float counter = 0f;
    private float nextSpawn = 0f;
    private int currWave = 1;
    private int waveSpawnNum = 5;
    private int currSpawnNum = 0;

    //TODO: desired spawning state is a weighted system that uses the provided enemy prefabs
    //      which divides the enemys into tiers based on index of the array. will spawn enemies
    //      at a calculated rate based on index, the calculation rates will change based on the 
    //      wave number

    private void Awake()
    {
        enemySpawner = this;
    }

    private void Start()
    {
        nextSpawn = spawnRateModifier * UnityEngine.Random.Range(spawnRateMinDev, spawnRateMaxDev);
        waveCount.text = currWave.ToString();
    }


    // Update is called once per frame
    void Update()
    {

        if (currSpawnNum == waveSpawnNum)
        {
            ZombieController[] children = GetComponentsInChildren<ZombieController>();
            if (children.Length == 0)
            {
                currSpawnNum = 0;
                currWave++;
                waveCount.text = currWave.ToString();
                if (nextWave != null)
                    nextWave(currWave);
                // TODO: WAVE indication animation or noise
                waveSpawnNum += spawnStepIncr;
                spawnRateModifier -= spawnRateStepIncr;
                if (spawnRateModifier < spawnRateFloor)
                    spawnRateModifier = spawnRateFloor;
            }

        }
        else
        {
            if (counter >= nextSpawn)
            {
                Spawn();
                currSpawnNum++;
                nextSpawn = spawnRateModifier * UnityEngine.Random.Range(spawnRateMinDev, spawnRateMaxDev);
                counter = 0f;
            }
            else
                counter += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        float location = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        Instantiate(enemyPrefabs[0], new Vector3(transform.position.x, transform.position.y + location, -0.1f), Quaternion.identity, transform);
        //TODO: CALCULATE Z LOCATION FOR LAYERING
    }
}
