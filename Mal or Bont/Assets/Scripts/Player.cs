using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();

    private AudioManager audioManager;
    private GameMaster gm;

    public int fallBoundary = -20;

    public Transform malbonte;
    public Transform deathPrefab;
    public GameObject malbonteObj;
    public GameObject clone;

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

    void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(9999);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crystal"))
        {
            DamagePlayer(9999);
        }
        if (other.CompareTag("Enemy"))
        {
            DamagePlayer(9999);
        }
        if (other.CompareTag("Finish"))
        {
            gm.EndGame();
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;

        if(playerStats.Health <= 0)
        {
            malbonteObj = GameObject.FindGameObjectWithTag("Player");
            malbonte = malbonteObj.transform;

            Instantiate(deathPrefab, malbonte.position, malbonte.rotation);

            clone = GameObject.FindGameObjectWithTag("Particles");
            Destroy(clone, 2.5f);

            audioManager.PlaySound(gm.playerDeathSound);

            GameMaster.killPlayer(this);
        }
    }
}
