using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject balloonPopVFX;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Instantiate(balloonPopVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
