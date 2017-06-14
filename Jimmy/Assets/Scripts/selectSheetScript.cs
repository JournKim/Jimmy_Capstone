using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class selectSheetScript : MonoBehaviour
{
    string filename;
    public Button btn,btn2, backBtn;
    string[] files;
    // Use this for initialization
    void Start()
    {
        btn.onClick.AddListener(songClicked);
        btn.onClick.AddListener(songClicked2);
        img.SetActive(false);

        backBtn.onClick.AddListener(()=>SceneManager.LoadScene("main"));
        backBtn.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("main");
            StopAllCoroutines();
        }
    }

    public GameObject img;
    public Text completeText;

    int i = 0;

    IEnumerator run()
    {
        while (i < 3)
        {
            if (i == 0)
            {
                convert();
                i++;
                
            }
            else if (i == 1)
            {
                i++;
                yield return new WaitForSeconds(5);
            }
            else if (i == 2)
            {
                complete();
                i++;
                yield return new WaitForSeconds(2);
            }

        }
    }

    void convert()
    {
        completeText.text = "변환중...";
        imgToText(filename);
        img.SetActive(true);
    }

    void complete()
    {
        completeText.text = "변환 완료\n";
        completeText.text += filename;
        backBtn.gameObject.SetActive(true);
    }

    int songnum;
    public void songClicked()
    {
        i = 0;
        songnum = 0;
        Debug.Log("clickd");
        StartCoroutine(run());
    }
    public void songClicked2()
    {
        i = 0;
        songnum = 1;
        Debug.Log("clickd");
        StartCoroutine(run());
    }

    void imgToText(string filename)
    {
        int a;
    }
}
