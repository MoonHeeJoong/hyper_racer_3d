using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    CarController car;
    
    public float spawnInterval = 20;
    int spawnCount = 0;
    
    float[] enemyXPositions = new float[]{-1.5f, -0.5f, 0.5f, 1.5f};


    void Update(){
        if(GameManager.Instance.gameState != GAMESTATE.PLAYING){
            spawnInterval = 20;
            spawnCount = 0;
            return;
        }

        if(car == null){
            car = GameManager.Instance.car;
            if(car == null){
                return;
            }
        }

        if(car.transform.position.z > GameManager.Instance.lastEnemySpawnZ + spawnInterval){
            GameManager.Instance.lastEnemySpawnZ = car.transform.position.z;
            SpawnEnemy();
        }

        DestoryEnemy();
    }

    void SpawnEnemy(){
        int randomIndex = Random.Range(0, enemyXPositions.Length);
        Vector3 spawnPosition = new Vector3(enemyXPositions[randomIndex], 0, GameManager.Instance.lastEnemySpawnZ + 20);
        //GameManager.Instance.objectList.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.Euler(0, 180, 0)));
        GameManager.Instance.objectList.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity));
    }
    
    void DestoryEnemy(){
        foreach(GameObject obj in GameManager.Instance.objectList){
            if(obj == null){
                continue;
            }
            if(obj.transform.position.z < car.transform.position.z - 10){
                if(obj.tag == "Enemy"){
                    Destroy(obj);
                }
            }
        }
    }

    
}