using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    private string[] choices;
    public int choice_size;

    void Awake()
    {
        choices = new string[choice_size];
    }
}
