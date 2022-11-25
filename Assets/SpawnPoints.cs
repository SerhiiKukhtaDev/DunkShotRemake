using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform ballPosition;
    [SerializeField] private Transform firstSpawnPoint;
    [SerializeField] private Transform secondSpawnPoint;
    [SerializeField] private RectTransform[] spawnAreas = new RectTransform[2];

    public Transform FirstSpawnPoint => firstSpawnPoint;

    public Transform SecondSpawnPoint => secondSpawnPoint;

    public RectTransform[] SpawnAreas => spawnAreas;

    public Transform BallPosition => ballPosition;
}
