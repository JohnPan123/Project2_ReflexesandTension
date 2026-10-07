using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private GameObject spawnedLaser;
    private bool laserDestroyed = false;
    private void Start()
    {
        spawnedLaser = Instantiate(laser , transform.position , Quaternion.identity);
    }
    private void Update()
    {
        if (!laserDestroyed)
            ProjectLaser();
    }
    private void ProjectLaser()
    {
        Vector3 vectorToPlayer = (Camera.main.GetComponentsInParent<Transform>()[1].position - transform.position);

        spawnedLaser.transform.forward = vectorToPlayer.normalized;
        spawnedLaser.transform.localScale = new Vector3(0.25f, 0.25f, vectorToPlayer.magnitude / 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    //Beneath this, these are used by the animator
    private float bulletSpeed = 50f;
    public void LaunchBullet()
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = (Camera.main.GetComponentsInParent<Transform>()[1].position - transform.position).normalized * bulletSpeed;
        
        Destroy(spawnedLaser);
        laserDestroyed = true;
    }
}