using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Types")]
    public ObstacleData[] obstacleTypes;

    [Header("Spawn Settings")]
    public Transform player;
    public float spawnDistance = 20f;
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 7f;

    private List<GameObject> activeObstacles = new List<GameObject>();
    private float nextSpawnZ;
    private float lastSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistance;
        lastSpawnZ = player.position.z;
    }

    void Update()
    {
        if (player.position.z > lastSpawnZ + minSpawnInterval) //Если мы проехали достаточно много спауним еще
        {
            TrySpawnObstacle(); 
        }
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if(activeObstacles[i] == null) continue;
            if (activeObstacles[i].transform.position.z < player.position.z - 10f) //Если объект слишком далеко
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }
    }

    void TrySpawnObstacle()
    {
        float currentZ = player.position.z + spawnDistance; //выбираем точку спауна
        if (currentZ >= nextSpawnZ) 
        {
            SpawnObstacle(currentZ);
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            nextSpawnZ = currentZ + interval;
            lastSpawnZ = player.position.z;
        }
    }

    void SpawnObstacle(float zPos)
    {
        ObstacleData selectedData = obstacleTypes[Random.Range(0, obstacleTypes.Length)]; //выбираем тип препядствия
        int lane = Random.Range(-1, 2); // выбираем линию
        float xPos = lane * player.GetComponent<PlayerController>().laneWidth;
        GameObject newObstacle = GameObject.CreatePrimitive(PrimitiveType.Cube); //создаем куб с данными мне параметрами
        newObstacle.transform.position = new Vector3(xPos, 0f, zPos); //выбираем позицию
        newObstacle.transform.localScale = selectedData.size;
        Renderer renderer = newObstacle.GetComponent<Renderer>();
        renderer.material.color = selectedData.color; //задаем красный цвет
        Collider collider = newObstacle.GetComponent<Collider>();
        collider.isTrigger = true; //включаем тригер
        Obstacle obstacleComp = newObstacle.AddComponent<Obstacle>();
        obstacleComp.data = selectedData; //выбираем пресет для создания объекта
        activeObstacles.Add(newObstacle);
    }
}