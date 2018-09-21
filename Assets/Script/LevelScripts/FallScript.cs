using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour
{
    float rotationY;
    float gravity;
    float randomSpeed;
    float fallDelay;
    private LevelGenerator LevelGenerator;
    private GameManager GameManager;

    private void Awake()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        GameManager = FindObjectOfType<GameManager>();
    }

    // Entscheidet ob das GameObject sichtbar ist und damit animiert in den Abgrund faellt
    // oder ob die Animationsberechnung gesparrt werden kann
    public void fallDown()
    {

        if (GetComponent<Renderer>().isVisible)
        {
            StartCoroutine(fallingVisible());
        }
        else
        {

            fallingInvisible();
        }
    }

    // Nicht sichtbares deaktiveren der Levelelemente (Boden, Kisten, Waende, GlowBalls, DistanceLines, MeterSchilder, Items)
    private void fallingInvisible()
    {
        int xPos = (int)transform.position.x;
        int zPos = (int)transform.position.z;

        if (LevelGenerator.AllGameObjects[xPos, zPos] != null && GameManager.gameStatePlay)
        {

            GameObject currentGameObject = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

            // Bombe, Player und Enemies werden gesondert behandelt
            // Bomben expodieren wenn der Boden unter ihnen faellt
            // Player und Enemies haben eine eigne Fall-Funktion
            switch (currentGameObject.tag)
            {
                case "Bombe":
                    currentGameObject.GetComponent<BombScript>().remoteBomb = false;
                    currentGameObject.GetComponent<BombScript>().countDown = 0f;
                    break;

                case "Player":
                    PlayerScript pc = currentGameObject.GetComponent<PlayerScript>();
                    if (pc != null)
                        pc.playerFall();
                    break;

                case "Enemy":
                    EnemyScript ec = currentGameObject.GetComponent<EnemyScript>();
                    if (ec != null)
                        ec.playerFall();
                    break;

                default:
                    break;
            }
        }

        if (GameManager.gameStatePlay)
        {
            LevelGenerator.AllGameObjects[xPos, zPos] = ObjectPooler.Instance.SpawnFromPool("FreeFall", transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

    }

    // Animiertes Fallen der Levelelemente (Boden, Kisten, Waende, GlowBalls, DistanceLines, MeterSchilder, Items)
    private IEnumerator fallingVisible()
    {
        randomSpeed = Random.Range(0.3f, 2f) / 10f;
        fallDelay = Random.Range(10f, 41f) / 10f;
        rotationY = Random.Range(-3f, 3f);
        gravity = 0;
        int xPos = (int)transform.position.x;
        int zPos = (int)transform.position.z;

        while (fallDelay >= 0)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            fallDelay -= Time.deltaTime;
            yield return null;
        }

        if (LevelGenerator.AllGameObjects[xPos, zPos] != null && GameManager.gameStatePlay)
        {

            GameObject currentGameObject = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

            // Bombe, Player und Enemies werden gesondert behandelt
            // Bomben expodieren wenn der Boden unter ihnen faellt
            // Player und Enemies haben eine eigne Fall-Funktion
            switch (currentGameObject.tag)
            {
                case "Bombe":
                    currentGameObject.GetComponent<BombScript>().remoteBomb = false;
                    currentGameObject.GetComponent<BombScript>().countDown = 0f;
                    break;

                case "Player":
                    PlayerScript pc = currentGameObject.GetComponent<PlayerScript>();
                    if (pc != null)
                        pc.playerFall();
                    break;

                case "Enemy":
                    EnemyScript ec = currentGameObject.GetComponent<EnemyScript>();
                    if (ec != null)
                        ec.playerFall();
                    break;

                default:
                    break;
            }
        }

        if (GameManager.gameStatePlay)
        {
            LevelGenerator.AllGameObjects[xPos, zPos] = ObjectPooler.Instance.SpawnFromPool("FreeFall", transform.position, Quaternion.identity);
        }

        while (gameObject.activeSelf && transform.position.y > -50f)
        {
            gravity += Time.deltaTime * 0.9f;
            transform.Translate(0, -((gravity * gravity) + randomSpeed), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return null;
        }

        gameObject.SetActive(false);
    }


    // Wird beim Level-Restart aufgerufen und laesst alle GameObjecte sofort (ohne vorheriges Wackeln) fallen
    // Kurze zufaellige Verzoegerung damit es besser aussieht
    public IEnumerator fallingLevelCleanup()
    {
        randomSpeed = Random.Range(0.3f, 3f) / 10f;
        fallDelay = Random.value;
        rotationY = Random.Range(-3f, 3f);
        gravity = 0;

        yield return new WaitForSeconds(fallDelay);

        while (gameObject.activeSelf && transform.position.y > -50f)
        {
            gravity += Time.deltaTime * 0.9f;
            transform.Translate(0, -((gravity * gravity) + randomSpeed), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return null;
        }
        gameObject.SetActive(false);
    }

}