using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageLoadManager : MonoBehaviour
{
    // ī�޶��� Ʈ�������� ���� ������ �����ϱ� ���� ����
    private Transform camTransform;

    // ûũ�� ������ ��ġ ����
    private readonly int chunkSize = 24;
    private int chunkCount = 0;

    // �������� ���� ���ϵ��� ������ ����
    private readonly int maxPatternCountInStage = 10;
    private int instancePatternCount = 0;

    private int curStageNumber = 0;

    // �������� ����
    [Tooltip("�������� ������� ����")][SerializeField] private StageData[] stageDatas;
    private StageData curStageData = null;

    // ���� ������ �����ϱ� ���� ����
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

    // ûũ ��ȯ
    private void InstanceNextChunk()
    {
        // ����ûũ�����
        if (previousPatternPrefab != null) Destroy(previousPatternPrefab);
        if (currentPatternPrefab != null) previousPatternPrefab = currentPatternPrefab;

        if (stageDatas.Length >= curStageNumber + 1)
        {
            instancePatternCount++;
            if (instancePatternCount >= maxPatternCountInStage) // �� ������������ ������ 10���̻� ������ �� ���� ���������� ��ȯ
            {
                instancePatternCount = 0;
                InitNextStage();
                return;
            }
        }
        else Debug.LogError("���������������Դϴ�!!");

        int randomPatternId = Random.Range(0, curStageData.PatternPrefabs.Length);
        GameObject randomPatternPrefab = curStageData.PatternPrefabs[randomPatternId];
        currentPatternPrefab = Instantiate(randomPatternPrefab, new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
    }

    // ���ο� ���������� �Ѿ ��
    private void InitNextStage()
    {
        curStageData = stageDatas[curStageNumber];
        txt_stageName.text = (curStageNumber + 1).ToString() + ". " + curStageData.StageName;
        curStageNumber++;

        // ���������� ó�� �����ϴ� �ܰ迡�� �Ϲ����� �ٴ� ����
        currentPatternPrefab = Instantiate(curStageData.PatternPrefabs[0], new Vector3(chunkCount * chunkSize, 0, 0), Quaternion.identity);
    }
}
