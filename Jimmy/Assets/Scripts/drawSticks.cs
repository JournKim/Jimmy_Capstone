using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawSticks : MonoBehaviour {
    public GameObject stickPrefab;
    GameObject[][] sticks = new GameObject[4][];
    float[][] freq = new float[4][];
    int[][] index = new int[4][];
    public Text t;

	// Use this for initialization
	void Start () {
        //pitches = new string[]{ "C","C#","D","D#","E","F","F#","G","G#","A","A#","B"};
        int zerox = 100;

        for(int i=0; i<4; i++)
        {
            sticks[i] = new GameObject[12];
            freq[i] = new float[12];
            index[i] = new int[12];
        }

        for(int i=0; i<4; i++)
        {
            {
                for(int j=0; j<12; j++)
                {
                    sticks[i][j] = Instantiate(stickPrefab) as GameObject;
                    sticks[i][j].transform.SetParent(this.GetComponent<RectTransform>(), false);
                    sticks[i][j].GetComponent<RectTransform>().position = new Vector3(zerox + 25 * i, 10, 0);
                }
            }
            
        }
        //float dist = 5.3833007812f; // (float)24000 / 4096;
        //float rt = Mathf.Pow(2, 1f / 12f);
        //float tmp = 440f;

        //freq[9] = tmp;
        //for(int i=8; i>=0; i--)
        //{
        //    freq[i] = freq[i + 1] / rt;
        //}

        //for(int i=10; i<stickNumber; i++)
        //{
        //    freq[i] = freq[i - 1] * rt;
        //}

        //for(int i=0; i<stickNumber; i++)
        //{
        //    index[i] = (int)Mathf.Round(freq[i] / dist);
        //    Debug.Log(freq[i] + ", "+ index[i]);
        //}
        //Debug.Log("rt : " + rt);
        //for(int i=0; i<3; i++)
        //{
        //    Debug.Log(tmp);
        //    tmp *= rt;
        //}
        //freq[0] = tmp;
        //index[0] = (int)(freq[0] / dist);
        /*
                //C
                freq[0] = 523; // 97
                index[0] = 97;
                ////C#
                freq[1] = 554; // 103
                index[1] = 103;
                ////D
                freq[2] = 587; // 109
                index[2] = 109;
                ////D#
                freq[3] = 622; // 116
                index[3] = 116;
                ////E
                freq[4] = 659; // 122
                index[4] = 122;
                ////F
                freq[5] = 699; // 130
                index[5] = 130;
                ////F#
                freq[6] = 740; // 137
                index[6] = 137;
                ////G
                freq[7] = 784; // 146
                index[7] = 146;
                ////G#
                freq[8] = 831; // 154
                index[8] = 154;
                ////A
                freq[9] = 880; // 163
                index[9] = 163;
                ////A#
                freq[10] = 932; // 173
                index[10] = 173;
                ////B
                freq[11] = 988; // 184
                index[11] = 184;
                ////C
                freq[12] = 1047; // 195
                index[12] = 195;
                */
        //for(int i=1; i<13; i++)
        //{
        //    freq[i] = freq[i - 1] * rt;
        //    index[i] = (int)(freq[i] / dist);
        //    //Debug.Log(i + ":" + index[i]);
        //}
    }

    // Update is called once per frame
    void Update () {

        //for (int i = 0; i < stickNumber; i++)
        //{
        //    average = 0;
        //   // average += fftSound.samples[index[i] - 2];
        //    average += fftSound.samples[index[i] - 1];
        //    average += fftSound.samples[index[i]];
        //    average += fftSound.samples[index[i] + 1];
        // //   average += fftSound.samples[index[i] + 2];

        //    if(max < average)
        //    {
        //        max = average;
        //        maxIndex = i;
        //    }
        //    //////////////////////////average *= (int)Mathf.Pow(2,i+1);

        //    sticks[i].GetComponent<RectTransform>().localScale = new Vector3(1, average * 1000, 0);
        //}
        //t.text = pitches[maxIndex % 12];
        
    }
}
