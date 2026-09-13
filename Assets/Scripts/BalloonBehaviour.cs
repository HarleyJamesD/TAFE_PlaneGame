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

            //planeMovement.StartPlaneBoost();
            return;
        }
        other.attachedRigidbody.gameObject.SetActive(false);
    }

    private void Pop()
    {
        Instantiate(balloonPopVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
