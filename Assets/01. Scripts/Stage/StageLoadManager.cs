using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageLoadManager : MonoBehaviour
{
    // 카메라의 트랜스폼에 따라 패턴을 생성하기 위한 변수
    private Transform camTransform;

    // 청크가 생성될 위치 관리
    private readonly int chunkSize = 24;
    private int chunkCount = 0;

    // 스테이지 안의 패턴들의 갯수를 관리
    private readonly int maxPatternCountInStage = 10;
    private int instancePatternCount = 0;

    private int curStageNumber = 0;

    // 스테이지 관련
    [Tooltip("스테이지 순서대로 기재")][SerializeField] private StageData[] stageDatas;
    private StageData curStageData = null;

    // 이전 패턴을 삭제하기 위한 변수
    private GameObject currentPatternPrefab;
    private GameObject previousPatternPrefab;

    [SerializeField] private TMP_Text txt_stageName;

    private void Awake()
    {
        InitNextStage();
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

    // 청크 소환
    private void InstanceNextChunk()
    {
        // 이전청크지우기
        if (previousPatternPrefab != null) Destroy(previousPatternPrefab);
        if (currentPatternPrefab != null) previousPatternPrefab = currentPatternPrefab;

        if (stageDatas.Length >= curStageNumber + 1)
        {
            instancePatternCount++;
            if (instancePatternCount >= maxPatternCountInStage) // 한 스테이지에서 패턴이 10개이상 생성될 때 다음 스테이지로 전환
            {
                instancePatternCount = 0;
                InitNextStage();
                return;
            }
        }
        else Debug.LogError("마지막스테이지입니다!!");

        int randomPatternId = Random.Range(0, curStageData.PatternPrefabs.Length);
        GameObject randomPatternPrefab = curStageData.PatternPrefabs[randomPatternId];
        currentPatternPrefab = Instantiate(randomPatternPrefab, new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
    }

    // 새로운 스테이지로 넘어갈 때
    private void InitNextStage()
    {
        curStageData = stageDatas[curStageNumber];
        txt_stageName.text = (curStageNumber + 1).ToString() + ". " + curStageData.StageName;
        curStageNumber++;

        // 스테이지를 처음 시작하는 단계에선 일반적인 바닥 생성
        currentPatternPrefab = Instantiate(curStageData.PatternPrefabs[0], new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
    }
}
