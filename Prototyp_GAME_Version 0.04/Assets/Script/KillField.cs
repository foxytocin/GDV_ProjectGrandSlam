using UnityEngine;

public class KillField : MonoBehaviour
{
    private float startTime;
    private float killTime = 1f;
    private Vector3 killFieldPosition;

    void Awake()
    {
        //Zeitpunkt an dem die Bombe erzeugt wurde
        startTime = Time.time;
        killFieldPosition = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Wenn der bombTimer erreicht wird und es keine remoteBomb ist, wird sie gezuendet.
        //Wenn es eine remoteBombe ist, bleibt diese aktiv bis der Player sie selber zündet.
        if (killTime < (Time.time - startTime))
        {
            Destroy(gameObject);
        }
    }
}