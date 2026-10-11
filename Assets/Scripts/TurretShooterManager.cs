using UnityEngine;
using System.Collections.Generic;

public class TurretShooterManager : MonoBehaviour
{
    public static List<GameObject> turretsList;
    private float timeElapsed = 0f;
    private float timeToFireNextBullet = 3f;
    private float[] timesToFire;
    private void Awake()
    {
        turretsList = new List<GameObject>();
        timesToFire = new float[] {0.1f , 0.3f , 0.5f , 0.7f , 0.9f , 1.1f};

        this.enabled = false;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeToFireNextBullet)
        {
            TimeToFire();
            ResetFiringTime();
        }
    }

    private void TimeToFire()
    {
        turretsList[Random.Range(0 , 20)].GetComponent<TurretScript>().SpawnBullet();
    }
    private void ResetFiringTime()
    {
        timeElapsed = 0f;
        timeToFireNextBullet = timesToFire[Random.Range(0 , 6)];
    }
}