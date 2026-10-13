using UnityEngine;
using System.Collections.Generic;

public class TurretShooterManager : MonoBehaviour
{
    public static List<GameObject> turretsList;
    [SerializeField] private AudioSource audio;
    private int bulletsTillTensionTime = 50;
    private float tensionTime = 7f;
    private float timeElapsedSinceLastBullet = 0f;
    private float timeToFireNextBullet = 3f;
    private float[] timesToFire;
    private void Awake()
    {
        turretsList = new List<GameObject>();
        timesToFire = new float[] {0.1f , 0.3f , 0.5f};
        audio = GetComponent<AudioSource>();

        this.enabled = false;
    }

    void Update()
    {
        if (GameManager.CheckIfPlayerIsAlive())
        {
            timeElapsedSinceLastBullet += Time.deltaTime;

            if (GameManager.GetVictoryState())
            {
                this.enabled = false;
                return;
            }

            else if (timeElapsedSinceLastBullet >= timeToFireNextBullet)
            {
                TimeToFire();
                ResetFiringTime();
            }
        }
    }

    private void TimeToFire()
    {
        turretsList[Random.Range(0 , 20)].GetComponent<TurretScript>().SpawnBullet();
        bulletsTillTensionTime--;
    }
    private void ResetFiringTime()
    {
        if (bulletsTillTensionTime > 0)
        {
            timeToFireNextBullet = timesToFire[Random.Range(0 , (timesToFire.Length - 1))];

            if (!audio.isPlaying) 
                audio.Play();
        }

        else
        {
            timeToFireNextBullet = tensionTime;
            bulletsTillTensionTime = (int)(70 * Random.Range(0.55f , 0.8f));
            audio.Pause();
        }
        timeElapsedSinceLastBullet = 0f;
    }
}