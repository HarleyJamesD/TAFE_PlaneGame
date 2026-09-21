using System;
using System.Collections;
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
    

    void Start()
    {
        GameManager.instance.onPlayerDeath += OnPlayerDeath;
        GameManager.instance.onGamePause += OnPause;
        planeMovement = plane.GetComponent<PlaneMovement>();
    }

    bool isPaused;

    [SerializeField] private float pauseScreenVerticalOffset = -3f;
    [SerializeField] private AnimationCurve tweenToPauseScreen;
    [SerializeField] private float pauseDuration = 1f;
    [SerializeField] private float pauseHeightFromPlane = 10f;
    Coroutine pauseCoroutine;
    private void OnPause(bool isGamePause)
    {
        isPaused = isGamePause;
        if (isGamePause)
        {
            if (pauseCoroutine != null) StopCoroutine(pauseCoroutine);
            pauseCoroutine = StartCoroutine(TweenToPauseCamera());
        } else
        {
            if (pauseCoroutine != null)
            {
                StopCoroutine(pauseCoroutine);
                pauseCoroutine = null;
            }
        }
        

        IEnumerator TweenToPauseCamera()
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;

            yield return null;

            Quaternion targetRotation = Quaternion.LookRotation(-plane.up, -plane.forward);
            Vector3 targetPosition = plane.position + plane.up * pauseHeightFromPlane + targetRotation * Vector3.up * pauseScreenVerticalOffset;
            

            float elapsed = 0f;

            while (elapsed < pauseDuration)
            {
                elapsed += Time.unscaledDeltaTime;

                float t = elapsed / pauseDuration;
                float tween = tweenToPauseScreen.Evaluate(t);

                transform.position = Vector3.Lerp(startPosition, targetPosition, tween);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, tween);

                yield return null;
            }

            transform.position = targetPosition;
            transform.rotation = targetRotation;

            //Testing
            Vector3 localPos = plane.InverseTransformPoint(transform.position);
            Quaternion localRot = Quaternion.Inverse(plane.rotation) * transform.rotation;
            Debug.Log($"Camera relative to plane: pos {localPos}, rot {localRot.eulerAngles}");
            yield return null;
        }
    }

    bool playerDead;
    void OnPlayerDeath()
    {
        playerDead = true;
    }

    [SerializeField] private float bufferZone = 0.05f;
    void Update()
    {
        if(!playerDead&&!isPaused) CameraFollow();
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
            //Lerp between the two points by a factor of cameraFollowSpeed adjusted by Time.deltaTime
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
