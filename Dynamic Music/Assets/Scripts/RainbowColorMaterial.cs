using UnityEngine;
using System.Collections;

public class RainbowColorMaterial : MonoBehaviour {

   public Gradient gradient;
   public bool RainbowOn = false;
   public bool syncToTempo;
   public Material _material;
   public float duration;
   private Color StartColor;
   public Color color;

   private Material _rainbowMaterial;
   private bool firstTime;
   void Start()
   {
      if (_material == null)
      {
         try
         {
            _material = GetComponent<Renderer>().material;
            StartColor = _material.color;
         }
         catch (System.Exception e)
         {
            //Who cares this is a novelty script
         }
      }

      if (TempoClock.Instance != null)
      {
         TempoClock.Instance.Half += OnBeat;
      }
   }

   public void StartRainbowCycle() {
      RainbowOn = true;
   }

   public void StartRainbowCycle(float _duration)
   {
      duration = _duration;
      RainbowOn = true;
   }

   public void StopRainbowCycle() {
      RainbowOn = false;
   }

   void Update()
   {
      if (RainbowOn)
      {
         float t = Mathf.Repeat(Time.time, duration ) / duration;
         color = gradient.Evaluate(t);

         if (_material != null)
         {
            _material.SetColor("_Color", color);
         }
         color = color * Mathf.LinearToGammaSpace(2f);
         if (_material != null)
         {
            _material.SetColor("_EmissionColor", color);
         }
      }
      else
      {
         if (_material != null)
         {
            _material.color = StartColor;
         }
      }
   }


   void OnBeat (object s, TempoClock.BeatEventArgs e) {
   if (syncToTempo) duration = (float) (e.NextBeatTime - e.CurrentTime);
   }

}


