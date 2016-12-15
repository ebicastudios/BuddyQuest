using UnityEngine;
using System.Collections;

enum SCREENS
{
    SPLASH_1 = 0,
    SPLASH_2 = 1,
    MAIN_MENU = 2
};

public class Camera_Controller : MonoBehaviour {


    GameObject fader1, fader2;

    void Awake()
    {
        fader1 = GameObject.Find("fader1");
        fader2 = GameObject.Find("fader2");
    }

    public void show_splash()
    {
        StartCoroutine("showSplash");
    }

    public void move(int where)
    {
        Vector3 pos = new Vector3(0,0,0);
        if (where == (int)SCREENS.SPLASH_1)
        {
            pos = GameObject.Find("splash_ebica").GetComponent<Transform>().position;
        }
        if (where == (int)SCREENS.SPLASH_2)
        {
            pos = GameObject.Find("splash_ebica").GetComponent<Transform>().position;
        }
        if (where == (int)SCREENS.MAIN_MENU)
        {
            pos = GameObject.Find("splash_ebica").GetComponent<Transform>().position;
        }
        pos.z = -100;
        this.GetComponent<Transform>().position = pos;
    }

    IEnumerator showSplash()
    {
        Vector3 f1_pos, f2_pos;

        // Set the two faders to be located between the camera and corresponding splash screens
        f1_pos = GameObject.Find("splash_ebica").GetComponent<Transform>().position;
        f2_pos = GameObject.Find("splash_second").GetComponent<Transform>().position;
        f1_pos.z = -0.25f;
        f2_pos.z = -0.25f;
        fader1.GetComponent<Transform>().position = f1_pos;
        fader2.GetComponent<Transform>().position = f2_pos;

        // Fade in first fader
        for (float f = 1f; f >= 0; f -= 0.008f)
        {
            Color c = fader1.GetComponent<SpriteRenderer>().color;
            c.a = f;
            fader1.GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        // Wait
        for(int i=0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Fade out first fader
        for (float f = 0f; f <= 1; f += 0.008f)
        {
            Color c = fader1.GetComponent<SpriteRenderer>().color;
            c.a = f;
            fader1.GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        // Move to second fader, and move first fader to next screen
        Vector3 move_pos = f2_pos;
        move_pos.z = -1.0f;
        this.GetComponent<Transform>().position = move_pos;
        f1_pos = GameObject.Find("main_menu").GetComponent<Transform>().position;
        f1_pos.z = -0.25f;
        fader1.GetComponent<Transform>().position = f1_pos;

        // Fade in second fader
        for (float f = 1f; f >= 0; f -= 0.008f)
        {
            Color c = fader2.GetComponent<SpriteRenderer>().color;
            c.a = f;
            fader2.GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        // Wait
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Fade out second fader
        for (float f = 0f; f <= 1; f += 0.008f)
        {
            Color c = fader2.GetComponent<SpriteRenderer>().color;
            c.a = f;
            fader2.GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        // Move to first fader (at main_menu)
        move_pos = f1_pos;
        move_pos.z = -1.0f;
        this.GetComponent<Transform>().position = move_pos;

        // Fade in first fader
        for (float f = 1f; f >= 0; f -= 0.008f)
        {
            Color c = fader1.GetComponent<SpriteRenderer>().color;
            c.a = f;
            fader1.GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        // Reset location of faders
        fader1.GetComponent<Transform>().position = new Vector3(-20f, 0f, 0f);
        fader2.GetComponent<Transform>().position = new Vector3(-20f, 0f, 0f);


    }
}