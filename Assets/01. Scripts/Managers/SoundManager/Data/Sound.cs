namespace USF.Manager.SoundClass
{
    using UnityEngine;
    using System;


    /// <summary>
    /// 오디오 매니저에 사용될 음악 데이터를 가지고 있는 클래스
    /// </summary>
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}


