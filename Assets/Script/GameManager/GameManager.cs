using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioBackgoundMusic;
    public AudioClip audioStart;
    //public AudioClip audioOnePlayer;
    public AudioClip audioOnePlayer;
    public AudioClip audioTwoPlayer;
    public AudioClip audioThreePlayer;
    public AudioClip audioFourPlayer;
    public AudioClip thunderAndRain;

    private PlayerSpawner playerSpawner;
    private Camera miniMapCam;
    private Canvas miniMapCanvas;
    private bool showMiniMap;
    private int player;
    private int playertmp;
    private int counter;

    void Awake()
    {
        counter = 0;
        player = 1;
        playertmp = player;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioStart, 1f);
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        miniMapCam = GameObject.Find("MiniMapCam").GetComponent<Camera>();
        miniMapCanvas = GameObject.Find("MiniMapCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        showMiniMap = false;
        miniMapCam.enabled = false;
        miniMapCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {

        if(Input.GetKeyDown("c"))
        {
            if(showMiniMap)
            {
                miniMapCanvas.enabled = false;
                miniMapCam.enabled = false;
                showMiniMap = false;
            } else {
                miniMapCam.enabled = true;
                miniMapCanvas.enabled = true;
                showMiniMap = true;
            }
        }

        if (Input.GetKey(KeyCode.Escape) || InputManager.OneStartButton())
        {
            if(counter > 30)
            {
                Application.Quit();
            } else {
                counter++;
            }
        }   

        if (Input.GetKeyUp(KeyCode.Escape) || InputManager.OneStartButton())
        {
            counter = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(InputManager.OneR1Button() && player < 4) {
            player += 1;
        }

        if (InputManager.OneL1Button() && player > 1)
        {
            player -= 1;
        }

        if(player != playertmp) {
            playertmp = player;
            playerSpawner.setPlayers(player);
            playerSpawner.createPlayers();            

            switch (player)
            {
                case 1: audioSource.PlayOneShot(audioOnePlayer, 0.8f); break;
                case 2: audioSource.PlayOneShot(audioTwoPlayer, 0.8f); break;
                case 3: audioSource.PlayOneShot(audioThreePlayer, 0.8f); break;
                case 4: audioSource.PlayOneShot(audioFourPlayer, 0.8f); break;
                default: break;
            }
        }

        if (Input.GetKeyDown("1") && player != 1)
        {
            player = 1;
        }
        if (Input.GetKeyDown("2") && player != 2)
        {
            player = 2;
        }
        if (Input.GetKeyDown("3") && player != 3)
        {
            player = 3;
        }
        if (Input.GetKeyDown("4") && player != 4)
        {
            player = 4;
        }
    }
}