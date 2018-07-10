using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int bombOwnerPlayerID;
    public int bombPower;
    public int bombTimer;
    public bool remoteBomb;
    private float startTime;
    private Vector3 bombPosition;
    public BombSpawner bombSpawner;

    void Awake()
    {
        //Zeitpunkt an dem die Bombe erzeugt wurde
        startTime = Time.time;
        bombPosition = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Bombe dreht sich um die eigene y-Achse.
        transform.eulerAngles += new Vector3(0, 70f * Time.deltaTime, 0);

        //Wenn der bombTimer erreicht wird und es keine remoteBomb ist, wird sie gezuendet.
        //Wenn es eine remoteBombe ist, bleibt diese aktiv bis der Player sie selber zündet.
        if ((bombTimer < (Time.time - startTime)) && !remoteBomb)
        {
            //Explode() im MapDestroyer wird aufgerufen um von der bombPosition und mit deren bombPower zu pruefen welche weiteren Felder um die Bombe herum explodieren muessen.
            FindObjectOfType<MapDestroyer>().Explode(bombPosition, bombPower, bombOwnerPlayerID);
          
            //Bombe selber wird zerstört.
            Destroy(gameObject);
        }
    }
}