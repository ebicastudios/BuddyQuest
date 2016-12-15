using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Debug_Tester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("dev_cursor").GetComponent<Main_Menu>().in_control = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
