using UnityEngine;

public class TurretSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretParent;
    private const float angularDisplacementIncrement = 360f / 20f;
    private const float turretDistance = 15f;
    private const float turretDownwardMagnitude = 5f;

    private void Update()
    {
        if (!TurretSpawnerManager.CheckIfAllSpawned() && TurretSpawnerManager.TimeToSpawnTurret())
            SpawnTurret();

        else if (TurretSpawnerManager.CheckIfAllFinishedSpawning())
        {

            turretParent.GetComponent<TurretShooterManager>().enabled = true;
            this.enabled = false;
        }
    }

    private void SpawnTurret()
    {
        int turretIndexToSpawn = Random.Range(0, 20);

        while (TurretSpawnerManager.turretsSpawned[turretIndexToSpawn])
        {
            if (++turretIndexToSpawn == 20)
                turretIndexToSpawn = 0;
        }

        transform.Rotate(Vector3.up * (angularDisplacementIncrement * turretIndexToSpawn));
        
        GameObject turretSpawned = Instantiate(turret , (transform.position + (transform.forward.normalized * turretDistance) - (Vector3.up * turretDownwardMagnitude)) , transform.rotation , turretParent.transform);
        TurretShooterManager.turretsList.Add(turretSpawned);

        turretSpawned.GetComponent<TurretScript>().SetTurretNumber(turretIndexToSpawn);
        TurretSpawnerManager.turretsSpawned[turretIndexToSpawn] = true;


        transform.Rotate(-Vector3.up * (angularDisplacementIncrement * turretIndexToSpawn));
    }
}