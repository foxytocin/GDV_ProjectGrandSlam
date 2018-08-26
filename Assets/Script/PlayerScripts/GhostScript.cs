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
   
    public void startsAnimations(Color32 playerColor)
    {
        StartCoroutine(animationGhost(playerColor));
    }
    
    public IEnumerator animationGhost(Color32 ghostColor)
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.Rotate(0, (Random.value*3)*60, 0);
        GetComponent<Renderer>().material.color = ghostColor;
        GetComponent<Light>().enabled = true;

        bool terminus = true;

        float myTime = 0f;

        while (terminus)
        {
            myTime += Time.deltaTime + 0.3f;

            if (myTime > 0.5f)
            {

                if (ghostColor.a > 4)
                {
                    ghostColor.a -= 4;
                }
                else
                {
                    GetComponent<Light>().enabled = false;
                    terminus = false;
                }

                GetComponent<Renderer>().material.color = ghostColor;
                myTime = 0.0f;
            }

            transform.localScale += new Vector3(0.1f * (Time.deltaTime + 0.3f), 0.1f * (Time.deltaTime + 0.3f), 0.1f * (Time.deltaTime + 0.3f));
            transform.Translate(0, 0, 0.2f * (Time.deltaTime + 0.3f));
            yield return null;
        }
        this.gameObject.SetActive(false);
        
    }

}
