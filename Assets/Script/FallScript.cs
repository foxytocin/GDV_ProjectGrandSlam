using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {
    float rotationY;
    float gravity;
    float randomDelay;
    float fallDelay;
    public GameObject FreeFallPrefab;
    public LevelGenerator LevelGenerator;

    private void Start()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        randomDelay = Random.Range(0.5f, 3f) / 10f;
        fallDelay = Random.Range(4f, 31f) / 10f;
        rotationY = Random.Range(-4f, 4f);
        gravity = 0;
    }

    IEnumerator falling()
    {
        while(fallDelay >= 0)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            fallDelay -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        LevelGenerator.AllGameObjects[(int)transform.position.x, (int)transform.position.z] = Instantiate(FreeFallPrefab, transform.position, Quaternion.identity);

        while(transform.position.y > -50f)
        {
            gravity += Time.deltaTime;
            transform.Translate(0, -((gravity * gravity) + randomDelay), 0);
            transform.localEulerAngles += new Vector3(0, rotationY * gravity, 0);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    public void fallDown() {
        StartCoroutine(falling());
    }
}