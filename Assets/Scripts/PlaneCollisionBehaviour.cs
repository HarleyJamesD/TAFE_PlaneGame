using UnityEngine;

public class PlaneCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private string balloonTag;

    [SerializeField] private GameObject explosionVFXObj;
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag(balloonTag))
        //{
        //    GetComponent<PlaneMovement>().StartPlaneBoost();
        //    return;
        //}
        Debug.Log("Object collided with " + collision.gameObject.name);
        GameManager.instance.PlayerDead();
        Instantiate(explosionVFXObj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
