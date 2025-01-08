using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipsRefSO audioClipRefs;

    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnObjectPlacedHere;
        TrashCounter.OnTrash += TrashCounterOnOnTrash;
    }

    private void TrashCounterOnOnTrash(object sender, EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(audioClipRefs.trash, counter.transform.position);
    }

    private void BaseCounter_OnObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(audioClipRefs.objectDrop, counter.transform.position);
    }

    private void Player_OnPickedUpSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.objectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefs.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    public void PlayPlayerFootsteps(Vector3 position)
    {
        PlaySound(audioClipRefs.footstep, position);
    }
    
    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefs.warning, Vector3.zero);
    }
    
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefs.warning, position);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], position, this.volume * volumeMultiplier);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float getVolume()
    {
        return volume;
    }
}
