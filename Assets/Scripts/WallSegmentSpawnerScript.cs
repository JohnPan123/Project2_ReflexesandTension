using UnityEngine;

public class WallSegmentSpawnerScript : MonoBehaviour
{
    public GameObject wallSegment;
    private const float wallSegmentTotalCount = 20f;
    private const float radiusOfWall = 13.75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int wallSegmentCount = 0; wallSegmentCount < wallSegmentTotalCount; wallSegmentCount++)
        {
            Instantiate(wallSegment , (transform.position + (transform.forward.normalized * radiusOfWall)) , transform.rotation , transform.parent);
            transform.Rotate(Vector3.up * (360f / 20f));
        }
    }
}
