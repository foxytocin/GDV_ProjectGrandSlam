using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private void Start()
    {
        transform.Rotate(0, 0, 0);
    }
    // Update is called once per frame
   
    public void startsAnimations(Color32 playerColor)
    {
        StartCoroutine(animationGhost(playerColor));
    }
    
    public IEnumerator animationGhost(Color32 ghostColor)
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        transform.Rotate(0, Random.value * 180f, 0f);
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

            transform.localScale += new Vector3(0.3f * Time.deltaTime, 0.3f * Time.deltaTime, 0.3f * Time.deltaTime);
            transform.Translate(0, 0.3f * (Time.deltaTime + 0.3f), 0);
            yield return null;
        }

        ghostColor.a = 1;
        GetComponent<Light>().color = ghostColor;
        
        this.gameObject.SetActive(false);
        
    }

}
