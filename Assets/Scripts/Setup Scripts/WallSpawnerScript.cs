using UnityEngine;

public class WallSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject wallSegment;
    [SerializeField] private GameObject parentOfWalls;
    private const float wallSegmentTotalCount = 20f;
    private const float angularDisplacementIncrement = 360f / 20f;
    private const float radiusOfWall = 13.75f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int wallSegmentCount = 0; wallSegmentCount < wallSegmentTotalCount; wallSegmentCount++)
        {
            Instantiate(wallSegment, (transform.position + (transform.forward.normalized * radiusOfWall)), transform.rotation, parentOfWalls.transform);
            transform.Rotate(Vector3.up * (angularDisplacementIncrement));
        }
    }
}
