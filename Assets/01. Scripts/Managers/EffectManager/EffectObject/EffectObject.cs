namespace USF.Manager.EffectObjectClass
{
    using UnityEngine;

    public class EffectObject : MonoBehaviour
    {
        [Header("EffectInfo")]
        [SerializeField] private float destroyTime = default;

        private void OnEnable()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}



