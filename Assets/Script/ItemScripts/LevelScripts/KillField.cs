using UnityEngine;

public class KillField : MonoBehaviour
{
    private float killTime = 0.3f;

    //Update is called once per frame
    void Update()
    {
        killTime -= Time.deltaTime;

        //Wenn der bombTimer erreicht wird und es keine remoteBomb ist, wird sie gezuendet.
        //Wenn es eine remoteBombe ist, bleibt diese aktiv bis der Player sie selber zündet.
        if (killTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}