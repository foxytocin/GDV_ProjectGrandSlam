using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

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

    public const string levelGang = "o";
    public const string levelWand = "x";

    public List<GameObject> AllGameObjects;
    string[][] levelSectionData;
    public string[][] levelBase;
    int SectionDataOffset;
    int rotation;
    bool specialSection;
    int CameraPositon;

    private void Update()
    {
        if(Input.GetKeyDown("1")) {
            KistenMenge = 6;
        }
        if (Input.GetKeyDown("2"))
        {
            KistenMenge = 5;
        }
        if (Input.GetKeyDown("3"))
        {
            KistenMenge = 4;
        }
        if (Input.GetKeyDown("4"))
        {
            KistenMenge = 3;
        }
        if (Input.GetKeyDown("5"))
        {
            KistenMenge = 2;
        }
        if (Input.GetKeyDown("6"))
        {
            KistenMenge = 1;
        }
    }

    // Use this for initialization
    void Start() {
        LevelSpeed = 0.5f;
        KistenMenge = 5;
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        levelSectionData = readFile(LevelTextdatei0);
    }

    public void createWorld(int CameraPosition)
    {
        //Debug.Log("createWorld: CameraPosition: "+CameraPosition+ " / SectionDateOffset: " +SectionDataOffset);
        this.CameraPositon = CameraPosition;
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
                        levelSectionData = readFile(LevelTextdatei6);
                        specialSection = false;
                        break;
                    case 5:
                        levelSectionData = readFile(LevelTextdatei2);
                        specialSection = false;
                        break;
                    case 6:
                        levelSectionData = readFile(LevelTextdatei3);
                        specialSection = false;
                        break;
                    case 7:
                        levelSectionData = readFile(LevelTextdatei4);
                        specialSection = false;
                        break;
                    case 8:
                        levelSectionData = readFile(LevelTextdatei4);
                        specialSection = false;
                        break;
                    case 9:
                        levelSectionData = readFile(LevelTextdatei5);
                        specialSection = false;
                        break;
                    case 10:
                        levelSectionData = readFile(LevelTextdatei1);
                        specialSection = true;
                        break;
                }

            } else {
                levelSectionData = readFile(LevelTextdatei1);
                specialSection = true;
            }

            drawLevelLine(CameraPosition);
        }
    }

    //Zeichnet die Linien der LevelSectionData zeilenweise
    void drawLevelLine(int CameraPosition) {

        for (int x = 0; x < levelSectionData[0].Length; x++)
        {
            switch (levelSectionData[CameraPosition - SectionDataOffset][x])
            {
                case levelGang:
                    createGang(new Vector3(x, 0f, CameraPosition));
                    break;
                case levelWand:
                    createWand(new Vector3(x, 0.5f, CameraPosition));
                    break;
            }
        }
    }

    //Erzeugt eine Bodenplatte und zufällig eine Kiste
    void createGang(Vector3 pos) {
        
        Instantiate(BodenPrefab, pos, Quaternion.identity);

        if(((int)Random.Range(0f, 21f)) % KistenMenge == 0 && this.CameraPositon > 11) {
            GameObject Kiste = Instantiate(KistePrefab, pos + new Vector3(0, 0.5f, 0), Quaternion.Euler(0,0, rotation));
            rotation += 90;

            AllGameObjects.Add(Kiste);
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
            Instantiate(WandPrefab, pos, Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity); //TUERME SIND DEAKTIVIERT
            makeBogen = false;

        //Mach ein normales Stück Wand (ein Cube)
        } else {
            Instantiate(WandPrefab, pos, Quaternion.Euler(0, 0, rotation));
            rotation += 90;
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
            Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(0, 2, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(1, 2, 0) : new Vector3(0, 2, 1)), Quaternion.identity);
            Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 2, 0) : new Vector3(0, 2, 2)), Quaternion.identity);
            Instantiate(WandPrefab, pos + ((RandomValue == 20) ? new Vector3(2, 1, 0) : new Vector3(0, 1, 2)), Quaternion.identity);
        }
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