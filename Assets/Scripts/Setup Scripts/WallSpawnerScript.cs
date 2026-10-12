using UnityEngine;

public class WallSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject wallSegment;
    [SerializeField] private GameObject parentOfWalls;
    private const float wallSegmentTotalCount = 20f;
    private const float angularDisplacementIncrement = 360f / 20f;
    private const float radiusOfWall = 13.75f;

    void Start()
    {
        float wallOriginOffset = wallSegment.transform.localScale.y / 2f;

        for (int wallSegmentCount = 0; wallSegmentCount < wallSegmentTotalCount; wallSegmentCount++)
        {
            Instantiate(wallSegment , (transform.position + (transform.forward.normalized * radiusOfWall) - (Vector3.up * wallOriginOffset)) , transform.rotation, parentOfWalls.transform);
            transform.Rotate(Vector3.up * (angularDisplacementIncrement));
        }
    }
}
