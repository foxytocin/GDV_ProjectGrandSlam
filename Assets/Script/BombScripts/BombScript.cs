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
    
    public AudioSource audioSource;
    public AudioClip audioZischen;
    public AudioClip audioPlopp;
    private CameraShake cameraShake;
    private MapDestroyer mapDestroyer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();
        mapDestroyer = FindObjectOfType<MapDestroyer>();
    }

    void Start()
    {
        audioSource.PlayOneShot(audioPlopp, 1f);
        audioSource.PlayOneShot(audioZischen, 0.9f);

        countDown = bombTimer;
        bombPosition = transform.position;
        transform.eulerAngles += new Vector3(0, bombAngle, 0);
        bombAngle = Random.Range(0, 36f) * 10f;
        bombRotation = Random.value >= 0.5f ? 1 : -1;

        if(remoteBomb) {
            GetComponent<Renderer>().material.color = playerColor;
        }
        
        StartCoroutine(bombAnimation());
    }

    IEnumerator bombAnimation()
    {
        while (countDown >= 0 || remoteBomb)
        {
            transform.eulerAngles += new Vector3(0, 80f * (Time.deltaTime * bombRotation), 0);
            countDown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

         explode();
    }

    void explode() {

        cameraShake.StartCoroutine(cameraShake.Shake(0.15f, 0.15f));

        //Explode() im MapDestroyer wird aufgerufen um von der bombPosition und mit deren bombPower zu pruefen welche weiteren Felder um die Bombe herum explodieren muessen.
        mapDestroyer.StartCoroutine(mapDestroyer.explode(bombPosition, bombPower, bombOwnerPlayerID));

        //Bombe selber wird zerstört.
        Destroy(gameObject);
    }
}