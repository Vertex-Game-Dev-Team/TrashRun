using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public GameObject boostEffect;
    public GameObject boomEffect;
    public GameObject dropEffect;

    private void Awake()
    {
        instance = this;
    }
}
