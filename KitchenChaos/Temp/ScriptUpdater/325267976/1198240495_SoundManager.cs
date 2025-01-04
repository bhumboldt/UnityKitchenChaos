using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefSO audioClipRefs;
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefs.deliveryFail, GetComponent<Camera>().main.transform.position);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], position, volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
