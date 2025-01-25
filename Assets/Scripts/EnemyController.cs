using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 7f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}