using UnityEngine;

public class Belt : MonoBehaviour
{
    private float speed = 10.0f;
        
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickable"))
        {
            if (collision.transform.parent == transform)
            {
                collision.transform.SetParent(null);
            }
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 currentVelocity = rb.linearVelocity;
                Vector3 forwardDirection = transform.forward.normalized;
                rb.linearVelocity = forwardDirection * speed;
            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickable"))
        {
            collision.transform.SetParent(null);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        }
    }
}
