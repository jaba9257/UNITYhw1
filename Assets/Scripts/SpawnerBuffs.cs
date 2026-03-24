using UnityEngine;
using System.Collections;

public class BuffsSpawner : MonoBehaviour
{
    private Transform player;
    public GameObject[] spawnPrefabs;
    public float spawnDistance = 25f;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;
    public float laneWidth = 2f;
    public int laneCount = 3;

    void Start()
    {
        player = gameObject.transform;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            Spawn();
        }
    }

    void Spawn()
    {
        if (spawnPrefabs.Length == 0) return;
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
        Vector3 pos = player.position + Vector3.forward * spawnDistance;
        pos.y = 0.5f;
        int lane = Random.Range(0, laneCount);
        float x = (lane - (laneCount / 2)) * laneWidth;
        pos.x = x;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}