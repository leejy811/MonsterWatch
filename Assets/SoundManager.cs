
/*
 BGM을 재생하려면 SoundManager.instance.PlayBGM("BackgroundMusic")와 같이 호출합니다.
SFX를 재생하려면 SoundManager.instance.PlaySFX("JumpSound")와 같이 호출합니다.
볼륨을 설정하려면 SoundManager.instance.SetBgmVolume(0.5f)로 BGM의 볼륨을 설정하고, SoundManager.instance.SetSfxVolume(0.7f)로 SFX의 볼륨을 설정합니다.
 */


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]       
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] List<Sound> sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;  
    [SerializeField] AudioSource[] sfxPlayer = null;


    void Awake()
    {
        // make Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        bgmPlayer.loop = true;
        PlayBGM("blackmagic");
    }

    #region BGM
    public void PlayBGM(string bgmName)
    {
        foreach (Sound s in bgm) 
        {
            if (bgmName == s.name)     // play BGM if such name exists
            {
                bgmPlayer.clip = s.clip;
                bgmPlayer.Play();           
                return;
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    #endregion

    #region SFX
    public void PlaySFX(string sfxName)
    {
        int idx = sfx.FindIndex(x => x.name == sfxName);
        if (idx < 0)
            return;
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            if (!sfxPlayer[i].isPlaying)     //check if available audiosrc left
            {
                sfxPlayer[i].clip = sfx[idx].clip;
                sfxPlayer[i].Play();
                return;
            }
        }
        //Debug.Log("남아있는 오디오소스가 없습니다");
        return;
    }
    public void StopSfx(string sfxName)
    {
        int idx = sfx.FindIndex(x => x.name == sfxName);
        if (idx < 0)
            return;

        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            if(sfxPlayer[i].clip==sfx[idx].clip)
                sfxPlayer[i].Stop();
        }
    }
    #endregion

    # region Volume Settings
    public void SetBgmVolume(float value)
    {
        bgmPlayer.volume = value;
    }

    public void SetSfxVolume(float value)
    {
        foreach(AudioSource audioSource in sfxPlayer)
        {
            audioSource.volume = value;
        }
    }

    public float GetBgmVolume()
    {
        return bgmPlayer.volume;
    }

    public float GetSfxVolume()
    {
        return sfxPlayer[0].volume;
    }
    #endregion
}