using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
