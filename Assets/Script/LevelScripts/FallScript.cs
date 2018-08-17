using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {
    float rotationY;
    float gravity;
    float randomDelay;
    float fallDelay;
    public GameObject FreeFallPrefab;
    public LevelGenerator LevelGenerator;

    private void Awake(){
        LevelGenerator = FindObjectOfType<LevelGenerator>();
    }

    IEnumerator falling()
    {
        randomDelay = Random.Range(0.3f, 2f) / 10f;
        fallDelay = Random.Range(10f, 41f) / 10f;
        rotationY = Random.Range(-3f, 3f);
        gravity = 0;

        while(fallDelay >= 0)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            fallDelay -= Time.deltaTime;
            yield return null;
        }

        if(LevelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] != null) {

            GameObject currentGameObject = LevelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z].gameObject;

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
        
        LevelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = Instantiate(FreeFallPrefab, transform.position, Quaternion.identity);

        while(transform.position.y > -50f)
        {
            gravity += Time.deltaTime * 0.9f;
            transform.Translate(0, -((gravity * gravity) + randomDelay), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return null;
        }

        gameObject.SetActive(false);
        
        StopAllCoroutines();
    }

    public void fallDown() {
        StartCoroutine(falling());
    }

}