using UnityEngine;

public class PlaneCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private string balloonTag;

    [SerializeField] private GameObject explosionVFXObj;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object collided with " + collision.gameObject.name);
        GameManager.instance.PlayerDead();
        HealthCount.instance.RemoveLife();
        Instantiate(explosionVFXObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
