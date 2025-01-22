using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATE{
    IDLE,
    PLAYING,
}

public class GameManager : Singleton<GameManager>
{
    public GameObject carPrefab;
    public GameObject mapsPrefab;

    //ui controller
    public UIManager uiManager;
    public GameUIController gameUIController;
    

    [NonSerialized] public CarController car;

    
    public GAMESTATE gameState = GAMESTATE.IDLE;

    int currentGas = 0;
    int maxGas = 100;
    int chargeGas = 20;
    //int consumeGasPerSecond = 1;
    int consumeGasPerZ = 10;
    float lastGasConsumeZ = 0;
    public float lastSpawnZ = 0; //spawner에서 사용

    float timer = 0;


    //관리를 위한 리스트
    public List<GameObject> objectList = new List<GameObject>();
    
    void Start(){
        //StartGame();
    }


    public void StartGame(){
        SpawnMap();
        SpawnCar();

        currentGas = maxGas;
        lastGasConsumeZ = 0;

        uiManager.ShowGameUI();
        gameUIController.UpdateGas(currentGas, maxGas);

        gameState = GAMESTATE.PLAYING;
    }

    public void EndGame(){
        DestroyAllObject();

        uiManager.ShowGameOverUI();

        gameState = GAMESTATE.IDLE;
    }

    void RestartGame(){}

    void PauseGame(){}


    void SpawnMap(){
        GameObject mapObject = Instantiate(mapsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        objectList.Add(mapObject);
    }

    void DestroyAllObject(){
        foreach(GameObject obj in objectList){
            Destroy(obj);
        }
        objectList.Clear();
    }

    void SpawnCar(){
        GameObject carObject = Instantiate(carPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        car = carObject.GetComponent<CarController>();
        objectList.Add(carObject);
    }

    void Update(){
        if(gameState == GAMESTATE.PLAYING){
            timer += Time.deltaTime;
            // if(timer > 1){
            //     timer = 0;
            //     ConsumeGas();
            // }

            if(car.transform.position.z > lastGasConsumeZ + consumeGasPerZ){
                lastGasConsumeZ = car.transform.position.z;
                ConsumeGas();
            }
            

            if(currentGas == 0){
                EndGame();
            }
        }
    }    

    void ConsumeGas(){
        //currentGas = Mathf.Clamp(currentGas - consumeGasPerSecond, 0, maxGas);
        currentGas = Mathf.Clamp(currentGas - consumeGasPerZ, 0, maxGas);

        gameUIController.UpdateGas(currentGas, maxGas);
    }

    public void ChargeGas(){
        currentGas = Mathf.Clamp(currentGas + chargeGas, 0, maxGas);

        gameUIController.UpdateGas(currentGas, maxGas);
    }
}
