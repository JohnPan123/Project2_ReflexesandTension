using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody bulletRB;

    private float bulletSpeed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();

        bulletRB.linearVelocity = (Camera.main.GetComponentsInParent<Transform>()[1].position - transform.position).normalized * bulletSpeed;

        Debug.Log(Camera.main.GetComponentsInParent<Transform>()[1].gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}