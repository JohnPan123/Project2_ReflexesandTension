using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float turretFiringHeight = 2f;
    private int turretNumber;

    private void Update()
    {
        if (!LimitReached()) VerticalIncrementDisplacement();
        else this.enabled = false;
    }
    //---------------------------------------------------TURRET NUMBER FUNCTIONS-----------------------------------------------\\
    public void SetTurretNumber(int number)
    {
        turretNumber = number;
    }
    public int GetTurretNumber() => turretNumber;
    //---------------------------------------------------TURRET NUMBER FUNCTIONS-----------------------------------------------\\

    //----------------------------------------------------TURRET SPAWN FUNCTIONS-----------------------------------------------\\
    private float verticalIncrement = 3.0f;
    private void VerticalIncrementDisplacement()
    {
        transform.Translate(Vector3.up * verticalIncrement * Time.deltaTime);
    }
    private bool LimitReached()
    {
        if (transform.position.y >= turretFiringHeight)
        {
            TurretSpawnerManager.turretsFinishedSpawning[turretNumber] = true;
            return true;
        }
        return false;
    }
    //---------------------------------------------------TURRET SPAWN FUNCTIONS-----------------------------------------------\\

    //---------------------------------------------------BULLET SPAWN FUNCTIONS-----------------------------------------------\\
    public void SpawnBullet()
    {
        Instantiate(bullet , gameObject.transform.position + (3f * Vector3.up) , Quaternion.identity);
    }
    //---------------------------------------------------BULLET SPAWN FUNCTIONS-----------------------------------------------\\
}