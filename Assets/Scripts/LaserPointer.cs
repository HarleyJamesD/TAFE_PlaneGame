using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float distance = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();   
    }

    [SerializeField] private LayerMask mask;
    Vector3 tempPos;
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        Vector3 rayDirection = transform.TransformDirection(-Vector3.forward);
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, rayDirection, out rayHit, distance, mask))
        {
            tempPos = rayHit.point;
            lineRenderer.SetPosition(1, rayHit.point);
            return;
        }
        lineRenderer.SetPosition(1, transform.position + transform.TransformDirection(-Vector3.forward) * distance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(tempPos, 1f);
    }
}
