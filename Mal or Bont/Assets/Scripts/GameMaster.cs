using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    private AudioManager audioManager;

    public string mainThemeSound;
    public string gemCollectionSound;
    public string statueActivationSound;
    public string playerDeathSound;

    private bool isPlaying = false;

    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject gemUI;
    public GameObject pauseUI;

    private bool isPaused = false;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        audioManager = AudioManager.instance;

        if(audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene");
        }
    }

    void Update()
    {
        if (!isPlaying)
        {
            isPlaying = !isPlaying;
            audioManager.PlaySound(mainThemeSound);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                pauseUI.SetActive(false);
            }
            else
            {
                pauseUI.SetActive(true);
            }
            isPaused = !isPaused;
        }
    }

    public int gemsCount = 0;
    public Text gemsUI;
    public Text finalScore;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public Vector2 lastCheckPointPos;
    public int spawnDelay = 2;

    private GameObject player;

    public void EndGame()
    {
        gemUI.SetActive(false);
        gameOverUI.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);

        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public static void killPlayer(Player mal)
    {
        Destroy (mal.gameObject);
        gm.StartCoroutine (gm.RespawnPlayer());
    }
}
