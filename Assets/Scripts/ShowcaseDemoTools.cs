using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowcaseDemoTools : MonoBehaviour
{
    /// <summary>
    ///  This Script is just basically tools to use during the showcase in case things fuck up or if we want to show something specific off.
    ///  I'm just going to add a restart button for now, then maybe see if there is something else I can add, though I may just keep it at that to avoid breaking anything
    /// </summary>

    public float restartCharge = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.R)) //player holds R
        {
            restartCharge += Time.deltaTime;
        }
        else // let go of R
        {
            restartCharge = 0;
        }

        if (restartCharge >= 10) // held R for 10 seconds
        {
            restartCharge = 0;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
