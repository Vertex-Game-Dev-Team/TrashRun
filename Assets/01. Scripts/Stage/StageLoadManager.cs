using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoadManager : MonoBehaviour
{
    // 카메라의 트랜스폼에 따라 패턴을 생성하기 위한 변수
    private Transform camTransform;
    private readonly int chunkSize = 24;
    private int chunkCount = 0;

    // 스테이지 관련
    [Tooltip("스테이지 순서대로 기재")][SerializeField] private StageData[] stageDatas;
    private StageData curStageData = null;
    private int curStageNumber; // 지금 스테이지 순서
    private int patternInstanceCount; // 패턴을 소환한 횟수에 따라 스테이지 교체

    // 이전 패턴을 삭제하기 위한 변수
    private GameObject currentPatternPrefab;
    private GameObject previousPatternPrefab;

    [SerializeField] private GameObject startPatternPrefab;

    private void Awake()
    {
        ChangeNextStage();
        Instantiate(currentPatternPrefab, Vector3.zero, Quaternion.identity);
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

    private void ChangeNextStage()
    {
        if (stageDatas[curStageNumber + 1] == null) return;

        curStageData = stageDatas[++curStageNumber];
    }
}
