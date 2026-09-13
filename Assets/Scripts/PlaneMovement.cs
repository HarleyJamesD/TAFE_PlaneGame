using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed = 10f;
    [SerializeField] private float forwardBoostSpeed = 70f;
    [SerializeField] private float boostDuration = 2f;
    private float currentPlaneSpeed;
    [SerializeField] private float pitchRotationSpeed = 10f;
    [SerializeField] private float rollRotationSpeed = 10f;
    

    [SerializeField] private bool invertedPitch = true;
    private int invertPitch;

    private InputSystem_Actions inputActions;

    private InputAction flightControllerInput;
    private InputAction planeBoost;
    private InputAction planeShoot;

    public float PitchAmount { get { return pitchAmount; } }
    private float pitchAmount;

    [SerializeField] private float trailBoostWidth = 1.6f;
    [SerializeField] private TrailRenderer leftTrail;
    private TrailRenderer leftTrailBoost;
    [SerializeField] private TrailRenderer rightTrail;
    private TrailRenderer rightTrailBoost;

    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform gun;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        flightControllerInput = inputActions.Player.FlightDirection;
        planeBoost = inputActions.Player.Boost;
        planeBoost.performed += PlaneBoost;
        planeShoot = inputActions.Player.Shoot;
        planeShoot.performed += PlaneShoot;

        currentPlaneSpeed = forwardMoveSpeed;
        
    }

    private void OnEnable()
    {
        flightControllerInput.Enable();
        planeBoost.Enable();
        planeShoot.Enable();
    }

    private void OnDisable()
    {
        flightControllerInput.Disable();
        planeBoost.Disable();
        planeShoot.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invertPitch = invertedPitch ? -1 : 1; //For AB testing
        leftTrailBoost = leftTrail.transform.GetChild(0).GetComponent<TrailRenderer>();
        rightTrailBoost = rightTrail.transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    private void PlaneBoost(InputAction.CallbackContext obj)
    {
        StartPlaneBoost();
    }

    Coroutine boostRoutine;
    public void StartPlaneBoost()
    {
        //Check if player already boosting  
        if (boostRoutine != null)
        {
            StopCoroutine(boostRoutine); // Cancel current boost routine to avoid overwriting behaviour
        }
        boostRoutine = StartCoroutine(PlaneBoostRoutine());

        IEnumerator PlaneBoostRoutine()
        {
            currentPlaneSpeed = forwardBoostSpeed;
            ToggleTrails(true);
            yield return new WaitForSeconds(boostDuration);
            currentPlaneSpeed = forwardMoveSpeed;
            ToggleTrails(false);
        }
    }

    private void ToggleTrails(bool isBoosting)
    {
        leftTrail.emitting = !isBoosting;
        rightTrail.emitting = !isBoosting;
        leftTrailBoost.emitting = isBoosting;
        rightTrailBoost.emitting = isBoosting;
    }

    private void PlaneShoot(InputAction.CallbackContext context)
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = gun.position;
        bullet.transform.rotation = gun.rotation;
        bullet.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,0,-currentPlaneSpeed * Time.deltaTime));
        pitchAmount = invertPitch * flightControllerInput.ReadValue<Vector2>().y;
        transform.Rotate(Vector3.right, pitchRotationSpeed * pitchAmount * Time.deltaTime);
        float rollAmountDir = flightControllerInput.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.forward, pitchRotationSpeed * rollAmountDir * Time.deltaTime);
    }
}
