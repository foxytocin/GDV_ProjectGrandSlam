using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public void startsAnimations(Color32 playerColor)
    {
        StartCoroutine(animationGhost(playerColor));
    }
    
    public IEnumerator animationGhost(Color32 ghostColor)
    {   
        ghostColor.a = 200;
        transform.eulerAngles = new Vector3(-90, 0, Random.value * 180f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GetComponent<Renderer>().material.color = ghostColor;
        GetComponent<Light>().color = ghostColor;
        GetComponent<Light>().enabled = true;
		GetComponent<Renderer>().material.SetColor("_EmissionColor", (Color)ghostColor * Mathf.LinearToGammaSpace (0.7f));
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

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
            transform.Translate(0, 0, 0.3f * (Time.deltaTime + 0.3f));
            yield return null;
        }
        
        gameObject.SetActive(false);
        
    }

}
