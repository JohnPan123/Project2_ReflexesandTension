using UnityEngine;

public class TurretSpawnerManager : MonoBehaviour
{
    private static bool spawnTurretTimeBool;
    public static bool[] turretsSpawned;
    public static bool[] turretsFinishedSpawning;
    private static float turretSpawnTimer;
    private static float timeToSpawnTurret;
    private void Awake()
    {
        turretsSpawned = new bool[20];
        turretsFinishedSpawning = new bool[20];

        for (int i = 0; i < 20; i++) turretsSpawned[i] = false;
        for (int i = 0; i < 20; i++) turretsFinishedSpawning[i] = false;
    }
    void Start()
    {
        spawnTurretTimeBool = false;
        turretSpawnTimer = 0f;
        timeToSpawnTurret = 0.5f;
    }

    void Update()
    {
        turretSpawnTimer += Time.deltaTime;
        spawnTurretTimeBool = (turretSpawnTimer >= timeToSpawnTurret);

        if (spawnTurretTimeBool) ResetTimer();

        if (CheckIfAllFinishedSpawning()) 
            this.enabled = false;
    }
    public static void ResetTimer()
    {
        turretSpawnTimer = 0f;
    }
    public static bool TimeToSpawnTurret() => spawnTurretTimeBool;
    public static bool CheckIfAllFinishedSpawning()
    {
        int totalTurretsSpawned = 0;

        foreach (bool turretPresent in turretsFinishedSpawning)
        {
            if (turretPresent)
                totalTurretsSpawned++;
        }

        return (totalTurretsSpawned == turretsFinishedSpawning.Length);
    }
    public static bool CheckIfNeedToSpawnMoreTurrets()
    {
        int totalTurretsSpawned = 0;

        foreach (bool turretPlace in turretsSpawned)
        {
            if (turretPlace)
                totalTurretsSpawned++;
        }

        return (totalTurretsSpawned < turretsSpawned.Length);
    }
}