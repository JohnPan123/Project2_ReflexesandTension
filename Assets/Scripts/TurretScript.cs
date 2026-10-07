using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private int turretNumber;
    public void SetTurretNumber(int number)
    {
        turretNumber = number;
    }
    public int GetTurretNumber() => turretNumber;

    //Everything beneath this is for use by the animator.
    private float verticalIncrement = 0.5f;
    private void VerticalIncrementDisplacement()
    {
        transform.Translate(Vector3.up * verticalIncrement);
    }
    private void VerticalDecrementDisplacement()
    {
        transform.Translate(-Vector3.up * verticalIncrement);
    }
    private void SpawnBullet()
    {
        Instantiate(bullet , gameObject.transform.position + (2f * Vector3.up) , Quaternion.identity);
    }

    public void TurretDespawned()
    {
        GameManager.turretsSpawned[turretNumber] = false;
        Destroy(gameObject);
    }
}