using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeScript : MonoBehaviour
{
    public WorldScript World;

    public int bombOwnerPlayerID;
    public bool remoteBomb = false;
    public int bombPower = 1;
    public int bombTimer = 3;
    private float startTime;

    //Zeitpunkt an dem die Bombe erzeugt wurde
    void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 70f * Time.deltaTime, 0);

        if ((bombTimer < (Time.time - startTime)) && !remoteBomb)
        {
            //GEHT NICHT: Zerstörung wird an den MapDestroyer übergeben.
            FindObjectOfType<MapDestroyer>().Explode((int)transform.position.x,(int)transform.position.z);

            //FUNKTIONIER: Bombe zerstört sich selber
            //Destroy(gameObject);
        }
    }

}