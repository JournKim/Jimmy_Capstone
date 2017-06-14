using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class selectSongScript : MonoBehaviour {
    bool isPhone = true;

    bool checkPlatform()
    {
#if UNITY_EDITOR
        return false;
#endif

#if UNITY_ANDROID
        return true;
#endif
    }

    public GameObject songPrefab;
    public static string selectedFileName = null;
    public string path;
    public static string m_sdCardPath;
    public static string CurrentSDCardPath
    {
        get
        {
            if (m_sdCardPath == null)
            {
                AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
                IntPtr getExternalStorageDirectoryMethod = AndroidJNI.GetStaticMethodID(jc.GetRawClass()
                    , "getExternalStorageDirectory", "()Ljava/io/File;");
                IntPtr file = AndroidJNI.CallStaticObjectMethod(jc.GetRawClass()
                    , getExternalStorageDirectoryMethod, new jvalue[] { });
                IntPtr getPathMethod = AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(file)
                    , "getPath", "()Ljava/lang/String;");
                IntPtr path = AndroidJNI.CallObjectMethod(file, getPathMethod, new jvalue[] { });
                m_sdCardPath = AndroidJNI.GetStringUTFChars(path);
                AndroidJNI.DeleteLocalRef(file);
                AndroidJNI.DeleteLocalRef(path);
                Debug.Log("m_sdCardPath = " + m_sdCardPath);
            }
            return m_sdCardPath;
        }
    }

    
    public static song selectedSong;
    public static string ttttt;

	// Use this for initialization
	void Start () {
        chooseChord.running = false;
        isPhone = checkPlatform();

        Debug.Log("select Song");

        string filename;
        string[] files = { "shadow.txt", "test.txt" };
        if (isPhone)
        {
            path = CurrentSDCardPath + "/JimmyTest";
            files = Directory.GetFiles(path, "*.txt");
        }

        // 파일 스캔
        for (int i = 0; i < files.Length; i++)
        {
            if(isPhone)
                filename = files[i].Substring(path.Length+1);
            else filename = files[i];
            

            GameObject obj =  Instantiate(songPrefab);
            obj.transform.SetParent(this.GetComponent<RectTransform>());
            obj.transform.position = this.transform.position;
            obj.transform.position -= new Vector3(0, -860 + 200*i);
            obj.GetComponentInChildren<Text>().text = filename;
            obj.GetComponent<Button>().onClick.AddListener(() => songClicked(obj));
        }

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            selectedFileName = null;
            chooseChord.selectedFileName = selectedFileName;
            chooseChord.selected = false;
            SceneManager.LoadScene("spectrum");
            StopAllCoroutines();
        }
    }

    void songClicked(GameObject t)
    {
        selectedFileName = path + "/" + t.GetComponentInChildren<Text>().text;
        if(!isPhone)
        {
            selectedFileName = "C:\\Users\\Journey\\Documents\\2017 1학기\\컴퓨터공학종합설계\\Jimmy\\Assets\\Songs\\"+ t.GetComponentInChildren<Text>().text;
        }

        chooseChord.selectedFileName = selectedFileName;
        chooseChord.selected = true;

        selectedSong = new song();
        selectedSong.openSong(selectedFileName);
        ttttt = song.tt;

        Debug.Log(selectedFileName);
        SceneManager.LoadScene("spectrum");
    }
}
