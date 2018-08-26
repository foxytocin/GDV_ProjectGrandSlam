using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private void Start()
    {
        transform.Rotate(-90, 0, 0);
    }
    // Update is called once per frame

    public IEnumerator animationGhost(Color32 ghostColor, Vector3 spawnPosition)
    {
        GetComponent<Renderer>().material.color = ghostColor;
        GetComponent<Light>().enabled = true;

        bool terminus = true;

        float myTime = 0f;

        while (terminus)
        {
            myTime += Time.deltaTime*0.3f;
            Debug.Log(myTime);
            if (myTime > 6f)
            {

                Debug.Log("animation");
                if (ghostColor.a >= 0)
                    ghostColor.a -= 4;
                else
                    GetComponent<Light>().enabled = false;

                transform.Rotate(0, 0, 2);
                transform.Translate(0, 0, 0.2f);
                GetComponent<Renderer>().material.color = ghostColor;

                myTime = 0.0f;
            }
        }
        this.gameObject.SetActive(false);
        yield return null;
    }
}
