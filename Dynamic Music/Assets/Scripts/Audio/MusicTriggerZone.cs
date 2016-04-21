using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicTriggerZone : MonoBehaviour {

   public AudioMixer mixer;
   public AudioMixerSnapshot kickSnapShot;
   public AudioMixerSnapshot addSnareSnapShot;
   public AudioMixerSnapshot addBassSnapShot;
   public AudioMixerSnapshot addArp1SnapShot;
   public AudioMixerSnapshot addArp2SnapShot;
   public AudioMixerSnapshot addLead1SnapShot;
   public AudioMixerSnapshot addLead2SnapShot;
   public float fadeTime;
   public AudioClip oneOff;
   public AudioSource oneOffAudioSource;

   private AudioMixerSnapshot enterSnapShot;
   private AudioMixerSnapshot exitSnapShot;
   private float pitch;


   void Awake() {
      if (this.CompareTag("AddKick"))
      {
         enterSnapShot = kickSnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1;
      }
      else if (this.CompareTag("AddSnare"))
      {
         enterSnapShot = addSnareSnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.1f;
      }
      else if (this.CompareTag("AddBass"))
      {
         enterSnapShot = addBassSnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.2f;
      }
      else if (this.CompareTag("AddArp1"))
      {
         enterSnapShot = addArp1SnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.3f;
      }
      else if (this.CompareTag("AddArp2"))
      {
         enterSnapShot = addArp2SnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.4f;
      }
      else if (this.CompareTag("AddLead1"))
      {
         enterSnapShot = addLead1SnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.5f;
      }
      else if (this.CompareTag("AddLead2"))
      {
         enterSnapShot = addLead2SnapShot;
         exitSnapShot = kickSnapShot;
         pitch = 1.6f;
      }

   }
   void OnTriggerEnter(Collider other) {
      enterSnapShot.TransitionTo(fadeTime);
      oneOffAudioSource.pitch = pitch;
      oneOffAudioSource.PlayOneShot(oneOff, .5f);
   }
   void OnTriggerExit(Collider other) {
      //exitSnapShot.TransitionTo(fadeTime);
   }
}
