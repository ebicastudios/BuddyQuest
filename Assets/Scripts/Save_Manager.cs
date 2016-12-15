// Buddy Quest
// Save Game Manager
// Save_Manager.cs
//
// This script manages the read/write functions of the saving feature,
// including constructing a display for the save/load screen. ANYTHING
// that reads or writes to a game save file is handled in here.
//
// Created By: Brandon Bush
// Last Update: 11/29/2016
// Version: 1.0
// Exception Guarantee: Iffy
// License: Ebica_Standard_License (Closed Source)
//
// To Implement: Header information in read/write

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Save_Manager : MonoBehaviour {

    // GameObjects
    GameObject[] Characters;

    // Specific Variables
    string[] filepaths;

    public string[] saves;
    public List<string> current_save;
    public bool saves_loaded = false;
    public bool specific_loaded = false;
    public bool characters_loaded = false;

    void Awake()
    {
        // Initialize GameObject
        Characters = new GameObject[5];
        Characters[0] = GameObject.Find("warrior");
        Characters[1] = GameObject.Find("mage");
        Characters[2] = GameObject.Find("thief");
        Characters[3] = GameObject.Find("archer");
        Characters[4] = GameObject.Find("bard");

        // Initialize Variables
        filepaths = new string[3];
        saves = new string[3];

        filepaths = new string[3];
        filepaths[0] = "./dat/sav/sav1.dat";
        filepaths[1] = "./dat/sav/sav2.dat";
        filepaths[2] = "./dat/sav/sav3.dat";
    }                          // Initialization stuff. Runs before all other functions.
    public void loadSaves()
    {
        for(int i=0; i < filepaths.Length; i++)
        {
            using(StreamReader sr = File.OpenText(filepaths[i]))
            {
                saves[i] = sr.ReadToEnd();
            }
        }
        saves_loaded = true;
    }               // Loads all saves into memory. Selection is done with loadSpecific(int).
    public void loadSpecific(int which)
    {
        if (!saves_loaded) { throw new System.Exception("Saves are not loaded"); }
        if (which < 0 || which > saves.Length) { throw new System.Exception("Index for load out of range"); }

        current_save = saves[which].Split(':').ToList();

        if (current_save.Count == 0) { throw new System.Exception("When loading current_save, list conversion contains 0 elements"); }

        specific_loaded = true;
    }   // Converts the save[which] string into a list of name current_save.
    public void initializeGameState()
    {
        if (!specific_loaded) { throw new System.Exception("Specific save not loaded"); }

        // Send the save file to each character object for loading. 
        characters_loaded = true;
        try
        {
            Characters[0].GetComponent<Character_Stats>().loadStats(current_save);
            Characters[1].GetComponent<Character_Stats>().loadStats(current_save);
            Characters[2].GetComponent<Character_Stats>().loadStats(current_save);
            Characters[3].GetComponent<Character_Stats>().loadStats(current_save);
            Characters[4].GetComponent<Character_Stats>().loadStats(current_save);
        }
        catch
        {
            characters_loaded = false;
        }
    }     // Send the current save file to various objects for loading. Actual assignment
                                             // of values is done by the objects themselves.

    public void saveSpecific(int which)
    {
        string write_out = "";
        string filepath;

        // Assign filepath. Throws exception if which is not 0, 1, or 2
        if (which == 0)
        {
            filepath = filepaths[0];
        }
        else if (which == 1)
        {
            filepath = filepaths[1];
        }
        else if (which == 2)
        {
            filepath = filepaths[2];
        }
        else
        {
            throw new System.Exception("Invalid save file selection. Index which out of range");
        }

        // Add header data to save file
        write_out += ":SAVE:\n";
        write_out += ":GENERAL:\n";
        write_out += ":END_GENERAL:\n";

        // Add character data to write_out string
        List<string>[] character_data = new List<string>[5];

        character_data[0] = Characters[0].GetComponent<Character_Stats>().saveMe();
        character_data[1] = Characters[1].GetComponent<Character_Stats>().saveMe();
        character_data[2] = Characters[2].GetComponent<Character_Stats>().saveMe();
        character_data[3] = Characters[3].GetComponent<Character_Stats>().saveMe();
        character_data[4] = Characters[4].GetComponent<Character_Stats>().saveMe();

        for(int j = 0; j < 5; j++)
        {
            for(int i = 0; i < character_data[j].Count; i++)
            {
                write_out += ":";
                write_out += character_data[j][i];
            }
            write_out += ":\n";
        }

        // Add Footer information to write_out string
        write_out += "\n:END_SAVE:\n";
        // Debug.Log(write_out);
        using (StreamWriter sw = new StreamWriter(filepath))
        {
            sw.WriteLine(write_out);
        }

    }   // Save Current Game State to Disc
}
