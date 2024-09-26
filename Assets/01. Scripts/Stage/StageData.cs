using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data")]
public class StageData : ScriptableObject
{
    [SerializeField] private string stageName = "��������";
    public string StageName => stageName;

    [SerializeField] private GameObject[] patternPrefabs;
    public GameObject[] PatternPrefabs => patternPrefabs;
}
