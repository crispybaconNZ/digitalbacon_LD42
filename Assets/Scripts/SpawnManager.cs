using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to update as this is really the world manager now
public class SpawnManager : MonoBehaviour {
    // Script to manage spawn things

    [SerializeField] private int crate_spawn_rate = 5;    // number of seconds between box spawns
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] public Rect chuteBounds;
    [SerializeField] private UIManager uiManager;
    private AudioSource pointScored, music;
    private List<Crate> crates = new List<Crate>();
    Coroutine crateSpawner = null;
    private int score;

    // crates won't spawn in the exact spawnLocation every time; this variable determines how far to vary the Y position by
    [SerializeField] private float spawnYDeviation; 

    IEnumerator CrateSpawnRoutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

            Vector3 actualSpawn = spawnLocation;
            actualSpawn.y += Random.Range(-spawnYDeviation, spawnYDeviation);

            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 90.0f) + 45.0f);
            Crate newCrate = Instantiate(cratePrefab, actualSpawn, rotation).GetComponent<Crate>();
            newCrate.spawnManager = this;
            crates.Add(newCrate);
        }
    }

    public void SetMusicPlayer(bool on = true) {
        if (on) {
            music.Play();        
        } else {
            music.Stop();
        }
    }

    void Start() {
        crateSpawner = StartCoroutine(CrateSpawnRoutine());
        AudioSource[] audioSources = GetComponents<AudioSource>();

        pointScored = audioSources[0];
        music = audioSources[1];        
        score = 0;
    }

    public void StopCrateSpawning() {
        StopCoroutine(crateSpawner);
        SetMusicPlayer(false);
    }

    public void CrateDelivered(Crate crate) {
        pointScored.Play();
        score += crate.points;
        Destroy(crate.gameObject);
        uiManager.SetScore(score);       
    }
}
