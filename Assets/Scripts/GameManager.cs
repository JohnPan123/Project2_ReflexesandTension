using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool spawnTurret;
    public static bool[] turretsSpawned;

    private static int turretsSpawnedCount;
    private static int turretsTillTension = 20;

    private static float turretSpawnTimer;
    private static float timeToSpawnTurret;
    private static float[] listOfTurretTimeSpawns;
    private static float suspenseTiming = 10f;
    private void Awake()
    {
        turretsSpawned = new bool[20];

        for (int i = 0; i < 20; i++) turretsSpawned[i] = false;
    }
    void Start()
    {
        spawnTurret = false;
        turretsSpawnedCount = 0;
        turretSpawnTimer = 0f;
        timeToSpawnTurret = 3f;

        listOfTurretTimeSpawns = new float[] { 0.3f, 0.5f, 0.8f, 1f, 1.2f, 1.4f, 1.7f };
    }
    // Update is called once per frame
    void Update()
    {
        turretSpawnTimer += Time.deltaTime;

        if (spawnTurret)
        {
            DetermineTimeToNextTurret();
            ResetTimer();
        }
    }
    private void DetermineTimeToNextTurret()
    {
        if (++turretsSpawnedCount == turretsTillTension)
            timeToSpawnTurret = suspenseTiming;
        else
            timeToSpawnTurret = listOfTurretTimeSpawns[Random.Range(0, 7)];
    }
    public static void ResetTimer()
    {
        turretSpawnTimer = 0f;
    }
    public static bool SpawnTurret() => (spawnTurret = (turretSpawnTimer >= timeToSpawnTurret));
}