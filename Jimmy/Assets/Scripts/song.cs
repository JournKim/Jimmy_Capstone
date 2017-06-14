using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(File))]
public class song : MonoBehaviour {

    Text t;
    public static string tt;
    public int n1;
    public int n2;
    public List<int> l0 = new List<int>();
    public List<int> l1 = new List<int>();

    public int rythm;

	// Use this for initialization
	void Start () {
        Debug.Log("song created");
        
        tt = "success";
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public void printSong()
    {
        for(int i=0; i<l0.Count; i++)
        {
            Debug.Log(l0[i] + " " + l1[i]);
        }
        
    }

    public int calcRythm()
    {
        Debug.Log(n1 + "," + n2);
        if (n1 == 4 && n2 == 4)
            return 4;
        if (n1 == 3 && n2 == 4)
            return 3;
        




        return 0;
    }

    public string openSong(string path)
    {
        chooseChord.delayCount = 3;
        if(!File.Exists(path))
        {
            return "File Not Exist";
        }
        

        //byte[] a;
        string a = File.ReadAllText(path);
        //a = System.IO.File.ReadAllBytes(path);
        tt = a;
        
        int[] tmp = null;

        int i = 0;
        n1 = n2 = 0;

        while (a[i] >= '0' && a[i] <= '9')
        {
            n1 *= 10;
            n1 += a[i++] - '0';
        }
        i++;
        while (a[i] >= '0' && a[i] <= '9')
        {
            n2 *= 10;
            n2 += a[i++] - '0';
        }

        tt = System.IO.File.ReadAllText(path);
        //char[] a = { '4',' ','4',' ','C',' ','C',' ','C',' ','C',' ','G',' ','G',' ','G',' ','G',' ','A','m',' ','A','m',' ','A','m',' ','A','m',' ','E','m',' ','E','m',' ','E','m',' ','E','m',' '};
        for (; i<a.Length; i++)
        {
            if (a[i] >= 'A' && a[i] <= 'G')
                tmp = new int[2];
            switch((char)a[i])
            {
                // A -> chords[0][0];
                case 'C':
                    tmp[0] = 0;
                    tmp[1] = 0;
                    break;
                case 'D':
                    tmp[0] = 2;
                    tmp[1] = 0;
                    break;
                case 'E':
                    tmp[0] = 4;
                    tmp[1] = 0;
                    break;
                case 'F':
                    tmp[0] = 5;
                    tmp[1] = 0;
                    break;
                case 'G':
                    tmp[0] = 7;
                    tmp[1] = 0;
                    break;
                case 'A':
                    tmp[0] = 9;
                    tmp[1] = 0;
                    break;
                case 'B':
                    tmp[0] = 11;
                    tmp[1] = 0;
                    break;
                case '#':
                    tmp[0]++;
                    tmp[1] = 0;
                    break;

                // minorchord
                case 'm':
                    tmp[1] = 1;
                    break;
                //7 chord
                case '7':
                    tmp[1] += 2;
                    break;

                //sus4 chord
                case 's':
                    tmp[1] = 4;
                    i += 3;
                    break;
                default:
                    // tmp를 l에 넣어준다.
                    if(tmp != null)
                    {
                        l0.Add(tmp[0]);
                        l1.Add(tmp[1]);
                        tmp = null;
                    }
                    break;
            }
        }
        return "Read Song Complete";
    }
    
}
