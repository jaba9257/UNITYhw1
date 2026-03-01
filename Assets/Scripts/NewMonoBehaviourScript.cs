using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float ms = 10f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + Vector3.forward * ms * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
