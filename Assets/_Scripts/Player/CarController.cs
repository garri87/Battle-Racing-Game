using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public List<Car> carScriptables;
    public Car carScriptable;

    public Car.CarClass carClass;
    public float maxForwardSpeed = 10f;
    public float maxReverseSpeed = -5f;
    public float acceleration = 1f;
    public float turnVelocity = 1f;
    public float brakeForce = 1f;
    public float currentSpeed = 0f;

    public KeyCode accelerateKey = KeyCode.W;
    public KeyCode brakeKey = KeyCode.S;

    public KeyCode fireKey = KeyCode.Space;


    public float horizontalInput, verticalInput;

    private Rigidbody rigidBody;


    private CarInventory _carInventory;


    private void Awake()
    {
        carScriptable = carScriptables[Random.Range(0,carScriptables.Count)];
        GetScriptable(carScriptable);
        rigidBody = GetComponent<Rigidbody>();
        transform.position += Vector3.up;
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Mathf.RoundToInt(currentSpeed) != 0)
        {
            if (currentSpeed > 0)
            {
                transform.Rotate(Vector3.up * horizontalInput * turnVelocity);
            }
            else
            {
                transform.Rotate(Vector3.up * -horizontalInput * turnVelocity);
            }

        }

        rigidBody.AddForce(transform.forward * currentSpeed, ForceMode.Impulse);

        if (currentSpeed > maxForwardSpeed)
        {
            currentSpeed = maxForwardSpeed;
        }
        if (currentSpeed < maxReverseSpeed)
        {
            currentSpeed = maxReverseSpeed;
        }


        if (Input.GetKey(accelerateKey))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {

            if (currentSpeed < 0 && !Input.GetKey(brakeKey))
            {
                currentSpeed = 0;
            }
            currentSpeed -= acceleration * Time.deltaTime;
        }

        if (Input.GetKey(brakeKey))
        {
            currentSpeed -= acceleration * brakeForce * Time.deltaTime;
        }

    }

    public void GetScriptable(Car scriptableObject)
    {
        carClass = scriptableObject.carClass;
        maxForwardSpeed = scriptableObject.maxForwardSpeed;
        maxReverseSpeed = scriptableObject.maxReverseSpeed;
        acceleration = scriptableObject.acceleration;
        turnVelocity = scriptableObject.turnVelocity;
        brakeForce = scriptableObject.brakeForce;
    }
}
