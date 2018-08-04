using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class LevelGenerator : MonoBehaviour
{
    public GameObject BodenPrefab;
    public GameObject WandPrefab;
    public GameObject KistePrefab;

    private float KistenMenge;

    public float LevelSpeed;
    public TextAsset LevelTextdatei0;
    public TextAsset LevelTextdatei1;
    public TextAsset LevelTextdatei2;
    public TextAsset LevelTextdatei3;
    public TextAsset LevelTextdatei4;
    public TextAsset LevelTextdatei5;
    public TextAsset LevelTextdatei6;
    public TextAsset LevelTextdatei7;
    public TextAsset LevelTextdatei8;

    private const string levelGang = "o";
    private const string levelWand = "x";
    private const string levelKiste = "k";

    public GameObject[,] AllGameObjects;
    public GameObject[,] SecondaryGameObjects1;
    public GameObject[,] SecondaryGameObjects2;
    public GameObject[,] SecondaryGameObjects3;
    public GameObject[,] DistanceLines;

    public string[][] levelSectionData;
    private string[][] levelBase;
    private int SectionDataOffset;
    private int rotation;
    private bool specialSection;
    private bool GenerateKisten;
    public const int tiefeLevelStartBasis = 25;
    public GenerateDistanceLine GenerateDistanceLine;

    // Use this for initialization
    void Awake()
    {
        GenerateKisten = true;
        LevelSpeed = 0.5f;
        KistenMenge = 10f;
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        AllGameObjects = new GameObject[33, 2000];
        SecondaryGameObjects1 = new GameObject[33, 2000];
        SecondaryGameObjects2 = new GameObject[33, 2000];
        SecondaryGameObjects3 = new GameObject[33, 2000];
        DistanceLines = new GameObject[4, 3000];
        levelSectionData = readFile(LevelTextdatei0);
        createStartBasis(tiefeLevelStartBasis);
    }

    private void Update()
    {
        if(Input.GetKeyDown("5")) {
            GenerateKisten = true;
            KistenMenge = 20f;
        }
        if (Input.GetKeyDown("6"))
        {
            GenerateKisten = true;
            KistenMenge = 40f;
        }
        if (Input.GetKeyDown("7"))
        {
            GenerateKisten = true;
            KistenMenge = 60f;
        }
        if (Input.GetKeyDown("8"))
        {
            GenerateKisten = true;
            KistenMenge = 80f;
        }
        if (Input.GetKeyDown("9"))
        {
            GenerateKisten = true;
            KistenMenge = 100f;
        }
        if (Input.GetKeyDown("0"))
        {
            GenerateKisten = false;
        }
    }


    public void createStartBasis(int tiefe)
    {
        for (int i = 0; i < tiefe; i++) {
            createWorld(i);
        }
    }

    public void createWorld(int CameraPosition)
    {
        setDifficulty(CameraPosition);

        if(CameraPosition - SectionDataOffset < levelSectionData.Length) {
            drawLevelLine(CameraPosition);

        } else {
            SectionDataOffset = CameraPosition;

            int RandomValue = (int)(Random.Range(0, 11f));

            if(specialSection) {

                switch (RandomValue)
                {
                    case 0:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        break;
                    case 1:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        break;
                    case 2:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        break;
                    case 3:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        break;
                    case 4:
                        levelSectionData = readFile(LevelTextdatei2);
                        specialSection = false;
                        break;
                    case 5:
                        levelSectionData = readFile(LevelTextdatei3);
                        specialSection = false;
                        break;
                    case 6:
                        levelSectionData = readFile(LevelTextdatei4);
                        specialSection = false;
                        break;
                    case 7:
                        levelSectionData = readFile(LevelTextdatei5);
                        specialSection = false;
                        break;
                    case 8:
                        levelSectionData = readFile(LevelTextdatei6);
                        specialSection = false;
                        break;
                    case 9:
                        levelSectionData = readFile(LevelTextdatei7);
                        specialSection = false;
                        break;
                    case 10:
                        levelSectionData = readFile(LevelTextdatei8);
                        specialSection = false;
                        break;
                    default:
                        Debug.Log("Switch-ERROR in createWorld()");
                        break;
                }

            } else {
                levelSectionData = readFile(LevelTextdatei1);
                specialSection = true;
            }
            drawLevelLine(CameraPosition);
        }
        StartCoroutine(cleanLine((CameraPosition - (10 + tiefeLevelStartBasis))));
    }

    //Abhängig von der CameraPosition wird die Menge der Kisten verändert
    //Je weiter der Spieler im Level, desto mehr Kisten werden generiert
    // Kisten Menge wird auf einer RandomValue 0 - 20 % KistenMenge errechnet.
    void setDifficulty(int row)
    {
        switch(row)
        {
            case 50:
                KistenMenge = 10f; //10% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 100:
                KistenMenge = 20f; //20% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 150:
                KistenMenge = 30f; //30% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 200:
                KistenMenge = 40f; //40% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 250:
                KistenMenge = 50f; //50% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 300:
                KistenMenge = 60f; //60% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 350:
                KistenMenge = 70f; //70% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 400:
                KistenMenge = 80f; //80% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 450:
                KistenMenge = 90f; //90% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 500:
                KistenMenge = 100f; //100% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            default:
                break;
        }
    }


    IEnumerator cleanLine(int CameraPosition) {
        
        if(CameraPosition >= 0) {

            for (int i = 0; i < levelSectionData[0].Length - 1; i++)
            {
                GameObject thisGameObject = AllGameObjects[i, CameraPosition];

                if (thisGameObject != null)
                {
                    switch (thisGameObject.tag)
                    {
                        case "Player":
                            break;

                        case "Bombe":
                            break;

                        default:
                            FallScript fc = thisGameObject.GetComponent<FallScript>();
                            if (fc != false)
                                fc.fallDown();
                            break;
                    }
                }

                if (SecondaryGameObjects1[i, CameraPosition] != null)
                {
                    FallScript fc = SecondaryGameObjects1[i, CameraPosition].gameObject.GetComponent<FallScript>();
                    if (fc != false)
                        fc.fallDown();
                }

                if (SecondaryGameObjects2[i, CameraPosition] != null) {
                    FallScript fc = SecondaryGameObjects2[i, CameraPosition].gameObject.GetComponent<FallScript>();
                    if (fc != false)
                        fc.fallDown();
                }

                if (SecondaryGameObjects3[i, CameraPosition] != null) {
                    FallScript fc = SecondaryGameObjects3[i, CameraPosition].gameObject.GetComponent<FallScript>();
                    if (fc != false)
                        fc.fallDown();
                }
            }

            //Loescht die DistanceLine aus der Spielwelt wenn diese 10 Felder hinter der Camnera ist
            if (CameraPosition > 10 && DistanceLines[0, CameraPosition - 10] != null) {
                Destroy(DistanceLines[0, CameraPosition - 10].gameObject);
                Destroy(DistanceLines[1, CameraPosition - 10].gameObject);
                Destroy(DistanceLines[2, CameraPosition - 10].gameObject);
                Destroy(DistanceLines[3, CameraPosition - 10].gameObject);
            }
        }

        yield return null;
    }


    //Zeichnet die Linien der LevelSectionData zeilenweise. Abhängig vom Symbol der aktuellen Stelle (Gang, Wand, Kiste)
    void drawLevelLine(int CameraPosition) {
        
        for (int i = 0; i < levelSectionData[0].Length - 1; i++)
        {
            switch (levelSectionData[CameraPosition - SectionDataOffset][i])
            {
                case levelGang:
                    createGang(new Vector3(i, 0f, CameraPosition), CameraPosition);
                    break;
                case levelWand:
                    createWand(new Vector3(i, 0.5f, CameraPosition));
                    break;
                case levelKiste:
                    createKiste(new Vector3(i, 0f, CameraPosition));
                    break;
            }
        }

        //Generiert alle X Meter die DistanceLine
        if(CameraPosition > 10 && CameraPosition % 20 == 0 )
            {
                GenerateDistanceLine.createDistanceLine(CameraPosition);
            }
    }


    //Erzeugt eine Bodenplatte und zufällig eine Kiste
    void createGang(Vector3 pos, int CameraPosition) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(BodenPrefab, pos - new Vector3(0, 0.1f, 0), Quaternion.identity, transform);

        if((Random.value <= (KistenMenge / 100f)) && CameraPosition > 11 && GenerateKisten) {
            GameObject Kiste = Instantiate(KistePrefab, pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0), transform);
            Kiste.tag = "Kiste";
            rotation += 90;

            AllGameObjects[(int)pos.x, (int)pos.z] = Kiste;
        }
    }


    //Erzeug ein Stück Wand, einen Turm, oder einen Bogen
    void createWand(Vector3 pos) {
        
        int RandomValue = (int)Random.Range(0f, 21f); //Zahl zwischen 0 und 20
        int xPos = (int)pos.x;
        int zPos = (int)pos.z - SectionDataOffset;
        bool createBogen = true;
        
        if(RandomValue == 0) {
            
            //Erzeugt einen Turm und deaktiviert das ein Bogen erzeugt werden kann
            GameObject Wand = Instantiate(WandPrefab, pos, Quaternion.identity, transform);
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity, transform);
            Wand.tag = "Wand";
            createBogen = false;

            AllGameObjects[(int)pos.x, (int)pos.z] = Wand;

        //Erzeugt ein normales Stück Wand
        } else {
            GameObject Wand = Instantiate(WandPrefab, pos, Quaternion.identity, transform);
            Wand.tag = "Wand";
            rotation += 90;

            AllGameObjects[(int)pos.x, (int)pos.z] = Wand;
        }

        //Erzeug einen Bogen. Wenn RandomValue 10 oder 20 ist.
        //Überprüft das in alle möglich Richtungen eine Wandstück ist zu welchem der Bogen erstellt werden kann.
        //Stellt sicher dass das Array das die levelSectionData nicht überschritten werden kann.
        if ((RandomValue % 10 == 0) && createBogen && (zPos < levelSectionData.Length - 3) && (xPos < levelSectionData[0].Length - 5) &&
            (levelSectionData[zPos][xPos + 2] == levelWand) &&
            (levelSectionData[zPos + 2][xPos] == levelWand) &&
            (levelSectionData[zPos][xPos + 1] != levelWand) &&
            (levelSectionData[zPos + 1][xPos] != levelWand))
        {
            //Ereugt die erste Saule des Bogens
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity, transform);
            SecondaryGameObjects2[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 2, 0), Quaternion.identity, transform);
            
            //Erzeugt den Rest des Bogen horiznal oder um 90 Grad gedreht vertikal.
            //Ternärer-Operator um die Richtung zu bestimmten.
            //Da hier nur 10 oder 20 von RandomValue auftreten kann, und die Bedingung RandomValue == 20 ist, wird zu 50% ein gedrehter Bogen erzeugt
            SecondaryGameObjects3[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(1, 2, 0) : new Vector3(0, 2, 1)), Quaternion.identity, transform);
            SecondaryGameObjects2[(int)pos.x + 1, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 2, 0) : new Vector3(0, 2, 2)), Quaternion.identity, transform);
            SecondaryGameObjects3[(int)pos.x + 1, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 1, 0) : new Vector3(0, 1, 2)), Quaternion.identity, transform);
        }
    }

    //Erzeugt eine Kiste und Boden unter ihr
    void createKiste(Vector3 pos) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(BodenPrefab, pos - new Vector3(0, 0.1f, 0), Quaternion.identity, transform);
        GameObject Kiste = Instantiate(KistePrefab, pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0), transform);
        Kiste.tag = "Kiste";
        rotation += 90;

        AllGameObjects[(int)pos.x, (int)pos.z] = Kiste;
    }


    //Einlesen der LevelTextDatei. Wandelt diese in ein Array um
    string[][] readFile(TextAsset file)
    {
        string[] lines = Regex.Split(file.ToString(), "\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];

        for (int i = 0; i < rows; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], "");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }

    public void setLevelSpeed(float speed)
    {
        this.LevelSpeed = speed; 
    }

    public float getLevelSpeed()
    {
        return LevelSpeed;
    }
}