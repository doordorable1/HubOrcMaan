using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Factory"))
        {
            GameManager.Instance.AddBox();
            Destroy(gameObject);
        }
    }
}