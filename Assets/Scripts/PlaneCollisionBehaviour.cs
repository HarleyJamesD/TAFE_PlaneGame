using UnityEngine;

public class PlaneCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFXObj;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object collided with " + collision.gameObject.name);
        GameManager.instance.PlayerDead();
        Instantiate(explosionVFXObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
