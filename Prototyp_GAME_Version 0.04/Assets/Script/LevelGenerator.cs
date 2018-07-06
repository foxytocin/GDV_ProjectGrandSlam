using UnityEngine;
using System.Text.RegularExpressions;

public class LevelGenerator : MonoBehaviour
{
    public GameObject BodenPrefab;
    public GameObject WandPrefab;
    public GameObject KistePrefab;

    private int KistenMenge;

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

    private string[][] levelSectionData;
    private string[][] levelBase;
    private int SectionDataOffset;
    private int rotation;
    private bool specialSection;
    private bool GenerateKisten;

    private void Update()
    {
        if(Input.GetKeyDown("1")) {
            GenerateKisten = true;
            KistenMenge = 6;
        }
        if (Input.GetKeyDown("2"))
        {
            GenerateKisten = true;
            KistenMenge = 5;
        }
        if (Input.GetKeyDown("3"))
        {
            GenerateKisten = true;
            KistenMenge = 4;
        }
        if (Input.GetKeyDown("4"))
        {
            GenerateKisten = true;
            KistenMenge = 3;
        }
        if (Input.GetKeyDown("5"))
        {
            GenerateKisten = true;
            KistenMenge = 2;
        }
        if (Input.GetKeyDown("6"))
        {
            GenerateKisten = true;
            KistenMenge = 1;
        }
        if (Input.GetKeyDown("0"))
        {
            GenerateKisten = false;
        }
    }

    // Use this for initialization
    void Start() {
        GenerateKisten = true;
        LevelSpeed = 0.5f;
        KistenMenge = 5;
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        levelSectionData = readFile(LevelTextdatei0);
        AllGameObjects = new GameObject[22,3000];
        SecondaryGameObjects1 = new GameObject[22, 3000];
        SecondaryGameObjects2 = new GameObject[22, 3000];
        SecondaryGameObjects3 = new GameObject[22, 3000];
    }

    public void createWorld(int CameraPosition)
    {
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
                }

            } else {
                levelSectionData = readFile(LevelTextdatei1);
                specialSection = true;
            }
            drawLevelLine(CameraPosition);
        }
        cleanLine((CameraPosition - 30));
    }


    void cleanLine(int CameraPosition) {

        //Debug.Log("CameraPosition: "+CameraPosition+ " / " +levelSectionData[0].Length);
        if(CameraPosition >= 0) {

            for (int i = 0; i < levelSectionData[0].Length; i++)
            {
                if (AllGameObjects[i, CameraPosition] != null)
                    Destroy(AllGameObjects[i, CameraPosition]);

                if(SecondaryGameObjects1[i, CameraPosition] != null)
                    Destroy(SecondaryGameObjects1[i, CameraPosition]);
                
                if (SecondaryGameObjects2[i, CameraPosition] != null)
                    Destroy(SecondaryGameObjects2[i, CameraPosition]);

                if (SecondaryGameObjects3[i, CameraPosition] != null)
                    Destroy(SecondaryGameObjects3[i, CameraPosition]);
            }

        }
    }


    //Zeichnet die Linien der LevelSectionData zeilenweise
    void drawLevelLine(int CameraPosition) {
        
        for (int i = 0; i < levelSectionData[0].Length; i++)
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
    }


    //Erzeugt eine Bodenplatte und zufällig eine Kiste
    void createGang(Vector3 pos, int CameraPosition) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(BodenPrefab, pos, Quaternion.identity);

        if(((int)Random.Range(0f, 21f)) % KistenMenge == 0 && CameraPosition > 11 && GenerateKisten) {
            GameObject Kiste = Instantiate(KistePrefab, pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, 0, rotation));
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
        bool makeBogen = true;
        
        if(RandomValue == 0) {
            
            //Macht einen Turm und deaktiviert das ein Bogen erzeugt werden kann
            GameObject Wand = Instantiate(WandPrefab, pos, Quaternion.identity);
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Wand.tag = "Wand";
            makeBogen = false;

            AllGameObjects[(int)pos.x, (int)pos.z] = Wand;

        //Mach ein normales Stück Wand (ein Cube)
        } else {
            GameObject Wand = Instantiate(WandPrefab, pos, Quaternion.identity);
            Wand.tag = "Wand";
            rotation += 90;

            AllGameObjects[(int)pos.x, (int)pos.z] = Wand;
        }

        //Erzeug einen Bogen.
        //Überprüft das in alle möglich Richtungen eine Wandstück ist zu welchem der Bogen erstellt werden kann.
        //Stellt sicher dass das Array das die levelSectionData nicht überschritten werden kann.
        if ((RandomValue % 10 == 0) && makeBogen && (zPos < levelSectionData.Length - 2) && (xPos < 19) &&
            (levelSectionData[zPos][xPos + 2] == levelWand) &&
            (levelSectionData[zPos + 2][xPos] == levelWand) &&
            (levelSectionData[zPos][xPos + 1] != levelWand) &&
            (levelSectionData[zPos + 1][xPos] != levelWand))
        {
            SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);
            SecondaryGameObjects2[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + new Vector3(0, 2, 0), Quaternion.identity);
            SecondaryGameObjects3[(int)pos.x, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(1, 2, 0) : new Vector3(0, 2, 1)), Quaternion.identity);
            SecondaryGameObjects2[(int)pos.x + 1, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 2, 0) : new Vector3(0, 2, 2)), Quaternion.identity);
            SecondaryGameObjects3[(int)pos.x + 1, (int)pos.z] = Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 1, 0) : new Vector3(0, 1, 2)), Quaternion.identity);
        }
    }

    //Erzeugt eine Kiste und Boden unter ihr
    void createKiste(Vector3 pos) {
        
        SecondaryGameObjects1[(int)pos.x, (int)pos.z] = Instantiate(BodenPrefab, pos, Quaternion.identity);
        GameObject Kiste = Instantiate(KistePrefab, pos + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(0, 0, rotation));
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
}