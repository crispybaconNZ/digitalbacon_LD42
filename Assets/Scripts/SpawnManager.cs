﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    // Script to manage spawn things

    [SerializeField] private int crate_spawn_rate = 5;    // number of seconds between box spawns
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private Vector3 spawnLocation;

    // crates won't spawn in the exact spawnLocation every time; this variable determines how far to vary the Y position by
    [SerializeField] private float spawnYDeviation; 

    IEnumerator CrateSpawnRoutine() {
        while (true) {
            Vector3 actualSpawn = spawnLocation;
            spawnLocation.y += Random.Range(-0.4f, 0.4f);

            yield return new WaitForSeconds(crate_spawn_rate);
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 90.0f) + 45.0f);
            Instantiate(cratePrefab, actualSpawn, rotation);
        }
    }

    void Start() {
        StartCoroutine(CrateSpawnRoutine());    
    }

}
