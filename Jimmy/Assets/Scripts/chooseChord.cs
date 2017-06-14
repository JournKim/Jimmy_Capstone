using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 코드
class chord
{
    public int[] idx; // 코드를 구성하는 음들을 저장.

    public void majorChord(int i)
    {
        idx = new int[3];
        idx[0] = i;
        idx[1] = i + 4;
        idx[2] = i + 7;
    }
    public void minorChord(int i)
    {
        idx = new int[3];
        idx[0] = i;
        idx[1] = i + 3;
        idx[2] = i + 7;
    }
    public void dominant7Chord(int i)
    {
        idx = new int[4];
        idx[0] = i;
        idx[1] = i + 4;
        idx[2] = i + 7;
        idx[3] = i + 10;
    }
    public void major7Chord(int i)
    {
        idx = new int[4];
        idx[0] = i;
        idx[1] = i + 4;
        idx[2] = i + 7;
        idx[3] = i + 11;
    }

    public void minor7Chord(int i)
    {
        idx = new int[4];
        idx[0] = i;
        idx[1] = i + 3;
        idx[2] = i + 7;
        idx[3] = i + 10;
    }

    public void sus4Chord(int i)
    {
        idx = new int[3];
        idx[0] = i;
        idx[1] = i + 5;
        idx[2] = i + 7;
    }


}

public class chooseChord : MonoBehaviour {
    public static string selectedFileName = null;
    int maxa, maxb;
    public static bool selected = false;
    public Button bpmUp, bpmDown;
    public Text bpmText;
    public Text chordNow, chordNext;
    public Canvas canv;
    public RawImage bg;
    public string[] chordsString;

    int[] bases;
    chord[][] chords;

    float[] freqOfA = { 440f, 880f, 1760f, 3750f };
    float[][] freqs = new float[4][];
    int[][] indexs = new int[4][];

    Text t;
    public Text t2;

    void initializeChords()
    {
        bases = new int[24];
        chords = new chord[12][];
        for (int i = 0; i < 12; i++)
        {
            bases[i] = i;
            bases[i + 12] = i;
            // chords[0][0] :  C, [1] : Cm, [2] : C7, [3] : Cm7, [4] : Csus4
            chords[i] = new chord[5];

            for(int j=0; j<5; j++)
                chords[i][j] = new chord();

            chords[i][0].majorChord(i);
            chords[i][1].minorChord(i);
            chords[i][2].dominant7Chord(i);
            chords[i][3].minor7Chord(i);
            chords[i][4].sus4Chord(i);
        }
    }

    public static int now = 0;
    public static int delayCount=3;
    public static bool running = false;
    public static int root, variation;

    // Use this for initialization
    void Start () {
        bg = canv.GetComponent<RawImage>();
        bg.color = Color.black;
        root = variation = 0;
        bpmUp.onClick.AddListener(bpm_up);
        bpmDown.onClick.AddListener(bpm_down);
        t = GetComponent<Text>();

        float rt = Mathf.Pow(2, 1f / 12f);
        for (int i=0; i<4; i++)
        {
            indexs[i] = new int[12];
            freqs[i] = new float[12];
            freqs[i][9] = freqOfA[i];

            for (int j = 9; j > 0; j--)
                freqs[i][j - 1] = freqs[i][j] / rt;

            for (int j = 10; j < 12; j++)
                freqs[i][j] = freqs[i][j - 1] * rt;
            
            for (int j = 0; j < 12; j++)
                indexs[i][j] = (int)(Mathf.Round(freqs[i][j] / fftSound.dist));
        }

        // 코드정보 입력.
        initializeChords();
        
        // 판별용 코드들.
    }

    public static bool chord_match = false;

    IEnumerator checkChord()
    {
        //Debug.Log("checkChord()");
        
        while(selected)
        {
            if (scanAmp2(root, variation) && !chord_match)
            {
                chord_match = true;
                bg.color = Color.green;
                //t2.color = Color.green;
            }
            yield return new WaitForSeconds(0);
        }
        
        
    }

    public float deltaTime = 0f;

