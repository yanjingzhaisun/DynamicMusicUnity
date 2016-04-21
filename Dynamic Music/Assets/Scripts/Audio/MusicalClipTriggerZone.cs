using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicalClipTriggerZone : MonoBehaviour {

   public AudioMixer mixer;
   public float fadeTime;

   public MusicalClip Kick;
   public MusicalClip Snare;
   public MusicalClip Bass;
   public MusicalClip Arp1;
   public MusicalClip Arp2;
   public MusicalClip Lead1;
   public MusicalClip Lead2;
   public MusicalClip OneOffClip;

   private RainbowColorMaterial rainbow;

   private MusicalClip _clip;
   
   private float pitch;

   private bool ready;
   private bool active;

   private ParticleSystem particles;

   void Awake() {

      rainbow = GetComponent<RainbowColorMaterial>();
      particles = GetComponentInChildren<ParticleSystem>();
      if (this.CompareTag("AddKick"))
      {
         _clip = Kick;
         pitch = 1;
      }
      else if (this.CompareTag("AddSnare"))
      {
         _clip = Snare;
         pitch = 1.1f;
      }
      else if (this.CompareTag("AddBass"))
      {
         _clip = Bass;
         pitch = 1.2f;
      }
      else if (this.CompareTag("AddArp1"))
      {
         _clip = Arp1;
         pitch = 1.3f;
      }
      else if (this.CompareTag("AddArp2"))
      {
         _clip = Arp2;
         pitch = 1.4f;
      }
      else if (this.CompareTag("AddLead1"))
      {
         _clip = Lead1;
         pitch = 1.5f;
      }
      else if (this.CompareTag("AddLead2"))
      {
         _clip = Lead2;
         pitch = 1.6f;
      }
   }
   void OnTriggerEnter(Collider other) 
   {
      _clip.CueClipLength(127);
      OneOffClip.Source.pitch = pitch;
      OneOffClip.CueClipASAP();
      rainbow.StartRainbowCycle((float)TempoClock.Instance.secondsPerMeasure/4);
      ready = true;
      particles.Play();
   }
   void OnTriggerExit(Collider other) 
   {
      //_clip.Stop();
      //rainbow.StopRainbowCycle();
   }

   void Update()
   {
      if (ready && _clip.IsPlaying())
      {
         active = true;
         ready = false;
      }
      if (active && !_clip.IsPlaying())
      {
         active = false;
         rainbow.StopRainbowCycle();
         particles.Stop();
      }
   }
}
