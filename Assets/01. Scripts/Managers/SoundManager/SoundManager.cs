namespace USF.Manager.SoundManager
{
    using System;
    using UnityEngine;
    using USF.Manager.SoundClass;

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance = null;

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        //싱글톤
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region 볼륨 조절
        public void UpdateMusicVolume(float value)
        {
            musicSource.volume = value;
        }

        public void UpdateSFXVolume(float value)
        {
            sfxSource.volume = value;
        }
        #endregion

        /// <summary>
        /// 음악을 실행시키는 함수
        /// </summary>
        /// <param name="name">음악의 이름</param>
        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Music Not Found");
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        /// <summary>
        /// 음악을 멈추는 함수
        /// </summary>
        public void StopMusic()
        {
            musicSource.Stop();
        }

        /// <summary>
        /// 효과음을 실행시키는 함수
        /// </summary>
        /// <param name="name">효과음의 이름</param>
        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Sount Not Found");
            }
            else
            {
                sfxSource.PlayOneShot(s.clip);
            }
        }
    }
}