    IEnumerator fadeOutChord()
    {
        float f = 0.01f;
        float t = (60f / (float)practice.BPM);
        while (chordNow.color.r > 0 && !chord_match)
        {
            chordNow.color -= new Color(f, f, f, f);
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator run()
    {
        while (selected)
        {
            if(delayCount> 0)
            {
                now = 0;
                t.text = delayCount + "";
                delayCount--;
                chordNow.text = "";
                chordNext.text = chordsString[0] +"\n" + chordsString[1];
            }
            else
            {
                chordNow.color = Color.white;
                chordNow.text = chordsString[now];
                if (now != chordsString.Length - 1)
                {
                    chordNext.text = chordsString[now + 1];
                    if (now != chordsString.Length - 2)
                        chordNext.text += ("\n" + chordsString[now + 2]);
                }
                else
                {
                    chordNext.text = "End";
                }
                
                chord_match = false;
                try
                {
                    StartCoroutine(fadeOutChord());
                    t.text = now + "";
                    chord_match = false;
                    root = selectSongScript.selectedSong.l0[now];
                    variation = selectSongScript.selectedSong.l1[now];

                    t.text = now + ":" + getChordName(root, variation);
                    now++;
                    
                    if (chord_match)
                    {
                        //t2.color = Color.green;
                        t2.text = "맞음";
                    }
                    else
                    {
                        bg.color = Color.red;
                        //t2.color = Color.red;
                        t2.text = "틀림";
                    }

                    if (selectSongScript.selectedSong.l0.Count == now)
                    {
                        selected = false;
                    }
                }
                catch (Exception e)
                {
                    t2.text = e.Message;
                }
            }
            
            yield return new WaitForSeconds(60f/practice.BPM);
        }

    }

    public int rythm;

    void makeSongText()
    {
        Debug.Log("makeSongText");
        List<int> n1 = selectSongScript.selectedSong.l0;
        List<int> n2 = selectSongScript.selectedSong.l1;
        rythm = selectSongScript.selectedSong.calcRythm();
        chordsString = new string[n1.Count];

        for (int i=0; i<n1.Count; i++)
        {
            chordsString[i] = getChordName(n1[i], n2[i]);
        }
    }

    // Update is called once per frame
    void Update () {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (selected)
        {
            if(!running)
            {
                makeSongText();
                StartCoroutine(checkChord());
                StartCoroutine(run());
                running = true;
            }
        }
        else 
        {
            t.text = "End";
        }
        
        //t2.text = selectSongScript.ttttt;
        
    }

    string getChordName(int a, int b)
    {
        string[] bases = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        string[] vari = { "", "m", "7", "m7", "sus4" };
        string result = "";

        result += bases[a];
        result += vari[b];
        
        return result;
    }
    bool pitchPlayed(int t)
    {
        float sum = 0;
        for(int i=0; i<4; i++)
        {
            sum += fftSound.samples[indexs[i][bases[t]]] * 100f;
        }

        if (sum > 1f)
            return true;
        else
            return false;
    }
    bool scanAmp2(int a, int b)
    {
        int[] idx = chords[a][b].idx;

        for(int i=0; i<idx.Length; i++)
        {
            if (!pitchPlayed(idx[i]))
                return false;
        }
        return true;
        //float[] amp = new float[idx.Length];
        //for(int i=1; i<4; i++)
        //{
        //    for(int j=0; j<idx.Length; j++)
        //    {
        //        amp[j] += fftSound.samples[ indexs[i][ bases[ idx[j] ] ] ] * 90f;
        //    }
        //}

        //float total = 0;
        //for (int i = 0; i < idx.Length; i++)
        //{
        //    total += amp[i];
        //}
        
        //if (total > 2.5)
        //    return true;
        //else
        //    return false;
    }

    bool scanAmp(int[] idx)
    {
        float[] amp = new float[3];
        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                amp[j] += fftSound.samples[indexs[i][idx[j]]] * 100f;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (amp[i] < 0.7)
            {
                return false;
            }
        }

        return true;
    }
    
    void bpm_up()
    {
        practice.BPM++;
        bpmText.text = practice.BPM + "";
    }
    void bpm_down()
    {
        practice.BPM--;
        bpmText.text = practice.BPM + "";
    }
}
