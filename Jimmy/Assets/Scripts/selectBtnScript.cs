﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectBtnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(clicked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void clicked()
    {
        SceneManager.LoadScene("selectSong");
    }
}
