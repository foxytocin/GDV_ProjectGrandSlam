using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int ItemID;
    private PlayerSpawner playerSpawner;
    private PlayerScript player;
    public float timeLeft;

    void Awake()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }
    void Start()
    {
        // Erzeugt beim Instanzieren einen zufälligen Item-Typ
        Itemwahl();
    }


    //Item wird gewürfelt
    public void Itemwahl()
    {
        int RandomValue = (int)(Random.Range(0, 3f));
        switch (RandomValue)
        {
            case 0:
                ItemID = 0;
                MakeBombPowerItem(ItemID);
                break;

            case 1:
                ItemID = 1;
                MakeHoudiniItem(ItemID);
                break;

            case 2:
                ItemID = 2;
                MakeRemoteBombItem(ItemID);
                break;
        }
    }

    //Schreibt Fähigkeit gewünschtem Player zu
    public void GrabItem(int id)
    {
        //Deaktiviert Mesh und Licht des Items
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Light>().enabled = false;

        // Findet den Player in der PlayerList anhand seiner ID und holt dessen PlayerScript damit es verändert werden kann.
        player = playerSpawner.playerList[id].GetComponent<PlayerScript>();

        switch (ItemID)
        {
            case 0:
                BombPower(ItemID);
                break;

            case 1:
                Houdini(ItemID);
                break;

            case 2:
                RemoteBomb(ItemID);
                break;
        }
    }
    //ItemLook
    public void MakeBombPowerItem(int id)
    {
        GetComponent<Renderer>().material.color = Color.red;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        GetComponent<Light>().color = Color.red;


    }
    public void MakeHoudiniItem(int id)
    {
        GetComponent<Renderer>().material.color = Color.green;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        GetComponent<Light>().color = Color.green;

    }
    public void MakeRemoteBombItem(int id)
    {
        GetComponent<Renderer>().material.color = Color.blue;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
        GetComponent<Light>().color = Color.blue;
    }



    //ItemFunktionen
    public void BombPower(int id)
    {

        player.bombPower += 1;
        Destroy(gameObject);
    }


    public void Houdini(int id)
    {

        player.houdiniTimer += 10.0f;
        player.remoteBombItem = false;
        Destroy(gameObject);

    }


    public void RemoteBomb(int id)
    {
        player.remoteBombTimer += 10.0f;
        player.houdiniItem = false;
        Destroy(gameObject);

    }

























    void Update()
    {
        //Drehung des Items
        transform.eulerAngles += new Vector3(0f, 80f * Time.deltaTime, 0f);


    }
}

