using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject balloonPopVFX;


    private void OnTriggerEnter(Collider other)
    {
        Pop();
        gameObject.SetActive(false);
        if (other.GetComponentInParent<PlaneMovement>())
        {
            other.GetComponentInParent<PlaneMovement>().StartPlaneBoost();
            return;
        }
        other.attachedRigidbody.gameObject.SetActive(false);
    }

    private void Pop()
    {
        Instantiate(balloonPopVFX, transform.position, Quaternion.identity);
        GameManager.instance.onBalloonPop?.Invoke();
        Destroy(gameObject);
    }
}
