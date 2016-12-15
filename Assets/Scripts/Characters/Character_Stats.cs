using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character_Stats : MonoBehaviour {

    enum STATS
    {
        STR,
        DEX,
        CON,
        INT,
        WIS,
        LCK,
        QCK,
        CNC,
        CRIT
    }                       // Enum for indexing stats array

    Global_Functions global;            // Holds accses to Global_Functions
    string character_name;              // Holds Character's name for display
    public int[] stats;                 // Holds Character's stats for calculations. Indexed by enum STATS
    
    void Awake ()
    {
        stats = new int[9];
        global = GameObject.Find("Game_Manager").GetComponent<Global_Functions>();
    }                    // Initialization Functions

    public void loadStats(List<string> what)
    {
        string block_start, block_end;
        int index_start, index_end;

        // Construct block tags for comparison in iteration
        block_start = "CHAR_" + this.name.ToUpper();
        block_end = "END_" + block_start;

        // Assign indexers for new list construction
        index_start = 0;
        index_end = 0;

        while(what[index_start] != block_start)
        {
            index_start++;
            index_end++;
        }
        while(what[index_end] != block_end)
        {
            index_end++;
        }


        // Construct a new list containing only info for this particular Character Object
        List<string> this_object = new List<string>();
        int index_current = index_start;
        while(index_current != index_end)
        {
            this_object.Add(what[index_current]);
            index_current++;
        }

        // Assign Name
        character_name = global.findBlockSingle(this_object, "NAME");

        int.TryParse(global.findBlockSingle(this_object, "STR"), out stats[(int)STATS.STR]);
        int.TryParse(global.findBlockSingle(this_object, "DEX"), out stats[(int)STATS.DEX]);
        int.TryParse(global.findBlockSingle(this_object, "CON"), out stats[(int)STATS.CON]);
        int.TryParse(global.findBlockSingle(this_object, "INT"), out stats[(int)STATS.INT]);
        int.TryParse(global.findBlockSingle(this_object, "WIS"), out stats[(int)STATS.WIS]);
        int.TryParse(global.findBlockSingle(this_object, "LCK"), out stats[(int)STATS.LCK]);
        int.TryParse(global.findBlockSingle(this_object, "QCK"), out stats[(int)STATS.QCK]);
        int.TryParse(global.findBlockSingle(this_object, "CNC"), out stats[(int)STATS.CNC]);
        int.TryParse(global.findBlockSingle(this_object, "CRIT"), out stats[(int)STATS.CRIT]);

    }   // Load in the name and main stats. Depends on Global_Functions and Generic Collections
    public List<string> saveMe()
    {
        List<string> ret_list = new List<string>();

        ret_list.Add("CHAR_" + this.name.ToUpper());

        // Character Name
        ret_list.Add("NAME");
        ret_list.Add(character_name);
        ret_list.Add("END_NAME");

        // Character Stats
        ret_list.Add("STR");
        ret_list.Add(stats[(int)STATS.STR].ToString());
        ret_list.Add("END_STR");

        ret_list.Add("DEX");
        ret_list.Add(stats[(int)STATS.DEX].ToString());
        ret_list.Add("END_DEX");

        ret_list.Add("CON");
        ret_list.Add(stats[(int)STATS.CON].ToString());
        ret_list.Add("END_CON");

        ret_list.Add("INT");
        ret_list.Add(stats[(int)STATS.INT].ToString());
        ret_list.Add("END_INT");

        ret_list.Add("WIS");
        ret_list.Add(stats[(int)STATS.WIS].ToString());
        ret_list.Add("END_WIS");

        ret_list.Add("LCK");
        ret_list.Add(stats[(int)STATS.LCK].ToString());
        ret_list.Add("END_LCK");

        ret_list.Add("QCK");
        ret_list.Add(stats[(int)STATS.QCK].ToString());
        ret_list.Add("END_QCK");

        ret_list.Add("CNC");
        ret_list.Add(stats[(int)STATS.CNC].ToString());
        ret_list.Add("END_CNC");

        ret_list.Add("CRIT");
        ret_list.Add(stats[(int)STATS.CRIT].ToString());
        ret_list.Add("END_CRIT");

        ret_list.Add("END_CHAR_" + this.name.ToUpper());

/*
            string debug_out = "";
            for (int i = 0; i < ret_list.Count; i++)
            {
                debug_out += ret_list[i];
                debug_out += "\n";
            }
            Debug.Log(debug_out);
*/
        return ret_list;
        
    }
}
