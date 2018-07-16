using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    public int bombOwnerPlayerID;
    public int bombPower;
    public float bombTimer;
    public bool remoteBomb;
    public Color playerColor;

    public float countDown;
    private Vector3 bombPosition;
    private float bombAngle;
    private int bombRotation;
    bool exploded = false;

    void Start()
    {
        countDown = bombTimer;
        bombPosition = transform.position;
        transform.eulerAngles += new Vector3(0, bombAngle, 0);
        bombAngle = Random.Range(0, 36f) * 10f;
        bombRotation = Random.value >= 0.5f ? 1 : -1;

        if(remoteBomb) {
            GetComponent<Renderer>().material.color = playerColor;
        }
    }

    //Update is called once per frame
    void Update()
    {
        //Bombe dreht sich um die eigene y-Achse.
        transform.eulerAngles += new Vector3(0, 70f * (Time.deltaTime * bombRotation), 0);

        //coubtDown wird runtergezaehlt
        countDown -= Time.deltaTime;

        //Wenn der bombTimer erreicht wird und es keine remoteBomb ist, wird sie gezuendet.
        //Wenn es eine remoteBombe ist, bleibt diese aktiv bis der Player sie selber zündet.
        if (countDown <= 0 && !remoteBomb && !exploded)
        {
            exploded = true;
            explode();
        }
    }

    void explode() {

        //Explode() im MapDestroyer wird aufgerufen um von der bombPosition und mit deren bombPower zu pruefen welche weiteren Felder um die Bombe herum explodieren muessen.
        MapDestroyer mapDestroyer = FindObjectOfType<MapDestroyer>();
        mapDestroyer.StartCoroutine(mapDestroyer.explode(bombPosition, bombPower, bombOwnerPlayerID));

        //Bombe selber wird zerstört.
        Destroy(gameObject);
    }
}