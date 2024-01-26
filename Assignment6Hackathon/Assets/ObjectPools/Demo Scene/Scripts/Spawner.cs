using System.Collections.Generic;
using MidniteOilSoftware.ObjectPoolManager;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField][Range(0f, 5f)] private float _spawnDelay = 0.01f;
    [SerializeField][Range(1f, 30f)] private float _launchForce = 10;

    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private float _randomizationRangeX = 1.0f; // Range for randomization around X
    [SerializeField] private float _randomizationRangeZ = 1.0f; // Range for randomization around Z

    private int _spawnedObjectCount = 0;

    [SerializeField] private TextMeshPro _objectCountText;

    private void Awake()
    {
        _spawnPosition = transform.position;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnRandomObject), 0f, _spawnDelay);
    }

    private void SpawnRandomObject()
    {
        GameObject prefab = _prefabs[Random.Range(0, _prefabs.Count)];

        // Randomize X and Z around the set position
        float randomX = Random.Range(-_randomizationRangeX, _randomizationRangeX);
        float randomZ = Random.Range(-_randomizationRangeZ, _randomizationRangeZ);

        Vector3 spawnPosition = _spawnPosition + new Vector3(randomX, 0f, randomZ);

        GameObject go = ObjectPoolManager.SpawnGameObject(prefab, spawnPosition, Quaternion.identity);
        go.GetComponent<Rigidbody>()?.AddForce(transform.up * _launchForce, ForceMode.Impulse);

        _spawnedObjectCount++;

        // Update TextMeshPro with the new count
        if (_objectCountText != null)
        {
            _objectCountText.text = "Bottles: " + (_spawnedObjectCount * 5).ToString();
        }
    }
}
