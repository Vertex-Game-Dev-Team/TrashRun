using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoadManager : MonoBehaviour
{
    private Transform camTransform;
    private readonly int chunkSize = 24;
    private int chunkCount = 0;

    [SerializeField] private StageData curStageData = null;

    private void Awake()
    {
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if(camTransform.position.x >= (chunkCount * chunkSize))
        {
            chunkCount++;
            InstanceNextChunk();
        }
    }

    private void InstanceNextChunk()
    {
        int randomPatternId = Random.Range(0, curStageData.PatternPrefabs.Length);
        GameObject randomPatternPrefab = curStageData.PatternPrefabs[randomPatternId];

        Instantiate(randomPatternPrefab, new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
         
    }
}
