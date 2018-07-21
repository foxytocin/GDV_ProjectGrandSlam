using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {
    float rotationY;
    float gravity;
    float randomDelay;
    float fallDelay;
    bool falling = false;
    bool start = false;

    private void Start()
    {
        randomDelay = Random.Range(0.5f, 3f) / 10f;
        fallDelay = Random.Range(4f, 31f) / 10f;
        rotationY = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update () {

        if (start)
        {
            transform.localEulerAngles += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        
            fallDelay -= Time.deltaTime;
            if (fallDelay <= 0)
            {
                start = false;
                falling = true;
            }

        }

        if(falling) {
            gravity += Time.deltaTime;
            transform.Translate(0, -((gravity * gravity) + randomDelay), 0);
            transform.localEulerAngles += new Vector3(0, rotationY + gravity, 0);
        }

        if(transform.position.y < - 50f) {
            Destroy(gameObject);
        }
	}

    public void fallDown() {
        start = true;
    }
}