using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoadManager : MonoBehaviour
{
    // ī�޶��� Ʈ�������� ���� ������ �����ϱ� ���� ����
    private Transform camTransform;
    private readonly int chunkSize = 24;
    private int chunkCount = 0;

    // �������� ����
    [Tooltip("�������� ������� ����")][SerializeField] private StageData[] stageDatas;
    private StageData curStageData = null;

    // ���� ������ �����ϱ� ���� ����
    private GameObject currentPatternPrefab;
    private GameObject previousPatternPrefab;

    [SerializeField] private GameObject startPatternPrefab;

    private void Awake()
    {
        curStageData = stageDatas[0];
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

        if (previousPatternPrefab != null) Destroy(previousPatternPrefab);
        if (currentPatternPrefab != null) previousPatternPrefab = currentPatternPrefab;

        currentPatternPrefab = Instantiate(randomPatternPrefab, new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
    }
}
