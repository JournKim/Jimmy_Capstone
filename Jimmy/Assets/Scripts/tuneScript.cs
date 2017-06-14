using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tuneScript : MonoBehaviour {
    public static int selected = 0;
    public Button[] strings = new Button[6];
    public Text[] stringNums = new Text[6];
    int[] pitches = new int[6];
    float[] freqOfA = { 440f, 880f, 1760f, 3520f };
    float[][] freqs = new float[4][];
    int[][] indexs = new int[4][];
    string[] chars = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    public Text t;
    int max;
	// Use this for initialization
	void Start () {
        selected = 0;
        t = GetComponent<Text>();

        float rt = Mathf.Pow(2, 1f / 12f);

        for (int i = 0; i < 4; i++)
        {
            indexs[i] = new int[12];
            freqs[i] = new float[12];
            freqs[i][9] = freqOfA[i];

            for (int j = 9; j > 0; j--)
            {
                freqs[i][j - 1] = freqs[i][j] / rt;
            }

            for (int j = 10; j < 12; j++)
            {
                freqs[i][j] = freqs[i][j - 1] * rt;
            }

            for (int j = 0; j < 12; j++)
            {
                // 해당 주파수의 신호가 들어있는 샘플링 배열의 index를 저장한다.
                indexs[i][j] = (int)(Mathf.Round(freqs[i][j] / fftSound.dist));
            }
        }

        pitches[0] = 4; // 6번줄 E
        pitches[1] = 9; // 5번줄 A
        pitches[2] = 2; // 4번줄 D
        pitches[3] = 7; // 3번줄 G
        pitches[4] = 11; // 2번줄 B
        pitches[5] = 4; // 1번줄 E(high)

        for(int i=0; i<6; i++)
        {
            stringNums[i] = strings[i].GetComponentInChildren<Text>();
            //strings[i].GetComponentInChildren<Text>(); 
        }
    }
	
	// Update is called once per frame
	void Update () {
        max = 0;
		for(int i=0; i<8192; i++)
        {
            if(fftSound.samples[i] > fftSound.samples[max])
                max = i;
        }
        for(int i=0; i<4; i++)
        {
            for(int j=0; j<12; j++)
            {
                if(indexs[i][j] == max)
                {
                    t.text = chars[j];
                }
            }
        }

        if (selected < 6)
        {
            for (int i = 0; i < 4; i++)
            {
                stringNums[selected].color = Color.red;
                if (max == indexs[i][pitches[selected]])
                {
                    stringNums[selected].color = Color.green;
                    strings[selected].GetComponent<Image>().color = Color.green;
                    selected++;
                }
            }
        }
        
	}
}
