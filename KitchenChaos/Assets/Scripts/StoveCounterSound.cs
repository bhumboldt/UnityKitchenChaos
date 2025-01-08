using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StoveCounterSound : MonoBehaviour
{
   private AudioSource audioSource;
   [SerializeField] private StoveCounter stoveCounter;

   private float warningSoundTimer;
   private bool playWarningSound;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
      stoveCounter.OnProgressChanged += StoveCounterOnOnProgressChanged;
   }

   private void StoveCounterOnOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
   {
      float burnShowProgressAmount = .5f;
      playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
   }

   private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
   {
      bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

      if (playSound)
      {
         audioSource.Play();
      }
      else
      {
         audioSource.Pause();
      }
   }

   private void Update()
   {
      if (playWarningSound)
      {
         warningSoundTimer -= Time.deltaTime;
         if (warningSoundTimer <= 0)
         {
            warningSoundTimer = .2f;
            SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
         }
      }
   }
}
