using UnityEngine;

public class CubeGravity : MonoBehaviour
{
    public Transform sphere;
    public float gravity = 0.01f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 gravityDirection = (transform.position  - sphere.position ).normalized;

        rb.AddForce(- gravityDirection * gravity, ForceMode.Acceleration);
    }

}
