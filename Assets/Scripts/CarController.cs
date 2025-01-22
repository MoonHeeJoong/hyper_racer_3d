using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    InputHandler inputHandler;
    Camera mainCamera;

    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float acceleration = 1f;
    [SerializeField] public float deceleration = 1f;
    public float currentSpeed = 0f;

    //Can MoveX Positions
    float[] moveXPositions = new float[]{-1.5f, -0.5f, 0.5f, 1.5f};
    int currentXPositionIndex = 2;
    bool isMovingX = false;
   
    void Update()
    {

        if(inputHandler == null)
        {
            inputHandler = InputHandler.Instance;
            return;
        }

        if (inputHandler.MovementInput.magnitude > 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, 0, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed - deceleration * Time.deltaTime, 0, maxSpeed);
        }

        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        if (inputHandler.MovementInput.x > 0 && !isMovingX)
        {
            isMovingX = true;
            currentXPositionIndex = Mathf.Clamp(currentXPositionIndex + 1, 0, moveXPositions.Length - 1);
        }
        else if (inputHandler.MovementInput.x < 0 && !isMovingX)
        {
            isMovingX = true;
            currentXPositionIndex = Mathf.Clamp(currentXPositionIndex - 1, 0, moveXPositions.Length - 1);
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x = moveXPositions[currentXPositionIndex];
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.02f); //handling speed
        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMovingX = false;
        }

                


        if(mainCamera == null)
        {
            mainCamera = Camera.main;
            if(mainCamera == null)
            {
                return;
            }
        }
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.z = transform.position.z - 2f *(currentSpeed / maxSpeed);
        cameraPosition.y = transform.position.y + 10;
        mainCamera.transform.position = cameraPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gas"))
        {
            GameManager.Instance.ChargeGas();
            Destroy(other.gameObject);
        }
    }

}
