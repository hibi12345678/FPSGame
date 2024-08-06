using UnityEngine;

public class CubeGravity : MonoBehaviour
{
    
    public float gravity = 0.01f;
    private Rigidbody rb;
    Transform planet;
    GameObject sphere;
    private void Start()
    {
        sphere = GameObject.Find("Planet");
        planet = sphere.transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 gravityDirection = (transform.position  - planet.position ).normalized;

        rb.AddForce(- gravityDirection * gravity, ForceMode.Acceleration);
    }

}
