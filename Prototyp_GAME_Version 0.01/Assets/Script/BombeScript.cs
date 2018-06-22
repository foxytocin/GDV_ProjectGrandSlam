using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeScript : MonoBehaviour
{

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
            explode();
        }
    }

    public void explode()
    {
        Destroy(gameObject);
        Debug.Log("Bombe vom Player " + bombOwnerPlayerID + " explodiert");
    }
}