using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LevelGenerator : MonoBehaviour
{
    ObjectPooler objectPooler;
    public float KistenMenge;
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
    public TextAsset LevelTextdatei12;
    public TextAsset LevelTextdatei13;
    public TextAsset LevelTextdatei14;
    public List<string[][]> levelPool;
    public string[][] levelSectionData;

    private const string levelGang = "o";
    private const string levelWand = "x";
    private const string levelKiste = "k";

    public GameObject[,] AllGameObjects;
    public GameObject[,] SecondaryGameObjects1;
    public GameObject[,] SecondaryGameObjects2;
    public GameObject[,] SecondaryGameObjects3;
    public GameObject[,] DistanceLines;
    
    private int SectionDataOffset;
    private int rotation;
    private bool specialSection;
    private bool generateKisten;
    public bool generateGlowBalls;
    public int tiefeLevelStartBasis;
    private GenerateDistanceLine GenerateDistanceLine;
    private SpawnDemoItems SpawnDemoItems;
    private MazeGenerator MazeGenerator;
    public bool generateMaze;
    private int dataBufferSize;
    public int levelBreite;
    public int levelTiefe;
    public int TurmMenge;
    public int BogenMenge;
   
    // Use this for initialization
    void Awake()
    {
        levelBreite = 33;
        levelTiefe = 2000;
        generateMaze = false;
        tiefeLevelStartBasis = 60;
        generateGlowBalls = false;
        generateKisten = true;
        KistenMenge = 10f;
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        AllGameObjects = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects1 = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects2 = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects3 = new GameObject[levelBreite, levelTiefe];
        DistanceLines = new GameObject[6, levelTiefe];
        levelPool = new List<string[][]>();
        MazeGenerator = FindObjectOfType<MazeGenerator>();
        SpawnDemoItems = FindObjectOfType<SpawnDemoItems>();
        GenerateDistanceLine = FindObjectOfType<GenerateDistanceLine>();
    }

    void Start()
    {
        TurmMenge = 10; // Menge in %
        BogenMenge = 8; //Menge in %

        objectPooler = ObjectPooler.Instance;
        createLevelData();
        StartCoroutine(createStartBasis(tiefeLevelStartBasis, true));
        SpawnDemoItems.spawnDemoItems();
    }

    public void restartLevel(bool animiert)
    {
        generateMaze = false;
        generateGlowBalls = false;
        generateKisten = true;
        specialSection = false;
        KistenMenge = 10f;
        SectionDataOffset = 0;
        rotation = 0;
        tiefeLevelStartBasis = 60;

        AllGameObjects = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects1 = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects2 = new GameObject[levelBreite, levelTiefe];
        SecondaryGameObjects3 = new GameObject[levelBreite, levelTiefe];
        DistanceLines = new GameObject[6, levelTiefe];

        // createLevelData() komplett neu aufzubauen, wird nur der Startbereich und deren laenge neu zugeordnet
        levelSectionData = levelPool[0];
        dataBufferSize = levelSectionData.Length;

        StartCoroutine(createStartBasis(tiefeLevelStartBasis, animiert));
    }

    // Inizialisiert die Levelbasis die beim Start des Spiels zu sehen sein soll
    // int tiefe definiert wie wieviele Levelzeilen dauerhaft generiert sind
    public IEnumerator createStartBasis(int tiefe, bool animiert)
    {
        generateKisten = false;

        if(!animiert)
        {
            for (int i = 0; i < tiefe; i++) {
                createWorld(i);
                yield return new WaitForSeconds(0.025f);
            }

        } else {

            for (int i = 0; i < tiefe; i++) {
                createWorld(i);
            }
        }

        
    }

    // Steuert die Weltgenerierung
    // Wird durch den CameraScroller aufgerufen wenn dieser Z exakt um 1 weitergefahren ist
    public void createWorld(int CameraPosition)
    {
        setDifficulty(CameraPosition);
        drawLevelLinefromText(CameraPosition);
    }


    // Steuert die Generierung der auf den Textdatein basierenden Levelabschnitte
    private void drawLevelLinefromText(int CameraPosition)
    {
        // Es sind Levelabschnittsdaten im Buffer vorhaden. Diese werden zeilenweise geschrieben
        // dataBufferSize wird bei jeder geschriebenen Zeile um 1 vermindert
        if(dataBufferSize > 0)
        {
            dataBufferSize--;
            drawLevelLine(CameraPosition);

        // Ist die dataBufferSize == 0 wird ein neuer Levelabschnitt in den Buffer geladen
        } else {
            
            SectionDataOffset = CameraPosition;

            int RandomValue = (int)(Random.Range(0f, 19f));

            // Wenn eine specialSection erlaubt wird, wird diese zuaellig ausgewählt und in den dataBuffer geschrieben
            if(specialSection) {

                // dataBufferSize: Zeilen bis ein neuer Abschnitt geladen werden muss
                // specialSection: Definiert ob danacb ein specialSection folgen darf
                // generateKisten: Definiert ob in diesem Levelabschnitt Kisten liegen dürfen
                switch (RandomValue)
                {
                    // 0 - 4 sind normale Levelabschnitt um deren Anteil zu erhoehen
                    // auch wenn ein specialSection generiert werden duerfte
                    case 0:
                        levelSectionData = levelPool[1];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 1:
                        levelSectionData = levelPool[1];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 2:
                        levelSectionData = levelPool[1];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = true;
                        generateKisten = true;
                        break;
                    case 3:
                        levelSectionData = levelPool[1];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = true;
                        generateKisten = true;
                        break;

                    // 4 - 16 sind Special-Levelabschnitte
                    case 4:
                        levelSectionData = levelPool[2];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 5:
                        levelSectionData = levelPool[3];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 6:
                        levelSectionData = levelPool[4];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 7:
                        levelSectionData = levelPool[5];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 8:
                        levelSectionData = levelPool[6];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 9:
                        levelSectionData = levelPool[7];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 10:
                        levelSectionData = levelPool[8];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 11:
                        levelSectionData = levelPool[9];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 12:
                        levelSectionData = levelPool[10];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = false;
                        break;
                    case 13:
                        levelSectionData = levelPool[11];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = false;
                        break;
                    case 14:
                        levelSectionData = levelPool[12];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 15:
                        levelSectionData = levelPool[13];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = true;
                        break;
                    case 16:
                        levelSectionData = levelPool[14];
                        dataBufferSize = levelSectionData.Length;
                        specialSection = false;
                        generateKisten = false;
                        break;
                    case 17:
                        generateMaze = true;
                        break;
                    case 18:
                        generateMaze = true;
                        break;
                    default:
                        Debug.Log("Switch-ERROR in createWorld()");
                        break;
                }

                // Erstellt einen Abschnitt als Maze
                // mazeCalculated: Prueft ob das Maze komplett fertig berechnet ist
                if(generateMaze && MazeGenerator.mazeCalculated) {
                    levelSectionData = MazeGenerator.mazeLevelData;
                    dataBufferSize = levelSectionData.Length;
                    specialSection = false;
                    generateKisten = false;

                    // Der MazeGenerator wird aufgerufen um das naechste Maze zu berechnen
                    generateMaze = false;
                    MazeGenerator.generateNewMaze();

                } else if(generateMaze && !MazeGenerator.mazeCalculated) {

                // Wenn ein Maze erstellt werden sollte aber mazeCalculated != true war, dient das normale Level als fallback
                levelSectionData = levelPool[1];
                dataBufferSize = levelSectionData.Length;
                specialSection = true;
                generateKisten = true;
                }

            // Ein normaler Levelabschnitt wird in den dataBuffer geschrieben
            } else {

                levelSectionData = levelPool[1];
                dataBufferSize = levelSectionData.Length;
                specialSection = true;
                generateKisten = true;
            }

            // Die erste Levelzeile aus dem neu angelegten dataBuffer wird geschrieben
            dataBufferSize--;
            drawLevelLine(CameraPosition);
        }
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
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 150:
                KistenMenge = 15f; //20% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 200:
                KistenMenge = 20f; //25% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 300:
                KistenMenge = 25f; //30% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 500:
                KistenMenge = 30f; //35% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 700:
                KistenMenge = 35f; //40% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 800:
                KistenMenge = 40f; //45% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 900:
                KistenMenge = 45f; //50% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 1000:
                KistenMenge = 50f; //60% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            case 1100:
                KistenMenge = 60f; //60% Kisten
                //Debug.Log("KistenMenge auf " +KistenMenge+ "% erhöht");
                break;
            default:
                break;
        }
    }


    // Wird durch den DestroyScroller aufgerufen wenn dieser Z exakt um 1 weitergefahren ist
    public void cleanLine(int CameraPosition) {
        
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
            if (CameraPosition > 10) {

                for(int i = 0; i < 6; i++)
                {
                    if(DistanceLines[i, CameraPosition - 10] != null)
                    {
                        DistanceLines[i, CameraPosition - 10].gameObject.SetActive(false);
                    }
                    
                }
            }

        }
    }


    //Zeichnet die Linien der LevelSectionData zeilenweise. Abhängig vom Symbol der aktuellen Stelle (Gang, Wand, Kiste)
    void drawLevelLine(int CameraPosition) {

        for (int i = 0; i < levelSectionData[0].Length - 1; i++)
        {
            //Debug.Log("Cam - Offeset: Wert: " +(CameraPosition - SectionDataOffset));
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
        if(CameraPosition > 10 && ((CameraPosition - 4) % 25 == 0) )
            {
                GenerateDistanceLine.createDistanceLine(CameraPosition);
            }
    }


    //Erzeugt eine Bodenplatte und zufällig eine Kiste
    void createGang(Vector3 pos, int CameraPosition) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Boden", pos - new Vector3(0, 0.1f, 0), Quaternion.identity);
        
        if((Random.value <= (KistenMenge / 100f)) && generateKisten) {
            rotation = Random.value > 0.5f ? 0 : 90;
            AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Kiste", pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0));
        }
    }


    //Erzeug ein Stück Wand, einen Turm, oder einen Bogen
    void createWand(Vector3 pos) {
        
        int xPos = (int)pos.x;
        int zPos = (int)pos.z;
        bool createBogen = true;
        
        if(Random.value <= (TurmMenge / 100f)) {
            
            //Erzeugt einen Turm und deaktiviert das ein Bogen erzeugt werden kann
            AllGameObjects[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos, Quaternion.identity);
            SecondaryGameObjects1[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, 0), Quaternion.identity);
            createBogen = false;

        //Erzeugt ein normales Stück Wand
        } else {

            AllGameObjects[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos, Quaternion.identity);
            
            // Ezeugt glowBalls wenn generateGlowBalls urch den DayNightSwitch.cs auf true gesetzt wurde
            if(generateGlowBalls)
                createGlowBall(pos);
        }

        // Erzeug einen Bogen. Wenn RandomValue 10 oder 20 ist.
        // Überprüft das in alle möglich Richtungen eine Wandstück ist zu welchem der Bogen erstellt werden kann.
        // Stellt sicher dass das Array das die levelSectionData nicht überschritten werden kann.
        if (Random.value <= (BogenMenge / 100f) && createBogen)
        {
            if( Random.value > 0.45f && xPos > 2 &&
                SecondaryGameObjects1[xPos - 1, zPos] != null &&
                AllGameObjects[xPos - 2, zPos] != null)

            {
                // Bogen in xRichtung
                if( SecondaryGameObjects1[xPos - 1, zPos].CompareTag("Boden") &&
                    AllGameObjects[xPos - 2, zPos].CompareTag("Wand") &&
                    SecondaryGameObjects1[xPos - 2, zPos] == null)
                    {
                        // Ereugt die erste Saule des Bogens
                        SecondaryGameObjects1[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, 0), Quaternion.identity);
                        SecondaryGameObjects2[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 2, 0), Quaternion.identity);
            
                        SecondaryGameObjects3[xPos - 1, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-1, 2, 0), Quaternion.identity);
                        SecondaryGameObjects2[xPos - 2, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-2, 2, 0), Quaternion.identity);
                        SecondaryGameObjects1[xPos - 2, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-2, 1, 0), Quaternion.identity);

                        // Zweiter Bogenabschnitt
                        if( Random.value <= (BogenMenge / 100f) &&
                            xPos > 4 &&
                            SecondaryGameObjects1[xPos - 3, zPos] != null &&
                            AllGameObjects[xPos - 4, zPos] != null &&
                            SecondaryGameObjects1[xPos - 3, zPos].CompareTag("Boden") &&
                            AllGameObjects[xPos - 4, zPos].CompareTag("Wand") &&
                            SecondaryGameObjects1[xPos - 4, zPos] == null)
                            {
                                // Baut an den ersten Bogen nur die fehlenden Teile an     
                                SecondaryGameObjects3[xPos - 3, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-3, 2, 0), Quaternion.identity);
                                SecondaryGameObjects2[xPos - 4, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-4, 2, 0), Quaternion.identity);
                                SecondaryGameObjects1[xPos - 4, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-4, 1, 0), Quaternion.identity);

                            }
                    }

            } else if(  zPos > 1  &&
                        SecondaryGameObjects1[xPos, zPos - 1] != null &&
                        AllGameObjects[xPos, zPos - 2] != null)
                        {

                        // Bogen in zRichtung
                        if( SecondaryGameObjects1[xPos, zPos - 1].CompareTag("Boden") &&
                            AllGameObjects[xPos, zPos - 2].CompareTag("Wand") &&
                            SecondaryGameObjects1[xPos, zPos - 2] == null)
                            {
                                //Ereugt die erste Saule des Bogens
                                SecondaryGameObjects1[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, 0), Quaternion.identity);
                                SecondaryGameObjects2[xPos, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 2, 0), Quaternion.identity);
                    
                                SecondaryGameObjects3[xPos, zPos - 1] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 2, -1), Quaternion.identity);
                                SecondaryGameObjects2[xPos, zPos - 2] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 2, -2), Quaternion.identity);
                                SecondaryGameObjects1[xPos, zPos - 2] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(0, 1, -2), Quaternion.identity);

                                // Zweiter Bogenabschnitt gedreht
                                if( Random.value <= (BogenMenge / 100f) &&
                                    xPos > 2 &&
                                    SecondaryGameObjects1[xPos - 1, zPos] != null &&
                                    SecondaryGameObjects1[xPos - 2, zPos] == null &&
                                    SecondaryGameObjects1[xPos - 1, zPos].CompareTag("Boden") &&
                                    AllGameObjects[xPos - 2, zPos].CompareTag("Wand"))
                                    {
                                        // Baut an den ersten Bogen nur die fehlenden Teile an
                                        SecondaryGameObjects3[xPos - 1, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-1, 2, 0), Quaternion.identity);
                                        SecondaryGameObjects2[xPos - 2, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-2, 2, 0), Quaternion.identity);
                                        SecondaryGameObjects1[xPos - 2, zPos] = objectPooler.SpawnFromPool("Wand", pos + new Vector3(-2, 1, 0), Quaternion.identity);

                                }
                            }
            }

        }
        
    }

    //Erzeugt eine Kiste und Boden unter ihr
    void createKiste(Vector3 pos)
    {
        rotation = Random.value > 0.5f ? 0 : 90;
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Boden", pos - new Vector3(0, 0.1f, 0), Quaternion.identity);
        AllGameObjects[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("Kiste", pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, rotation, 0));
    }

    // Ezeugt glowBalls wenn generateGlowBalls urch den DayNightSwitch.cs auf true gesetzt wurde
    void createGlowBall(Vector3 pos)
    {
        if(Random.value > 0.97f)
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = objectPooler.SpawnFromPool("GlowBall", pos + new Vector3(0f, 1f, 0f), Quaternion.identity);
    }


    // Iniziales Einlesen der LevelTextdatein in den levelPool
    // Aus dem levelPool werden spater zufaerllige Levelabschnitt entnommen und kombiniert
    void createLevelData()
    {
        levelPool.Add(readFile(LevelTextdatei0));
        levelPool.Add(readFile(LevelTextdatei1));
        levelPool.Add(readFile(LevelTextdatei2));
        levelPool.Add(readFile(LevelTextdatei3));
        levelPool.Add(readFile(LevelTextdatei4));
        levelPool.Add(readFile(LevelTextdatei5));
        levelPool.Add(readFile(LevelTextdatei6));
        levelPool.Add(readFile(LevelTextdatei7));
        levelPool.Add(readFile(LevelTextdatei8));
        levelPool.Add(readFile(LevelTextdatei9));
        levelPool.Add(readFile(LevelTextdatei10));
        levelPool.Add(readFile(LevelTextdatei11));
        levelPool.Add(readFile(LevelTextdatei12));
        levelPool.Add(readFile(LevelTextdatei13));
        levelPool.Add(readFile(LevelTextdatei14));

        levelSectionData = levelPool[0];
        dataBufferSize = levelSectionData.Length;
    }


    // Parsen der LevelTextdatein zu einem string-Array
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

}