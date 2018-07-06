using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int bombOwnerPlayerID;
    public int bombPower;
    public int bombTimer;
    public bool remoteBomb;
    private float startTime;
    public xzPosition bombPosition;

    //Zeitpunkt an dem die Bombe erzeugt wurde
    void Awake()
    {
        startTime = Time.time;
        bombPosition = new xzPosition((int)transform.position.x, (int)transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 70f * Time.deltaTime, 0);

        if ((bombTimer < (Time.time - startTime)) && !remoteBomb)
        {
            FindObjectOfType<MapDestroyer>().Explode(bombPosition, bombPower, bombOwnerPlayerID);
            Destroy(gameObject);
        }
    }
}