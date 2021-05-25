using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCollector : MonoBehaviour
{
    private GameMaster gm;
    private AudioManager audioManager;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene");
        }
    }

    private bool colected = false;
    public Text text;

    void Update()
    {
        colected = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Gem" && !colected)
        {
            colected = true;
            gm.gemsCount += 1;
            gm.gemsUI.text = gm.gemsCount.ToString();
            gm.finalScore.text = gm.gemsCount.ToString();
            audioManager.PlaySound(gm.gemCollectionSound);
            Destroy(other.gameObject);
        }
    }
}
