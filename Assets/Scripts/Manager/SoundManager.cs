using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private const string SOUNDMANAGER_VOLUME = "SoundManagerVolume";

    public int volume = 5;

    private AudioSource waterAudioSource;

    private void Awake()
    {
        Instance = this;
        LoadVolume();

        waterAudioSource = gameObject.AddComponent<AudioSource>();
        waterAudioSource.loop = true; 
    }

    private void Start()
    {
        CuttingCounter.OnWash += CuttingCounter_OnWash;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        print("11");
        PlaySound(audioClipRefsSO.trash);
    }

    private void KitchenObjectHolder_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }

   private void CuttingCounter_OnWash(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.washSound);
    }

    private void PlaySound(AudioClip[] clips,Vector3 position, float volumeMutipler = 1.0f)
    {
        if (volume == 0) return;
        int index = Random.Range(0, clips.Length);

        AudioSource.PlayClipAtPoint(clips[index], position, volumeMutipler*(volume/10.0f));
    }

    private void PlaySound(AudioClip[] clips, float volumeMutipler = .1f)
    {
        PlaySound(clips, Camera.main.transform.position, volumeMutipler);
    }

    public void PlayStepSound(float volumeMutipler = 1.0f)
    {
        PlaySound(audioClipRefsSO.footstep, volumeMutipler);
    }

    public void PlayWaterSound()
    {
        if (waterAudioSource.isPlaying)
        {
            // 如果水流声音已经在播放，就不做任何操作
            return;
        }

        waterAudioSource.clip = audioClipRefsSO.waterSound[0]; // 假设 waterSound 数组只有一个声音
        waterAudioSource.volume = volume / 10.0f;
        waterAudioSource.Play(); // 播放水流声音
    }

    public void StopWaterSound()
    {
        waterAudioSource.Stop();
    }

    public void PlayWarningSound()
    {
        PlaySound(audioClipRefsSO.warning);
    }

    public void PlaySuccessSound()
    {
        PlaySound(audioClipRefsSO.delivery);
    }
    public void PlayCarHornSound()
    {
        PlaySound(audioClipRefsSO.carhorn);
    }
    public void PlayHurtSound()
    {
        PlaySound(audioClipRefsSO.hurtSound);
    }
    public void ChangeVolume()
    {
        //volume 0-10     0~1
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }
        SaveVolume();
    }
    public int GetVolume()
    {
        return volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetInt(SOUNDMANAGER_VOLUME, volume);
    }

    private void LoadVolume()
    {
        volume = PlayerPrefs.GetInt(SOUNDMANAGER_VOLUME, volume);
    }

}
