using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    public int bombOwnerPlayerID;       // ID des Players der die Bombe gelegt hat
    public int bombPower;               // Staerker / Reichweite der Bombe
    public float bombTimer;             // Zeit bis die Bombe explodiert
    public bool remoteBomb;             // RemoteBombe wenn der Player das entsprechende Item eingesammelt hat
    public Color playerColor;           // Farbe des Players um RemoteBomben mit dieser zu versehen

    public float countDown;             // Überwacht die Vergangene Zeit bis zur Explosion
    private Vector3 bombPosition;       // Position an der die Bombe erzeugt wurde
    private float bombAngle;            // Zufaellige Y-Rotation beim Instanzieren der Bombe
    private int bombRotation;           // Steuert ob die Bombe sich recht- oder linkherum dreht
    
    public AudioSource audioSource;
    public AudioClip audioZischen;
    private CameraShake cameraShake;
    private MapDestroyer mapDestroyer;
    private LevelGenerator levelGenerator;
    private Color32 bombColor;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();
        mapDestroyer = FindObjectOfType<MapDestroyer>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        bombColor = GetComponent<Renderer>().material.color;
    }

    // Beim ersten Instanzieren aus dem ObjectPool erhält die Bombe eine zufaellige Ausrichtung und Rotationsrichtung
    void Start()
    {
        bombAngle = Random.Range(0, 36f) * 10f;
        bombRotation = Random.value >= 0.5f ? 1 : -1;
    }


    // bombAnimation() wird vom BombSpawner nachdem Instanzieren der Bombe aufgerufen um sie "zu starten"
    // Durch den ObjectPool werden die Bomben erneut vewendet und benoetigen bei der Wiederverwendung diesen "Reset"
    public IEnumerator bombAnimation()
    {
        FindObjectOfType<AudioManager>().playSound("woosh_2");
        audioSource.PlayOneShot(audioZischen, (0.9f * FindObjectOfType<AudioManager>().settingsFXVolume));

        bombPosition = transform.position;
        transform.eulerAngles += new Vector3(0, bombAngle, 0);

        if(remoteBomb) {
            GetComponent<Renderer>().material.color = playerColor;
        } else {
            GetComponent<Renderer>().material.color = bombColor;
        }

        // Bombe uebernimmt die Rotation der Bodenplatte auf der sie liegt. Faengt der Boden an zu wackeln, wird so diese Wackelbewegung uebernommen
        // Um die Wackelbewegung deutlicher zu mache, werden die Winkel mit dem Faktor 3 multipliziet
        Vector3 anglesBode;
        Vector3 anglesRotation = transform.localEulerAngles;

        countDown = bombTimer;
        // Bombe explodiert nach Ablauf des Timers (countDown) oder durch remoteBombe (Fernzuendung durch Player)
        while (countDown >= 0 || remoteBomb)
        {
            anglesBode = levelGenerator.SecondaryGameObjects1[(int)transform.position.x, (int)transform.position.z].gameObject.transform.localEulerAngles * 3f;
            anglesRotation += new Vector3(0, 80f * (Time.deltaTime * bombRotation), 0);

            transform.localEulerAngles = anglesBode + anglesRotation;
            countDown -= Time.deltaTime;
            yield return null;
        }
        FindObjectOfType<AudioManager>().stopSound("Bomb_zuendschnur");
        StopAllCoroutines();
        explode();
    }

    // Steuert die Auswirkung der Explosion
    void explode() {

        // Erschütterung der Kamera
        cameraShake.StartCoroutine(cameraShake.Shake(0.3f, 0.135f));

        // MapDestroyer wird aufgerufen um von der bombPosition und mit deren bombPower zu pruefen welche weiteren Felder um die Bombe herum explodieren muessen.
        mapDestroyer.explode(bombPosition, bombPower, bombOwnerPlayerID);

        // Bombe wird aus dem AllGameObject-Array "entfernt" damit Player wieder hindurchlaufen koennen
        levelGenerator.AllGameObjects[(int)bombPosition.x, (int)bombPosition.z] = null;

        // Bombe wird bis zur Wiederverwendung deaktiviert und zurueck in den ObjectPool gelegt
        gameObject.SetActive(false);
    }
}