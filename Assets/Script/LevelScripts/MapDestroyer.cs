using System.Collections;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    ObjectPooler objectPooler;
    public GameObject KistenPartsPrefab;
    public GameObject Item_Prefab;
    public LevelGenerator levelGenerator;
    public PlayerSpawner PlayerSpawner;
    private PlayerScript player;
    private int xPos;
    private int zPos;
    public AudioSource audioSource;
    public AudioClip audioClip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        objectPooler = ObjectPooler.Instance;
    }

    //Wird beim explodieren der Bombe durch das BombeScript aufgerufen.
    //Empfaengt die Position, die bombPower der Bombe und die ID des Spielers welche sie gelegt hat.
    public void explode(Vector3 position, int bombPower, int id)
    {
        xPos = (int)position.x;
        zPos = (int)position.z;
        player = PlayerSpawner.playerList[id].GetComponent<PlayerScript>();

        //Ueberprüft ob der Spieler, der die Bombe gelegt hat, exakt an der gleichen Stelle (in der Bombe) stehen geblieben ist.
        //Sollte dies zutreffen, wird der Spieler getoetet.
        if ((int)player.transform.position.x == xPos && (int)player.transform.position.z == zPos)
        {
             player.dead();
        }

        //Die explodierte Bombe wird dem Spieler wieder gutgeschrieben.
        player.setAvaibleBomb(1);

        //Explosions-Animation an der Stelle der Bombe wird abgespielt.
        objectPooler.SpawnFromPool("Explosion", new Vector3(xPos, 0.5f, zPos), Quaternion.identity);
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(KillField((int)position.x, (int)position.z));

        //Es werden 4 Coroutinen angelegt und gestartet, welche gleichzeitig in alle Himmelsrichtung (x, -x, z, -z) die Fehler durchlaufen.
        //Die bombPower gibt an wieviele Felder in jede Richtung erreicht und geprueft werden muessen.
        //Die Player ID der Bombe (= ID vom Player der sie gelegt hat) wird durchgereicht, falls diese spaeter noch benoetigt wird.
        StartCoroutine(CellWalker(bombPower, 0.06f, id, xPos, zPos, 0));
        StartCoroutine(CellWalker(bombPower, 0.06f, id, xPos, zPos, 1));
        StartCoroutine(CellWalker(bombPower, 0.06f, id, xPos, zPos, 2));
        StartCoroutine(CellWalker(bombPower, 0.06f, id, xPos, zPos, 3));
    }

    //Jeder der 4 Coroutinen laeuft solange der Rueckgabewert von ExplodeCell = true ist.
    private IEnumerator CellWalker(int bombPower, float waitTime, int id, int xPos, int zPos, int direction)
    {

        switch(direction)
        {
            case 0:
                int distanz = 1;
                while (distanz <= bombPower && ExplodeCell(xPos + distanz, zPos, id))
                {
                    yield return new WaitForSeconds(waitTime);
                    distanz++;
                }
                break;
                
            case 1:
                distanz = 1;
                while (distanz <= bombPower && ExplodeCell(xPos - distanz, zPos, id))
                {
                    yield return new WaitForSeconds(waitTime);
                    distanz++;
                }
                break;

            case 2:
                distanz = 1;
                while (distanz <= bombPower && ExplodeCell(xPos, zPos + distanz, id))
                {
                    yield return new WaitForSeconds(waitTime);
                    distanz++;
                }
                break;

            case 3:
                distanz = 1;
                while (distanz <= bombPower && ExplodeCell(xPos, zPos - distanz, id))
                {
                    yield return new WaitForSeconds(waitTime);
                    distanz++;
                }
                break;
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

                    //Spawnt Item
                    if(Random.value > 0.5f)
                        FindObjectOfType<ItemSpawner>().SpawnItem(x, z);

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
            }
        }
        
        yield return new WaitForSeconds(0.2f);

        if(levelGenerator.SecondaryGameObjects1[x, z] != null)
        {
            if(levelGenerator.SecondaryGameObjects1[x, z].gameObject.CompareTag("KillField"))
            {
                levelGenerator.SecondaryGameObjects1[x, z].gameObject.tag = "Boden";
            }
        }

        yield return null;
    }
}