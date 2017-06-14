using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class testIO : MonoBehaviour {

    public static string m_sdCardPath;
    public static string CurrentSDCardPath
    {
        get
        {
            if (m_sdCardPath == null)
            {
                AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
                IntPtr getExternalStorageDirectoryMethod = AndroidJNI.GetStaticMethodID(jc.GetRawClass(), "getExternalStorageDirectory", "()Ljava/io/File;");
                IntPtr file = AndroidJNI.CallStaticObjectMethod(jc.GetRawClass(), getExternalStorageDirectoryMethod, new jvalue[] { });
                IntPtr getPathMethod = AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(file), "getPath", "()Ljava/lang/String;");
                IntPtr path = AndroidJNI.CallObjectMethod(file, getPathMethod, new jvalue[] { });
                m_sdCardPath = AndroidJNI.GetStringUTFChars(path);
                AndroidJNI.DeleteLocalRef(file);
                AndroidJNI.DeleteLocalRef(path);
                Debug.Log("m_sdCardPath = " + m_sdCardPath);
            }
            return m_sdCardPath;
        }

    }

    Text t;
    public Button selectBtn;
    string path;

	// Use this for initialization
	void Start () {
        t = GetComponentInChildren<Text>();
        selectBtn.onClick.AddListener(selectClicked);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    


    public void selectClicked()
    {
        string filename;
        t.text = "";
        path = CurrentSDCardPath + "/JimmyTest";
        string[] files = Directory.GetFiles(path, "*.txt");
        for(int i=0; i<files.Length; i++)
        {
            filename = files[i].Substring(path.Length);
            t.text += filename + "\n";
        }
    }
}
