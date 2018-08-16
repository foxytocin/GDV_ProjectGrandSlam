using System.Collections;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    public LevelGenerator levelGenerator;
    public PlayerSpawner PlayerSpawner;
    private IEnumerator coroutinexPositiv;
    private IEnumerator coroutinexNegativ;
    private IEnumerator coroutinezPositiv;
    private IEnumerator coroutinezNegativ;
    private Vector3 explosionPosition;
    public AudioSource audioSource;
    public AudioClip audioClip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        objectPooler = ObjectPooler.Instance;
    }

    //Wird beim explodieren der Bombe durch das BombeScript aufgerufen.
    //Empfaengt die Position, die bombPower der Bombe und die ID des Spielers welche sie gelegt hat.
    public IEnumerator explode(Vector3 position, int bombPower, int id)
    {
        explosionPosition = position;

        //Ueberprüft ob der Spieler, der die Bombe gelegt hat, exakt an der gleichen Stelle (in der Bombe) stehen geblieben ist.
        //Sollte dies zutreffen, wird der Spieler getoetet.
        if ((int) PlayerSpawner.playerList[id].gameObject.transform.position.x == (int)explosionPosition.x && (int) PlayerSpawner.playerList[id].gameObject.transform.position.z == (int)explosionPosition.z)
        {
             PlayerSpawner.playerList[id].GetComponent<PlayerScript>().dead();
        }

        //Die explodierte Bombe wird dem Spieler wieder gutgeschrieben.
        PlayerSpawner.playerList[id].GetComponent<PlayerScript>().setAvaibleBomb(1);

        //Explosions-Animation an der Stelle der Bombe wird abgespielt.
        objectPooler.SpawnFromPool("Explosion", new Vector3(explosionPosition.x, 0.5f, explosionPosition.z), Quaternion.identity);
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(KillField((int)position.x, (int)position.z));

        //Es werden 4 Coroutinen angelegt und gestartet, welche gleichzeitig in alle Himmelsrichtung (x, -x, z, -z) die Fehler durchlaufen.
        //Die bombPower gibt an wieviele Felder in jede Richtung erreicht und geprueft werden muessen.
        //Die Player ID der Bombe (= ID vom Player der sie gelegt hat) wird durchgereicht, falls diese spaeter noch benoetigt wird.
        coroutinexPositiv = xPositiv(bombPower, 0.06f, id);
        coroutinexNegativ = xNegativ(bombPower, 0.06f, id);
        coroutinezPositiv = zPositiv(bombPower, 0.06f, id);
        coroutinezNegativ = zNegativ(bombPower, 0.06f, id);

        StartCoroutine(coroutinexPositiv);
        StartCoroutine(coroutinexNegativ);
        StartCoroutine(coroutinezPositiv);
        StartCoroutine(coroutinezNegativ);

        yield return null;
    }

    //Jeder der 4 Coroutinen laeuft solange der Rueckgabewert von ExplodeCell = true ist.
    private IEnumerator xPositiv(int bombPower, float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell((int)explosionPosition.x + distanz, (int)explosionPosition.z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator xNegativ(int bombPower, float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell((int)explosionPosition.x - distanz, (int)explosionPosition.z, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zPositiv(int bombPower, float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell((int)explosionPosition.x, (int)explosionPosition.z + distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }

    private IEnumerator zNegativ(int bombPower, float waitTime, int id)
    {
        yield return new WaitForSeconds(waitTime);

        int distanz = 1;
        while (distanz <= bombPower && ExplodeCell((int)explosionPosition.x, (int)explosionPosition.z - distanz, id))
        {
            yield return new WaitForSeconds(waitTime);
            distanz++;
        }
    }


    //Prueft was fuer ein GameObject sich in der zu pruefenden Zelle befindet und "reagiert" entsprechend.
    //Waende und Kisten sorgen dafuer das der Rueckgabewert = false wird und die Explosion in diese Richtung nicht mehr fortgesetzt wird.
    //Bomben stoppen die aktuelle Explosion ebenfalls, werden aber direkt gezuendet in dem ihr Timer auf 0 und die remoteBomb funktion deaktiviert wird.
    //So werden Kettenreaktionen unter den Bomben erzeugt.
    bool ExplodeCell(int x, int z, int id)
    {
        if(x < 0) x = 0;
        if(z < 0) z = 0;

        GameObject thisGameObject = levelGenerator.AllGameObjects[x, z];

        //if == null bedeute an dieser Stelle ist ein GANG
        if (thisGameObject == null)
        {
            objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
            //levelGenerator.AllGameObjects[x, z] = Instantiate(KillFieldPrefab, new Vector3(x, 0.1f, z), Quaternion.Euler(90f, 0, 0), transform);
            StartCoroutine(KillField(x, z));
            return true;
        }
        else
        {
            //Switch-Case prueft den Tag des GameObjects. Performance ist so besser als mit dem Namen zu arbeiten, welcher ein Sting waere. 
            switch (thisGameObject.tag)
            {
                case "Bombe":
                    thisGameObject.GetComponent<BombScript>().remoteBomb = false;
                    thisGameObject.GetComponent<BombScript>().countDown = 0f;
                    return false;

                case "Wand":
                    return false;

                case "Kiste":
                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                    levelGenerator.AllGameObjects[x, z] = null;
                    thisGameObject.SetActive(false);
                    StartCoroutine(KillField(x, z));

                    //Ersetzt die Kiste durch Kiste_destroyed Prefab
                    //objectPooler.SpawnFromPool("Kiste_Destroyed", new Vector3(x, 0.5f, z), Quaternion.identity);
                    Instantiate(KistenPartsPrefab, new Vector3(x, 0.5f, z), Quaternion.identity, transform);
                    return false;

                case "FreeFall":
                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                    return true;

                case "Item":
                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                    levelGenerator.AllGameObjects[x, z] = null;
                    thisGameObject.SetActive(false);
                    StartCoroutine(KillField(x, z));
                    return true;

                case "Player":
                    objectPooler.SpawnFromPool("Explosion", new Vector3(x, 0.5f, z), Quaternion.identity);
                    thisGameObject.GetComponent<PlayerScript>().dead();
                    levelGenerator.AllGameObjects[x, z] = null;
                    StartCoroutine(KillField(x, z));
                    return true;

                default:
                    break;
            }
        }
        return false;
    }

        //private Color oldColor;
        //Markiert die Bodenplatte auf der eine Explosion stattfindet als "KillField" in dem der Tag von "Boden" auf "KillField" geaendert wird
        //Lauft der Player auf eine so markierte Bodenplatte stirbt er
        //Nach X Sekunden wird der Tag-Switch rueckgangig gemacht
        IEnumerator KillField(int x, int z)
        {

            if(levelGenerator.SecondaryGameObjects1[x, z] != null)
            {
                if(levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("Boden"))
                {
                    levelGenerator.SecondaryGameObjects1[x, z].gameObject.tag = "KillField";
                    //oldColor = levelGenerator.SecondaryGameObjects1[x, z].gameObject.GetComponent<Renderer>().material.color;
                    //levelGenerator.SecondaryGameObjects1[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            
            yield return new WaitForSeconds(0.2f);

            if(levelGenerator.SecondaryGameObjects1[x, z] != null)
            {
                if(levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("KillField"))
                {
                    levelGenerator.SecondaryGameObjects1[x, z].gameObject.tag = "Boden";
                    //levelGenerator.SecondaryGameObjects1[x, z].gameObject.GetComponent<Renderer>().material.color = oldColor;
                }
            }

            yield return null;
        }
}