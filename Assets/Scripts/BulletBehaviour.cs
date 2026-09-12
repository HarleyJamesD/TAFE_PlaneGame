using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 5f;
    private Rigidbody rb;

    [SerializeField] private GameObject explosionVFXObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(BulletLifetime());
    }
    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(bulletLifetime);
        ResetBullet();
    }

    private void ResetBullet()
    {
        Instantiate(explosionVFXObj, transform.position, Quaternion.identity);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.transform.position = Vector3.zero;
        gameObject.SetActive(false);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit collider" + collision.gameObject.name);
        Instantiate(explosionVFXObj, transform.position, Quaternion.identity);
        ResetBullet();
    }
}
