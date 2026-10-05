using UnityEngine;

public class WallSegmentSpawnerScript : MonoBehaviour
{
    public GameObject wallSegment;
    public GameObject turret;
    private const float wallSegmentTotalCount = 20f;
    private const float angularDisplacementIncrement = 360f / 20f;
    private const float radiusOfWall = 13.75f;
    private const float turretDistance = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int wallSegmentCount = 0; wallSegmentCount < wallSegmentTotalCount; wallSegmentCount++)
        {
            GameObject wallSeg = Instantiate(wallSegment , (transform.position + (transform.forward.normalized * radiusOfWall)) , transform.rotation);
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
        transform.Rotate(Vector3.up * (angularDisplacementIncrement * Random.Range(0 , 21)));
        Instantiate(turret , (transform.position + (transform.forward.normalized * turretDistance)) , transform.rotation);
    }
}