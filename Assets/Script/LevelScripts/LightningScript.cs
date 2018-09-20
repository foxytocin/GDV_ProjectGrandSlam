using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningScript : MonoBehaviour
{
    GameObject lightningTurtle;
    public GameObject lightningLChild_Prefab;
    public GameObject lightningRChild_Prefab;
    public GameObject lightning_prefab;

    public LightningSpawner lightningSpawner;

    public int depth = 1;

    private Mesh lightningMesh;
    private List<Vector3> lightningVertices;
    private List<Vector3> faceNormals;
    private List<Vector3> vertexNormals;
    private List<int> lightningFaces;

    private float maxXStep = 1.0f;
    private float maxYStep = 2.0f;

    Material mat;
    float emission;


    void Start()
    {
        // Listen initialisieren
        lightningFaces = new List<int>();
        lightningVertices = new List<Vector3>();
        faceNormals = new List<Vector3>();
        vertexNormals = new List<Vector3>();

        lightningSpawner = FindObjectOfType<LightningSpawner>();

        GetComponent<MeshFilter>().mesh = lightningMesh = new Mesh();        //Gleiche Instanz Zuweisung
        mat = GetComponent<MeshRenderer>().material;


        //Ausschalten der Blitzschatten
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        //Blitz erzeugen
        GenerateLightning(new Vector3(0f, 20f, 0f));


        // Mesh Vertices hinzufuegen
        lightningMesh.vertices = lightningVertices.ToArray();

        // Mesh Faces hinzufuegen     
        lightningMesh.triangles = makeFaces(lightningMesh.vertices); ;

        // Vertices Normals aus Facenormals berechnen 
        lightningMesh.normals = setVertexNormals(faceNormals);

        //helles leuchtendes Weiß als Material
        emission = 1f;
        Color finalColor = Color.white * Mathf.LinearToGammaSpace(emission * 3f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.EnableKeyword("_EMISSION");

        //Selbstzerstörung des Blitzes nach 0.5 Sekunden
        Destroy(gameObject, 0.5f);
        Destroy(lightningTurtle, 0.5f);
    }
    
    int[] makeFaces(Vector3[] vertArr)
    {
        for (var i = 0; i < vertArr.Length - 3; i += 2)
        {
            lightningFaces.Add(i);
            lightningFaces.Add(i + 1);
            lightningFaces.Add(i + 2);
            Vector3 norm = calcNormal(vertArr[i], vertArr[i + 1], vertArr[i + 2]);
            faceNormals.Add(norm);

            lightningFaces.Add(i + 1);
            lightningFaces.Add(i + 3);
            lightningFaces.Add(i + 2);
        }
        return lightningFaces.ToArray();
    }

    Vector3[] setVertexNormals(List<Vector3> fNorms)
    {
        Vector3[] fNormalsArray = fNorms.ToArray();
        vertexNormals.Add(fNormalsArray[0]);
        vertexNormals.Add(fNormalsArray[0]);

        for (int i = 1; i < fNormalsArray.Length; i++)
        {
            Vector3 vN = fNormalsArray[i] + fNormalsArray[i - 1];
            vN = vN.normalized;
            vertexNormals.Add(vN);
            vertexNormals.Add(vN);
        }
        vertexNormals.Add(fNormalsArray[fNormalsArray.Length - 1]);
        vertexNormals.Add(fNormalsArray[fNormalsArray.Length - 1]);

        return vertexNormals.ToArray();
    }

    Vector3 calcNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 ab = b - a;
        Vector3 ac = c - a;

        return Vector3.Cross(ab, ac).normalized;
    }


    // LightningTurtle bewegen
    void move(float length, int secondStep, float offset)
    {
        lightningTurtle.transform.Translate(length, 0, 0);
        Vector3 P = lightningTurtle.transform.position;
        if(secondStep == 1)
        {
            lightningVertices.Add(P);
            lightningVertices.Add(P + new Vector3(offset, 0, 0));
        }
    }

    void turn(float deg)
    {
        lightningTurtle.transform.Rotate(0, 0, deg);
    }
    
    public void GenerateLightning(Vector3 pos)
    {
        // GameObject initialisieren
        lightningTurtle = new GameObject("LightningTurtle");
        lightningTurtle.transform.Translate(pos);
        Vector3 P = lightningTurtle.transform.position;
        lightningVertices.Add(P);
        lightningVertices.Add(P + new Vector3(1, 0, 0));

        move(Random.Range(0.0f, maxXStep), 0, 1f);
        turn(-90f);
        move(Random.Range(0.0f, maxYStep), 1, 1f);

        for(int i = 25; i > 0; i--)
        {
            // 50/50 Chance, ob Turtle sich nach links oder rechts bewegt  (<0.5=rechts)
            if (Random.value < 0.5f)
            {
                turn(90f);
                move(Random.Range(0.0f, maxXStep), 0, i/25f);
                turn(-90f);
                move(Random.Range(0.0f, maxYStep), 1, i/25f);
            }
            else
            {
                turn(-90f);
                move(Random.Range(0.0f, maxXStep), 0, i/25f);
                turn(90f);
                move(Random.Range(0.0f, maxYStep), 1, i/25f);
                
                //ChildLightning
                int randomChildLightningProb = Mathf.RoundToInt((Random.value * 12) + ((i * i * i) / 1300f));
                if (randomChildLightningProb > 10)
                {
                    if (Random.value < 0.5f)
                    {
                        Quaternion rot = Quaternion.Euler(180f, 0f, 0f);
                        GameObject lc = Instantiate(lightningLChild_Prefab, lightningSpawner.thunderPos + new Vector3(lightningVertices[lightningVertices.Count - 2].x + 0.4f, lightningTurtle.transform.position.y, 0), rot);
                        lc.transform.parent = gameObject.transform;
                        LightningLeftChildScript lcs = lc.GetComponent<LightningLeftChildScript>();
                        lcs.init();
                        lcs.GenerateLeftChildLightning(new Vector3(0, 0, 0), i);
                    }
                    else
                    {
                        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
                        GameObject rc = Instantiate(lightningRChild_Prefab, lightningSpawner.thunderPos + new Vector3(lightningVertices[lightningVertices.Count - 2].x + 0.4f, lightningTurtle.transform.position.y, 0), rot);
                        rc.transform.parent = gameObject.transform;
                        LightningRightChildScript rcs = rc.GetComponent<LightningRightChildScript>();
                        rcs.init();
                        rcs.GenerateRightChildLightning(new Vector3(0, 0, 0), i);
                    }
                }
            }
        }
    }
}




