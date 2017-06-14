using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

    public Button btn;

	// Use this for initialization
	void Start () {

        Debug.Log("Hello World!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void btnClicked()
    {
        Debug.Log("클릭");
    }

}
