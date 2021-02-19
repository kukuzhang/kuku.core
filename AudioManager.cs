using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Urban
{
    

    public class AudioManager : BaseManager<AudioManager>
    {
        private AudioSource bkMusic = null;
        private float bgmValue = 1;
        private float soundValue = 1;

        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();


        public AudioManager()
        {

            MonoManager.Self.AddUpdateEvent(update);

        }

        public void update()
        {
            for(int i = soundList.Count -1; i >= 0; --i)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }

        }


        public void PlayBgm(string name,bool isLoop = true)
        {
            if(bkMusic == null)
            {
                GameObject obj = new GameObject();
                obj.name = "BGMMusic";
                bkMusic = obj.AddComponent<AudioSource>();
            }

            ResManager.Self.LoadAsync<AudioClip>("Music/bgm/" + name, (clip) =>
            {
                bkMusic.clip = clip;
                bkMusic.loop = isLoop;
                bkMusic.volume = bgmValue;
                bkMusic.Play();
            });
        }

        public void StopBgm()
        {
            if (bkMusic != null)
                bkMusic.Stop();
        }


        public void PauseBgm()
        {
            if (bkMusic != null)
                bkMusic.Pause();
        }

        public void ChangeBgmValue(float v)
        {
            bgmValue = v;
            if (bkMusic != null)
                bkMusic.volume = bgmValue;

        }



        public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callback = null)
        {
            if(soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "sound";
                GameObject.DontDestroyOnLoad(soundObj);
            }

            //AudioSource source = soundObj.AddComponent<AudioSource>();
            ResManager.Self.LoadAsync<AudioClip>("Sound/" + name, (clip) =>
            {
                AudioSource source = soundObj.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = bgmValue;
                source.loop = isLoop;
                source.Play();
                soundList.Add(source);

                if (callback != null)
                    callback(source);
            });
        }

        public void changeSoundValue(float v)
        {
            soundValue = v;
            for(int i = 0; i < soundList.Count; i++)
            {
                soundList[i].volume = soundValue;
            }
        }

        public void StopSound(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                source.Stop();
                GameObject.Destroy(source);

            }
        }




    }

}
