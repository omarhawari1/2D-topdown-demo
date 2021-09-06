using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField]private float spawnTime;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private List<Transform> spawnPoints = new List<Transform>();

    private int spawnPointNumber = 0;
    private float elapsedTime = 0.0f;

    private void Update() 
    {
        elapsedTime += Time.deltaTime;
        if(!isServer){return;}
        foreach(Transform spawnPoint in spawnPoints)
        {
            spawnPointNumber = Random.Range(0, spawnPoints.Count);
        }
        if(elapsedTime > spawnTime)
        {
            elapsedTime = 0;
            ServerSpawnEnemys();
        }
    }

    [Server]
    private void ServerSpawnEnemys()
    {
        GameObject EnemyInstance = Instantiate(enemyPrefab, spawnPoints[spawnPointNumber].position, enemyPrefab.transform.rotation);
        NetworkServer.Spawn(EnemyInstance);
    }
}
