using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class fftSound : MonoBehaviour {

    public AudioSource source;

    public static float[] samples = new float[8192];
    public static float[] freqs = new float[8192];
    public static float dist = 48000f / 8192f;

	// Use this for initialization
	void Start () {
        source.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        source.loop = true;

        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("Mic On!");
        for (int i=0;i<8192; i++)
        {
            freqs[i] = dist * i;
        }
        
        source.Play();
	}
    public bool played = false;

	// Update is called once per frame
	void Update () {
        //if (chooseChord.selected && !played)
        //{
        //    source.Play();
        //    played = true;
        //}

        //float max = 0;
        //int idx=0;

        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
       
        //for(int i=0; i<8192; i++)
        //{
        //    if (samples[i] > 0.01)
        //    {
        //        //Debug.Log(i);
        //    }
        //    //if(max < samples[i])
        //    //{
        //    //    max = samples[i];
        //    //    idx = i;
        //    //}
        //}
        //Debug.Log(idx);
        if(Input.GetKey(KeyCode.Escape))
        {
            selectSongScript.selectedSong = null;
            StopAllCoroutines();
            source.Stop();
            SceneManager.LoadScene("main");

        }
    }
}
