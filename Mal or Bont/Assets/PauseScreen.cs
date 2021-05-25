using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static GameMaster gm;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public void Resume()
    {
        gm.pauseUI.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("APPLICATION QUIT!");
        Application.Quit();
    }
}
