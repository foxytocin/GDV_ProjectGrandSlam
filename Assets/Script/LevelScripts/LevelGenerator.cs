using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class LevelGenerator : MonoBehaviour
{
    ObjectPooler objectPooler;
    public GameObject GlowBallPrefab;
    public float KistenMenge;
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
    public TextAsset LevelTextdatei9;
    public TextAsset LevelTextdatei10;
    public TextAsset LevelTextdatei11;

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
    private bool generateKisten;
    public bool generateGlowBalls;
    public int tiefeLevelStartBasis = 60;
    public GenerateDistanceLine GenerateDistanceLine;
   
    // Use this for initialization
    void Awake()
    {
        generateGlowBalls = false;
        generateKisten = true;
        LevelSpeed = 0.5f;
        KistenMenge = 10f;
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        AllGameObjects = new GameObject[33, 2000];
        SecondaryGameObjects1 = new GameObject[33, 2000];
        SecondaryGameObjects2 = new GameObject[33, 2000];
        SecondaryGameObjects3 = new GameObject[33, 2000];
        DistanceLines = new GameObject[6, 3000];
    }

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        levelSectionData = readFile(LevelTextdatei0);
        createStartBasis(tiefeLevelStartBasis);
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

            int RandomValue = (int)(Random.Range(0, 13f));

            if(specialSection) {

                switch (RandomValue)
                {
                    case 0:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 1:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 2:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 3:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 4:
                        levelSectionData = readFile(LevelTextdatei2);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 5:
                        levelSectionData = readFile(LevelTextdatei3);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 6:
                        levelSectionData = readFile(LevelTextdatei4);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 7:
                        levelSectionData = readFile(LevelTextdatei5);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 8:
                        levelSectionData = readFile(LevelTextdatei6);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 9:
                        levelSectionData = readFile(LevelTextdatei7);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 10:
                        levelSectionData = readFile(LevelTextdatei8);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 11:
                        levelSectionData = readFile(LevelTextdatei9);
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 12:
                        levelSectionData = readFile(LevelTextdatei10);
                        specialSection = false;
                        generateKisten = false;
                        break;
                    case 13:
                        levelSectionData = readFile(LevelTextdatei11);
                        specialSection = false;
                        generateKisten = false;
                        break;
                    default:
                        Debug.Log("Switch-ERROR in createWorld()");
                        break;
                }

            } else {
                levelSectionData = readFile(LevelTextdatei1);
                specialSection = true;
                generateKisten = true;
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
                KistenMenge = 15f; //20% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 150:
                KistenMenge = 20f; //25% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 200:
                KistenMenge = 25f; //30% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 300:
                KistenMenge = 30f; //35% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 400:
                KistenMenge = 35f; //40% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 500:
                KistenMenge = 40f; //45% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 600:
                KistenMenge = 45f; //50% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 700:
                KistenMenge = 50f; //60% Kisten
                Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 800:
                KistenMenge = 60f; //60% Kisten
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

                for(int i = 0; i < 6; i++)
                {
                    Destroy(DistanceLines[i, CameraPosition - 10].gameObject);
                }
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
        if(CameraPosition > 10 && CameraPosition % 25 == 0 )
            {
                GenerateDistanceLine.createDistanceLine(CameraPosition);
            }
    }


    //Erzeugt eine Bodenplatte und zufällig eine Kiste
    void createGang(Vector3 pos, int CameraPosition) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Boden", pos - new Vector3(0, 0.1f, 0), Quaternion.identity);

        if((Random.value <= (KistenMenge / 100f)) && CameraPosition > 11 && generateKisten) {
            AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Kiste", pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0));
            rotation += 90;
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
            AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos, Quaternion.identity);
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, 0), Quaternion.identity);
            createBogen = false;

        //Erzeugt ein normales Stück Wand
        } else {
            //GameObject Wand = objectPooler.SpawnFromPool("Wand", pos, Quaternion.identity);
            //Wand.tag = "Wand";
            AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos, Quaternion.identity);
            createGlowBall(pos);
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
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, 0), Quaternion.identity);
            SecondaryGameObjects2[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 2, 0), Quaternion.identity);
            
            //Erzeugt den Rest des Bogen horiznal oder um 90 Grad gedreht vertikal.
            //Ternärer-Operator um die Richtung zu bestimmten.
            //Da hier nur 10 oder 20 von RandomValue auftreten kann, und die Bedingung RandomValue == 20 ist, wird zu 50% ein gedrehter Bogen erzeugt
            SecondaryGameObjects3[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + ((RandomValue == 20) ? new Vector3(1, 2, 0) : new Vector3(0, 2, 1)), Quaternion.identity);
            SecondaryGameObjects2[(int)pos.x + 1, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + ((RandomValue == 20) ? new Vector3(2, 2, 0) : new Vector3(0, 2, 2)), Quaternion.identity);
            SecondaryGameObjects3[(int)pos.x + 1, (int)pos.z] = objectPooler.SpawnFromPool("Wand", pos + ((RandomValue == 20) ? new Vector3(2, 1, 0) : new Vector3(0, 1, 2)), Quaternion.identity);
        }
    }

    //Erzeugt eine Kiste und Boden unter ihr
    void createKiste(Vector3 pos)
    {
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Boden", pos - new Vector3(0, 0.1f, 0), Quaternion.identity);
        AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Kiste", pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0));
        rotation += 90;
    }

    void createGlowBall(Vector3 pos)
    {
        if(Random.value > 0.97f && generateGlowBalls)
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("GlowBall", pos + new Vector3(0, 1, 0), Quaternion.identity);
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