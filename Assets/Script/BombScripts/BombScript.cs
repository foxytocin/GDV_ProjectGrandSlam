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
    private LevelGenerator levelGenerator;
    Color32 bombColor;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cameraShake = FindObjectOfType<CameraShake>();
        mapDestroyer = FindObjectOfType<MapDestroyer>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        bombColor = GetComponent<Renderer>().material.color;
    }

    void Start()
    {
        bombAngle = Random.Range(0, 36f) * 10f;
        bombRotation = Random.value >= 0.5f ? 1 : -1;
    }

    public IEnumerator bombAnimation()
    {
        audioSource.PlayOneShot(audioPlopp, 1f);
        audioSource.PlayOneShot(audioZischen, 0.9f);

        bombPosition = transform.position;
        transform.eulerAngles += new Vector3(0, bombAngle, 0);

        if(remoteBomb) {
            GetComponent<Renderer>().material.color = playerColor;
        } else {
            GetComponent<Renderer>().material.color = bombColor;
        }

        //Bombe uebernimmt die Rotation der Bodenplatte auf der sie liegt. Faengt der Boden an zu wackeln, wird so diese Wackelbewegung uebernommen
        //Um die Wackelbewegung deutlicher zu mache, werden die Winkel mit dem Faktor 3 multipliziet
        Vector3 anglesBode;
        Vector3 anglesRotation = transform.localEulerAngles;

        countDown = bombTimer;

        while (countDown >= 0 || remoteBomb)
        {
            anglesBode = levelGenerator.SecondaryGameObjects1[(int)transform.position.x, (int)transform.position.z].gameObject.transform.localEulerAngles * 3f;
            anglesRotation += new Vector3(0, 80f * (Time.deltaTime * bombRotation), 0);

            transform.localEulerAngles = anglesBode + anglesRotation;
            countDown -= Time.deltaTime;
            yield return null;
        }

        StopAllCoroutines();
        explode();
    }

    void explode() {

        cameraShake.StartCoroutine(cameraShake.Shake(0.3f, 0.135f));

        //Explode() im MapDestroyer wird aufgerufen um von der bombPosition und mit deren bombPower zu pruefen welche weiteren Felder um die Bombe herum explodieren muessen.
        mapDestroyer.explode(bombPosition, bombPower, bombOwnerPlayerID);

        levelGenerator.AllGameObjects[(int)bombPosition.x, (int)bombPosition.z] = null;

        gameObject.SetActive(false);
        //Bombe selber wird zerstört.
        //Destroy(gameObject);
    }
}