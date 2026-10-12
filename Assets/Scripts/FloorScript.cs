using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private static bool topFloorDestroyed = false;
    private void DestroyFloor()
    {
        topFloorDestroyed = true;
        Destroy(gameObject);
    }
    public static bool TopFloorDestroyed() => topFloorDestroyed;
    private void OnTriggerEnter(Collider other)
    {
        
    }
}