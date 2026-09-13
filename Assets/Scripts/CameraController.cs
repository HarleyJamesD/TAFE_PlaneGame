using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraTargetOffsetDistance;
    private Vector3 cameraTargetOffset;

    [SerializeField] private Transform plane;
    private PlaneMovement planeMovement;
    [SerializeField] private Transform cameraDefaultPos;
    [SerializeField] private float cameraFollowSpeed = 1f;
    [SerializeField] private float cameraRotateSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.onPlayerDeath += OnPlayerDeath;
        planeMovement = plane.GetComponent<PlaneMovement>();
    }

    bool playerDead;
    void OnPlayerDeath()
    {
        playerDead = true;
    }

    [SerializeField] private float bufferZone = 0.05f;
    void Update()
    {
        if(!playerDead) CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 planePos = plane.position;
        cameraTargetOffset = planePos + (-plane.TransformDirection(Vector3.forward) * cameraTargetOffsetDistance);

        Quaternion targetRotation = Quaternion.LookRotation(cameraTargetOffset - transform.position);
        Quaternion rotationToPlane = Quaternion.LookRotation(planePos - transform.position);
        Quaternion defaultPosToTargetRot = Quaternion.LookRotation(cameraTargetOffset - cameraDefaultPos.position);

        //If plane is not turning (pitch is 0) lerp to camera default pos
        if (planeMovement.PitchAmount == 0) 
        {
            transform.position = Vector3.Lerp(transform.position, cameraDefaultPos.position, cameraFollowSpeed * Time.deltaTime);
        }
        else
        {
            //When plane turning follow plane path
            transform.position = Vector3.Lerp(transform.position, planePos, cameraFollowSpeed * Time.deltaTime);
        }

        //Smoothly rotate towards target rotation 
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraRotateSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(cameraTargetOffset, 10f);
    }
}
