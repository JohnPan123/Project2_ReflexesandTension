using UnityEngine;

public class WallSegmentandTurretSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject wallSegment;
    [SerializeField] private GameObject turret;

    private const float wallSegmentTotalCount = 20f;
    private const float angularDisplacementIncrement = 360f / 20f;
    private const float radiusOfWall = 13.75f;
    private const float turretDistance = 15f;
    private const float turretDownwardMagnitude = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int wallSegmentCount = 0; wallSegmentCount < wallSegmentTotalCount; wallSegmentCount++)
        {
            Instantiate(wallSegment , (transform.position + (transform.forward.normalized * radiusOfWall)) , transform.rotation);
            transform.Rotate(Vector3.up * (angularDisplacementIncrement));
        }
    }
    
    private void Update()
    {
        if (GameManager.SpawnTurret())
            SpawnTurret();
    }

    private void SpawnTurret()
    {
        int turretIndexToSpawn = Random.Range(0 , 20);
        
        while (GameManager.turretsSpawned[turretIndexToSpawn])
        {
            if (++turretIndexToSpawn == 20)
                turretIndexToSpawn = 0;
        }

        transform.Rotate(Vector3.up * (angularDisplacementIncrement * turretIndexToSpawn));
        GameObject turretSpawned = Instantiate(turret , (transform.position + (transform.forward.normalized * turretDistance) - (Vector3.up * turretDownwardMagnitude)) , transform.rotation);
        turretSpawned.GetComponent<TurretScript>().SetTurretNumber(turretIndexToSpawn);

        transform.Rotate(-Vector3.up * (angularDisplacementIncrement * turretIndexToSpawn));
        GameManager.turretsSpawned[turretIndexToSpawn] = true;
    }
}