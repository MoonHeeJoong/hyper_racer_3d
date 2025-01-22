using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject gasPrefab;
    CarController car;

    float lastSpawnZ = 0;
    public float spawnInterval = 15;

    //Gas x Positions
    float[] gasXPositions = new float[]{-1.5f, -0.5f, 0.5f, 1.5f};


    void Update(){
        if(GameManager.Instance.gameState != GAMESTATE.PLAYING){
            return;
        }

        if(car == null){
            car = GameManager.Instance.car;
            if(car == null){
                return;
            }
        }

        if(car.transform.position.z > lastSpawnZ + spawnInterval){
            lastSpawnZ = car.transform.position.z;
            SpawnGas();
        }
    }

    void SpawnGas(){
        int randomIndex = Random.Range(0, gasXPositions.Length);
        Vector3 spawnPosition = new Vector3(gasXPositions[randomIndex], 0.5f, lastSpawnZ + 20);
        GameManager.Instance.objectList.Add(Instantiate(gasPrefab, spawnPosition, Quaternion.identity));
    }
    
}
