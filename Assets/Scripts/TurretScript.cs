using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float verticalIncrement = 0.05f;
    void VerticalIncrementDisplacement()
    {
        transform.Translate(Vector3.up * verticalIncrement);
    }
}
