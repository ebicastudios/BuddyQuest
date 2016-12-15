using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Global_Functions : MonoBehaviour {

    public bool debug = true;

    void Write_Log(string what)
    {
        try
        {
            if (!File.Exists("./debug_log.txt"))                            // If the file does not exist, create it.
            {
                File.Create("./debug_log.txt");
            }
            using (StreamWriter sw = File.AppendText("./debug_log.txt"))    // Open filestream for append
            {
                sw.WriteLine(what + "\n");
            }
        }
        catch (DirectoryNotFoundException)
        {
            Debug.Log("DirectoryNotFoundException thrown in Global_Functions: Write_Log(string), debug file not written");
        }
        catch (PathTooLongException)
        {
            Debug.Log("PathTooLongException thrown in Global_Functions: Write_Log(string), debug file not written");
        }
        catch
        {
            Debug.Log("UnknownException thrown in Global_Functions: Write_Log(string), debug file not written");
        }
    }                   // Debug Log Reporting. Used in every other script to log for Debug Purposes.
    public string findBlockSingle(List<string> what, string which)
    {
        int index = 0;
        while (what[index] != which)
        {
            index++;
        }
        index++;

        return what[index];
    }

}
