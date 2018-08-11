using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    // Script to manage spawn things

    [SerializeField] private int crate_spawn_rate = 5;    // number of seconds between box spawns
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private Vector3 spawnLocation;
    public List<Crate> crates = new List<Crate>();

    // crates won't spawn in the exact spawnLocation every time; this variable determines how far to vary the Y position by
    [SerializeField] private float spawnYDeviation; 

    IEnumerator CrateSpawnRoutine() {
        while (true) {
            yield return new WaitForSeconds(crate_spawn_rate);

            Vector3 actualSpawn = spawnLocation;
            spawnLocation.y += Random.Range(-spawnYDeviation, spawnYDeviation);

            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 90.0f) + 45.0f);
            crates.Add(Instantiate(cratePrefab, actualSpawn, rotation).GetComponent<Crate>());
        }
    }

    void Start() {
        StartCoroutine(CrateSpawnRoutine());    
    }

    public void StopCrateSpawning() {
        StopCoroutine(CrateSpawnRoutine());
    }
}
