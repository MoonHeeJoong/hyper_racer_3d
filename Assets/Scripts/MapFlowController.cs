using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFlowController : MonoBehaviour
{
    Queue<GameObject> mapQueue = new Queue<GameObject>();
    public CarController car;

    public float mapInterval = 20;

    // Start is called before the first frame update
    void Start()
    {   
        //Child GameObject
        GameObject[] maps = GameObject.FindGameObjectsWithTag("Map");
        foreach (GameObject map in maps)
        {
            mapQueue.Enqueue(map);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(car == null){
            car = GameManager.Instance.car;
            if(car == null){
                return;
            }
        }

        if(car.transform.position.z > mapQueue.Peek().transform.position.z + 15){
            GameObject map = mapQueue.Dequeue();
            mapQueue.Enqueue(map);
            map.transform.position = new Vector3(0, 0, mapQueue.Peek().transform.position.z + mapInterval);
        } 
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //1
        Gizmos.DrawLine(new Vector3(1, 1, mapQueue.Peek().transform.position.z - mapInterval), new Vector3(1, 1, mapQueue.Peek().transform.position.z + mapInterval));
        //-1
        Gizmos.DrawLine(new Vector3(-1, 1, mapQueue.Peek().transform.position.z - mapInterval), new Vector3(-1, 1, mapQueue.Peek().transform.position.z + mapInterval));
        //0
        Gizmos.DrawLine(new Vector3(0, 1, mapQueue.Peek().transform.position.z - mapInterval), new Vector3(0, 1, mapQueue.Peek().transform.position.z + mapInterval));
        
    }
}
