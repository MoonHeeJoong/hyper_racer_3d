using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject gasPrefab;
    CarController car;

    
    public float spawnInterval = 15;
    int spawnCount = 0;

    //Gas x Positions
    float[] gasXPositions = new float[]{-1.5f, -0.5f, 0.5f, 1.5f};


    void Update(){
        if(GameManager.Instance.gameState != GAMESTATE.PLAYING){
            spawnInterval = 15;
            spawnCount = 0;
            return;
        }

        if(car == null){
            car = GameManager.Instance.car;
            if(car == null){
                return;
            }
        }

        if(car.transform.position.z > GameManager.Instance.lastGasSpawnZ + spawnInterval){
            GameManager.Instance.lastGasSpawnZ = car.transform.position.z;
            SpawnGas();
        }

        DestoryGas();

        // //난이도업
        // if(spawnCount > 4){
        //     spawnInterval++;
        //     spawnCount = 0;
        // }
        // else{
        //     spawnCount++;
        // }
    }

    void SpawnGas(){
        int randomIndex = Random.Range(0, gasXPositions.Length);
        Vector3 spawnPosition = new Vector3(gasXPositions[randomIndex], 0, GameManager.Instance.lastGasSpawnZ + 20);
        GameManager.Instance.objectList.Add(Instantiate(gasPrefab, spawnPosition, Quaternion.identity));
    }
    
    void DestoryGas(){
        foreach(GameObject obj in GameManager.Instance.objectList){
            if(obj == null){
                continue;
            }
            if(obj.transform.position.z < car.transform.position.z - 10){
                if(obj.tag == "Gas"){
                    Destroy(obj);
                }
            }
        }
    }
}
