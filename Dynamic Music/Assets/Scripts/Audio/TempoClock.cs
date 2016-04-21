using UnityEngine;
using System.Collections;

public class TempoClock : MonoBehaviour {

    public static TempoClock Instance;
    void Awake()
    {
        Instance = this;
        secondsPerMeasure = (60 / BPM * 4);
        samplesPerMeasure = secondsPerMeasure * samplerate;
    }
        
    public double BPM;
   public float waitBeforeStart = 1f;
   public double samplerate;
    public double secondsPerMeasure;
   public double samplesPerMeasure;
    public double starttime;
    public int sixteenthcount=0;
    public int eighthcount = 0;
    public int quartercount = 0;
    public int halfcount = 0;
    public int measurecount = 0;
    public double nextMeaure;
    public double nextSixteenth;
    public double nextEighth;
    public double nextQuarter;
    public double nextHalf;
   private bool firstTime = true;

    public class BeatEventArgs
    {
        public enum EBeatType{Sixteenth, Eighth, Quarter, Half, Full, Measure };
        public EBeatType BeatType;
        public int BeatID;
         public double CurrentTime;
        public double BeatTime;
        public double NextBeatTime;
        
        public BeatEventArgs() { }
        public BeatEventArgs(EBeatType beatType, int beatID, double currentTime, double beatTime, double nextBeatTime)
        {
            BeatType = beatType;
            BeatID = beatID;
            CurrentTime = currentTime;
            BeatTime = beatTime;
            NextBeatTime = nextBeatTime;
        }

    }
    public delegate void BeatEvent(object sender, BeatEventArgs args);
    public event BeatEvent Beat;
    public event BeatEvent Sixteenth;
    public event BeatEvent Eighth;
    public event BeatEvent Quarter;
    public event BeatEvent Half;
    public event BeatEvent Measure;

	void Start () {
      double startTime = AudioSettings.dspTime;
      
   }

   void FirstBeat(double time) {
      secondsPerMeasure = (60 / BPM * 4);
      nextMeaure = time + secondsPerMeasure;
      nextSixteenth = time + secondsPerMeasure / 16;
      nextEighth = time + secondsPerMeasure / 8;
      nextQuarter = time + secondsPerMeasure / 4;
      nextHalf = time + secondsPerMeasure / 2;
      firstTime = false;
   }
	
	void Update () {
      double currentTime = AudioSettings.dspTime;
      secondsPerMeasure = (60 / BPM * 4);
      if (firstTime) FirstBeat(currentTime); 
      if (currentTime > nextSixteenth)
      {
         sixteenthcount++;
         if (Sixteenth != null)
            Sixteenth(this, new BeatEventArgs(BeatEventArgs.EBeatType.Sixteenth, sixteenthcount, currentTime, nextSixteenth, nextSixteenth + secondsPerMeasure / 16));
         if (Beat != null)
            Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Sixteenth, sixteenthcount, currentTime, nextSixteenth, nextSixteenth + secondsPerMeasure / 16));
         nextSixteenth += secondsPerMeasure / 16;
         if (sixteenthcount % 2 != 0)
         {
            eighthcount++;
            if (Eighth != null)
               Eighth(this, new BeatEventArgs(BeatEventArgs.EBeatType.Eighth, eighthcount, currentTime, nextEighth, nextEighth + secondsPerMeasure / 8));
            if (Beat != null)
               Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Eighth, eighthcount, currentTime, nextEighth, nextEighth + secondsPerMeasure / 8));
            nextEighth += secondsPerMeasure / 8;
            if (eighthcount % 2 != 0)
            {
               quartercount++;
               if (Quarter != null)
                  Quarter(this, new BeatEventArgs(BeatEventArgs.EBeatType.Quarter, quartercount, currentTime, nextQuarter, nextQuarter + secondsPerMeasure / 4));
               if (Beat != null)
                  Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Quarter, quartercount, currentTime, nextQuarter, nextQuarter + secondsPerMeasure / 4));
               nextQuarter += secondsPerMeasure / 4;
               if (quartercount % 2 != 0)
               {
                  halfcount++;
                  if (Half != null)
                     Half(this, new BeatEventArgs(BeatEventArgs.EBeatType.Half, halfcount, currentTime, nextHalf, nextHalf + secondsPerMeasure / 2));
                  if (Beat != null)
                     Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Half, halfcount, currentTime, nextHalf, nextHalf + secondsPerMeasure / 2));
                  nextHalf += secondsPerMeasure / 2;
                  if (halfcount % 2 != 0)
                  {
                     measurecount++;
                     if (Measure != null)
                        Measure(this, new BeatEventArgs(BeatEventArgs.EBeatType.Measure, measurecount, currentTime, nextMeaure, nextMeaure + secondsPerMeasure));
                     if (Beat != null)
                        Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Measure, measurecount, currentTime, nextMeaure, nextMeaure + secondsPerMeasure));
                     nextMeaure += secondsPerMeasure;
                  }
               }
            }
         }
      }
	}
}
