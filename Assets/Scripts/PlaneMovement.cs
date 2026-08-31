using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed = 10f;
    [SerializeField] private float pitchRotationSpeed = 10f;
    [SerializeField] private float rollRotationSpeed = 10f;

    [SerializeField] private bool invertedPitch = true;
    private int invertPitch;

    private InputSystem_Actions inputActions;

    private InputAction flightControllerInput;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        flightControllerInput = inputActions.Player.FlightDirection;
    }

    private void OnEnable()
    {
        flightControllerInput.Enable();
    }

    private void OnDisable()
    {
        flightControllerInput.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        invertPitch = invertedPitch ? -1 : 1; //For AB testing
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,0,-forwardMoveSpeed*Time.deltaTime));
        float pitchAmountDir = invertPitch * flightControllerInput.ReadValue<Vector2>().y;
        transform.Rotate(Vector3.right, pitchRotationSpeed * pitchAmountDir * Time.deltaTime);
        float rollAmountDir = flightControllerInput.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.forward, pitchRotationSpeed * rollAmountDir * Time.deltaTime);
    }
}
