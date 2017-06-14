using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class practice : MonoBehaviour {
    public static int BPM;
    public Button btn;
    public static bool start;
	// Use this for initialization
	void Start () {
        btn.onClick.AddListener(clicked);
	}
	
	// Update is called once per frame
	void Update () {
		if(!start)
        {
            BPM = 60;
            start = true;
        }
	}

    public void clicked()
    {
        Debug.Log(selectSongScript.selectedFileName);
        
    }
}
