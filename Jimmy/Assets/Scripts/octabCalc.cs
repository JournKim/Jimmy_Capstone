using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octabCalc : MonoBehaviour {

    public float freqOfA;
    public GameObject[] stick = new GameObject[12];
    int[] index = new int[12];
    float[] freq = new float[12];

    const float sign = 0.0001f;
    //Queue<float>[] q = new Queue<float>[12];
    //float[] sum = new float[12];
   // bool[] flag = new bool[12];
    

	// Use this for initialization
	void Start () {
        float rt = Mathf.Pow(2, 1f / 12f);

        // freq[9]에 A의 주파수 입력.
        freq[9] = freqOfA;

        // 반음씩 내려가면서 계산한 주파수를 입력.
        for(int i=9; i>0; i--)
        {
            freq[i - 1] = freq[i] / rt;
        }
        // 반음씩 올라가면서 계산한 주파수를 입력
        for(int i=10; i<12; i++)
        {
            freq[i] = freq[i - 1] * rt;
        }

        //
        for(int i=0; i<12; i++)
        {
            // 해당 주파수의 신호가 들어있는 샘플링 배열의 index를 저장한다.
            index[i] = (int)(Mathf.Round(freq[i] / fftSound.dist));
        }
        
	}
	
	// Update is called once per frame
	void Update () {

        float amp;
        for (int i=0; i<12; i++)
        {
            amp = fftSound.samples[index[i]] * 100f;
            if (amp > 1)
                amp = 1;
            stick[i].transform.localScale = new Vector3(1, amp, 1);
        }
    }
}
