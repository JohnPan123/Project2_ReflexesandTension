using UnityEngine;

public class TopFloorScript : MonoBehaviour
{
    private void DestroyTopFloor() //This is used in the animator
    {
        GameManager.DeclareVictory();
        Destroy(gameObject);
    }
}