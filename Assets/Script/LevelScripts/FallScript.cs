using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {
    float rotationY;
    float gravity;
    float randomSpeed;
    float fallDelay;
    private LevelGenerator LevelGenerator;

    private void Awake()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void fallDown() {

        if(GetComponent<Renderer>().isVisible)
        {
            StartCoroutine(fallingVisible());
        } else {

            fallingInvisible();
        }
    }

    private void fallingInvisible()
    {
        int xPos = (int)transform.position.x;
        int zPos = (int)transform.position.z;

        if(LevelGenerator.AllGameObjects[xPos, zPos] != null) {

            GameObject currentGameObject = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

            switch (currentGameObject.tag)
            {
                case "Bombe":
                    currentGameObject.GetComponent<BombScript>().remoteBomb = false;
                    currentGameObject.GetComponent<BombScript>().countDown = 0f;
                    break;

                case "Player":
                    currentGameObject.GetComponent<PlayerScript>().playerFall();
                    break;
                default:
                    break;
            }
        }
        
        LevelGenerator.AllGameObjects[xPos, zPos] = ObjectPooler.Instance.SpawnFromPool("FreeFall", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }


    private IEnumerator fallingVisible()
    {
        randomSpeed = Random.Range(0.3f, 2f) / 10f;
        fallDelay = Random.Range(10f, 41f) / 10f;
        rotationY = Random.Range(-3f, 3f);
        gravity = 0;
        int xPos = (int)transform.position.x;
        int zPos = (int)transform.position.z;

        while(fallDelay >= 0)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            fallDelay -= Time.deltaTime;
            yield return null;
        }

        if(LevelGenerator.AllGameObjects[xPos, zPos] != null) {

            GameObject currentGameObject = LevelGenerator.AllGameObjects[xPos, zPos].gameObject;

            switch (currentGameObject.tag)
            {
                case "Bombe":
                    currentGameObject.GetComponent<BombScript>().remoteBomb = false;
                    currentGameObject.GetComponent<BombScript>().countDown = 0f;
                    break;

                case "Player":
                    currentGameObject.GetComponent<PlayerScript>().playerFall();
                    break;
                default:
                    break;
            }
        }
        
        LevelGenerator.AllGameObjects[xPos, zPos] = ObjectPooler.Instance.SpawnFromPool("FreeFall", transform.position, Quaternion.identity);

        while(transform.position.y > -50f)
        {
            gravity += Time.deltaTime * 0.9f;
            transform.Translate(0, -((gravity * gravity) + randomSpeed), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return null;
        }

        gameObject.SetActive(false);
        StopAllCoroutines();
    }


    public IEnumerator fallingLevelCleanup()
    {
        randomSpeed = Random.Range(0.3f, 3f) / 10f;
        fallDelay = Random.value;
        rotationY = Random.Range(-3f, 3f);
        gravity = 0;
        int xPos = (int)transform.position.x;
        int zPos = (int)transform.position.z;

        while(fallDelay >= 0)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            fallDelay -= Time.deltaTime;
            yield return null;
        }
        
        while(transform.position.y > -50f)
        {
            gravity += Time.deltaTime * 0.9f;
            transform.Translate(0, -((gravity * gravity) + randomSpeed), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return null;
        }

        gameObject.SetActive(false);
        StopAllCoroutines();
    }

}