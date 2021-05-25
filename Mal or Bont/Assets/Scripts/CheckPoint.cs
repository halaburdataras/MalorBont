using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;
    private AudioManager audioManager;
    public Sprite statue_activated;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = transform.position;
            pos.z = 0;

            

            if(this.gameObject.GetComponent<SpriteRenderer>().sprite != statue_activated)
            {
                audioManager.PlaySound(gm.statueActivationSound);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = statue_activated;
            }

            

            gm.spawnPoint.position = pos;
        }
    }
}
