using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    private GameObject spawnedLaser;
    private const int floorLayer = 3;
    private const int wallLayer = 6;
    private bool laserNotDestroyed = true;
    private void Start()
    {
        spawnedLaser = Instantiate(laser , transform.position , Quaternion.identity);
    }
    private void Update()
    {
        if (laserNotDestroyed) ProjectLaser();
    }
    private void ProjectLaser()
    {
        Vector3 vectorToPlayer = (Camera.main.GetComponentsInParent<Transform>()[1].position - transform.position);

        spawnedLaser.transform.forward = vectorToPlayer.normalized;
        spawnedLaser.transform.localScale = new Vector3(0.5f , 0.5f , vectorToPlayer.magnitude);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject objectCollidedWith = collision.gameObject;

        if (objectCollidedWith.CompareTag("Player")) DestroyBulletAnim();
        else if (objectCollidedWith.layer == floorLayer) DestroyBulletAnim();
        else if (objectCollidedWith.layer == wallLayer) DestroyBulletAnim();
    }

    //Beneath this, these are used by the animator
    private float bulletSpeed = 50f;
    private void LaunchBullet()
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = 
            (Camera.main.GetComponentsInParent<Transform>()[1].position - transform.position).normalized * bulletSpeed;
        
        Destroy(spawnedLaser);
        laserNotDestroyed = false;
    }

    private void DestroyBulletAnim()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<Animator>().SetBool("Destroy Bullet" , true);
        
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<MeshFilter>());
        Destroy(GetComponent<SphereCollider>());
        //Destroy(GetComponent<Rigidbody>());
    }
    private void DestroyBulletObj()
    {
        Destroy(gameObject);
    }
}