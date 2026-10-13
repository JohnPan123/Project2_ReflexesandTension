using UnityEngine;

public class FloorScript : MonoBehaviour
{
    //-----------------------------------------------------TOP FLOOR STUFF----------------------------------------------\\
    private static bool topFloorDestroyed = false;
    private void DestroyTopFloor() //This is used in the animator
    {
        topFloorDestroyed = true;
        Destroy(gameObject);
    }
    public static bool TopFloorDestroyed() => topFloorDestroyed;
    //-----------------------------------------------------TOP FLOOR STUFF----------------------------------------------\\

    //---------------------------------------------------BOTTOM FLOOR STUFF---------------------------------------------\\

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.DeclareVictory();
    }
    //---------------------------------------------------BOTTOM FLOOR STUFF---------------------------------------------\\
}