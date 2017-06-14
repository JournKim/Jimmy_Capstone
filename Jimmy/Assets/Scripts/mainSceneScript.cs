using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainSceneScript : MonoBehaviour {

    public Button btnPlay;
    public Button btnSheet;
    public Button btnTune;
    public Button btnQuit;

	// Use this for initialization
	void Start () {
        btnPlay.onClick.AddListener(onClickPlay);
        btnSheet.onClick.AddListener(onClickSheet);
        btnTune.onClick.AddListener(onClickTune);
        btnQuit.onClick.AddListener(onClickQuit);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
	}

    void onClickPlay()
    {
        SceneManager.LoadScene("selectSong");
    }

    void onClickSheet()
    {
        SceneManager.LoadScene("selectSheet");
    }

    void onClickTune()
    {
        SceneManager.LoadScene("tune");
    }

    void onClickQuit()
    {
        Application.Quit();
    }
}
