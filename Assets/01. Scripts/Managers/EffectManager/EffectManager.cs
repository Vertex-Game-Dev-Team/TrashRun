namespace USF.Manager.EffectManager
{
    using System;

    using UnityEngine;

    using USF.Manager.EffectClass;

    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance;

        public Effect[] effectArray;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayEffect(Vector3 position, string effectName)
        {
            Effect effect = Array.Find(effectArray, x => x.effectName == effectName);

            Instantiate(effect.effectObject, position, Quaternion.identity);
        }
    }
}



