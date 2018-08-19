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

    public PlayerSpawner playerSpawner;
    private int player;
    private int playertmp;
    int counter;

    private void Awake()
    {
        counter = 0;
        player = 1;
        playertmp = player;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioStart, 0.8f);
        //audioSource.PlayOneShot(audioBackgoundMusic, 0.15f);
    }

    // Update is called once per frame
    void FixedUpdate () {   

        if (Input.GetKey(KeyCode.Escape) || InputManager.OneStartButton())
        {
            if(counter > 30)
            {
                Application.Quit();
                Debug.Log("Programm beendet");
            } else {
                counter++;
            }
        }   

        if (Input.GetKeyUp(KeyCode.Escape) || InputManager.OneStartButton())
        {
            counter = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
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
                case 1: audioSource.PlayOneShot(audioOnePlayer, 0.5f); break;
                case 2: audioSource.PlayOneShot(audioTwoPlayer, 0.5f); break;
                case 3: audioSource.PlayOneShot(audioThreePlayer, 0.5f); break;
                case 4: audioSource.PlayOneShot(audioFourPlayer, 0.5f); break;
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