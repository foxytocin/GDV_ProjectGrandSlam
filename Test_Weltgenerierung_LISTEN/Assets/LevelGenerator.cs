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

    public TextAsset LevelTextdatei0;
    public TextAsset LevelTextdatei1;
    public TextAsset LevelTextdatei2;
    public TextAsset LevelTextdatei3;
    public TextAsset LevelTextdatei4;
    public TextAsset LevelTextdatei5;
    public TextAsset LevelTextdatei6;

    public const string levelGang = "o";
    public const string levelWand = "x";

    string[][] levelSectionData;
    public string[][] levelBase;
    int SectionDataOffset;
    int rotation;
    bool specialSection;
    int CameraPositon;


    // Use this for initialization
    void Start() {
        SectionDataOffset = 0;
        rotation = 0;
        specialSection = false;
        levelSectionData = readFile(LevelTextdatei0);
    }

    public void createWorld(int CameraPosition)
    {
        Debug.Log("createWorld: CameraPosition: "+CameraPosition+ " / SectionDateOffset: " +SectionDataOffset);
        this.CameraPositon = CameraPosition;
        if(CameraPosition - SectionDataOffset < levelSectionData.Length) {
            drawLevelLine(levelSectionData, CameraPosition);

        } else {
            SectionDataOffset = CameraPosition;

            int RandomValue = ((int)(Random.value * 10));

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

            drawLevelLine(levelSectionData, CameraPosition);
        }
    }


    //Zeichnet die Linien der LevelSectionData zeilenweise
    void drawLevelLine(string[][] levelSectionData, int CameraPosition) {

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

        if(((int)(Random.value * 10)) % 5 == 0 && this.CameraPositon > 11) {
            Instantiate(KistePrefab, pos + new Vector3(0, 0.5f, 0), Quaternion.Euler(0,0, rotation));
            rotation += 90;
        }
    }


    //Erzeug ein Stück Wand, einen Turm, oder einen Bogen
    void createWand(Vector3 pos) {
        
        int RandomValue = (int)(Random.value * 10);
        bool makeBogen = false;
        
        if(RandomValue % 5 == 0) {

            //Macht einen Turm und deaktiviert das ein Bogen erzeugt werden kann
            Instantiate(WandPrefab, pos, Quaternion.identity);
            //Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity); //TUERME SIND DEAKTIVIERT
            makeBogen = false; //DEAKTIVIERT

        } else {
            Instantiate(WandPrefab, pos, Quaternion.Euler(0, 0, rotation));
            rotation += 90;
        }

        //Erzeug einen Bogen
        if (RandomValue == 0 && makeBogen && pos.x < 19) {
            Instantiate(WandPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(0, 2, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(1, 2, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(2, 2, 0), Quaternion.identity);
            Instantiate(WandPrefab, pos + new Vector3(2, 1, 0), Quaternion.identity);
        }
    }


    //Einlesen der LevelTextDatei. Wandelt diese in ein Array um
    string[][] readFile(TextAsset file)
    {
        string[] lines = Regex.Split(file.ToString(), "\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], "");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
}