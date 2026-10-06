using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    //Everything beneath this is for use by the animator.
    private float verticalIncrement = 0.5f;
    public void VerticalIncrementDisplacement()
    {
        transform.Translate(Vector3.up * verticalIncrement);
    }
    private void SpawnBullet()
    {
        Instantiate(bullet , gameObject.transform.position + (2f * Vector3.up) , Quaternion.identity);
    }
    private void VerticalDecrementDisplacement()
    {
        transform.Translate(-Vector3.up * verticalIncrement);
    }
}