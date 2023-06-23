using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Car", menuName = "Vehicles/New Car")]

public class Car : ScriptableObject
{
    public enum CarClass{
        SportCar,
        Pickup,
        Truck
    }
    public CarClass carClass;
    public GameObject carPrefab;
    public float maxForwardSpeed = 10f;
    public float maxReverseSpeed = -5f;
    public float acceleration = 1f;
    public float turnVelocity = 1f;
    public float brakeForce = 1f;

}
