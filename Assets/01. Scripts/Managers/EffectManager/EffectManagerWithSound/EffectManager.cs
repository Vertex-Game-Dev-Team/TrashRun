namespace USF.Manager.EffectManagerWithSound
{
    using System;


    using UnityEngine;

    using USF.Manager.EffectClass;
    using USF.Manager.SoundManager;

    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance;

        public Effect[] effectArray;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayEffect(Vector3 position, string effectName, string clipName)
        {
            Effect effect = Array.Find(effectArray, x => x.effectName == effectName);
            SoundManager.instance.PlaySFX(clipName);
            Instantiate(effect.effectObject, position, Quaternion.identity);
        }
    }
}




