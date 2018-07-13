using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {

    float gravity;
    float randomDelay;
    float fallDelay;
    bool falling = false;
    bool start = false;

    private void Start()
    {
        randomDelay = Random.Range(0.4f, 3f) / 10f;
        fallDelay = Random.Range(4f, 31f) / 10f;
    }

    // Update is called once per frame
    void Update () {

        if (start)
        {
            fallDelay -= Time.deltaTime;
            if (fallDelay <= 0)
                falling = true;
        }

        if(falling) {
            gravity += Time.deltaTime;
            transform.Translate(0, -((gravity * gravity * Time.deltaTime) + randomDelay), 0);
        }

        if(transform.position.y < - 30f) {
            Destroy(gameObject);
        }
	}

    public void fallDown() {
        start = true;
    }
}