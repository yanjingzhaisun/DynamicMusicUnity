using UnityEngine;
using System.Collections;

public class MusicalClip : MonoBehaviour {

   public AudioClip Clip;
   public AudioSource Source;
   public int LengthOfClipIn16ths;
   public int Downbeat16th = 1;
   public int StartingBeatID;
   public int HowLongToPlayClipIn16ths;
   public bool Cued;
   private double next16thTime;
   private double sixteenthInSeconds;

   public MusicalClip (AudioClip clip, AudioSource source, int numberOf16ths, int downbeat16th) {
      Clip = clip;
      Source = source;
      LengthOfClipIn16ths = numberOf16ths;
      Downbeat16th = downbeat16th;
   }

   void Awake()
   {
      if (TempoClock.Instance == null)
      {
         GameObject TempoManager = new GameObject();
         TempoManager.AddComponent<TempoClock>();
      }
      TempoClock.Instance.Sixteenth += OnBeat;

      sixteenthInSeconds = TempoClock.Instance.secondsPerMeasure / 16;
      Source = GetComponent<AudioSource>();
      Clip = Source.clip;
      Source.playOnAwake = false;
      Source.bypassReverbZones = true;
   }

   public void CueClip() {
      Cued = true;
   }

   public void CueClip(int startingBeatID)
   {
      StartingBeatID = startingBeatID;
      Cued = true;
   }

   public void CueClipLength(int howLongToPlayClipIn16ths)
   {
      Cued = true;
      HowLongToPlayClipIn16ths = howLongToPlayClipIn16ths;
   }

   public void CueClipLength(int startingBeatID, int howLongToPlayClipIn16ths) {
      StartingBeatID = startingBeatID;
      Cued = true;
      HowLongToPlayClipIn16ths = howLongToPlayClipIn16ths;
   }

   public void CueClipASAP() {
      Source.clip = Clip;
      Source.PlayScheduled(next16thTime);
   }

   private void PlayOnBeat(double _time) {
      Source.clip = Clip;
      Source.PlayScheduled(_time);
      if (HowLongToPlayClipIn16ths != 0)
      {
         Source.SetScheduledEndTime(_time + (HowLongToPlayClipIn16ths * sixteenthInSeconds));
      }
      Cued = false;
   }

   public bool IsPlaying()
   {
      return Source.isPlaying;
   }

   public void Stop() {
      Source.SetScheduledEndTime(next16thTime); 
   }

   public void Stop(double timeToStop) {
      Source.SetScheduledEndTime(timeToStop);
   }


   void OnBeat(object s, TempoClock.BeatEventArgs e) {
         next16thTime = e.NextBeatTime;
         if (!Cued) return;
         if ((e.BeatID + 1) == StartingBeatID)
         {
            PlayOnBeat(e.NextBeatTime);
         }
         if ((e.BeatID + 1) % 16 == Downbeat16th)
         {
            PlayOnBeat(e.NextBeatTime);
         }
         else if (Downbeat16th == 16 && (e.BeatID + 1) % 16 == 0) {
            PlayOnBeat(e.NextBeatTime);
         }
      
   }
}
