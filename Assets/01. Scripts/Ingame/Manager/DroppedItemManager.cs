// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class DroppedItemManager : MonoBehaviour
{
    private static DroppedItemManager instance;
    public static DroppedItemManager Instance => instance;

    [SerializeField] private GameObject scrapPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnScrap(Vector3 spawnPoint)
    {
        Instantiate(scrapPrefab, spawnPoint, Quaternion.identity);
    }
}
