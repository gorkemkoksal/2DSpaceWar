using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")] 
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPreFab;
    [SerializeField] GameObject pathPreFab;
    [SerializeField] float timeIntervals = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 7;
    [SerializeField] float moveSpeed = 10f;
    public GameObject GetEnemyPreFab() { return enemyPreFab; }
    public List<Transform> GetWayPoints() 
    { 
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPreFab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints; 
    }
    public float GetTimeIntervals() { return timeIntervals; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

}
