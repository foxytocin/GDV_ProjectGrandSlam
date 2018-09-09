using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningScript : MonoBehaviour
{
    GameObject turtle;
    public GameObject lightningLChild_Prefab;
    public GameObject lightningRChild_Prefab;
    public GameObject lightning_prefab;

    public LightningSpawner lightningSpawner;

    public int depth = 1;

    private Mesh meinMesh;
    private List<Vector3> verts;
    private List<Vector3> faceNormals;
    private List<Vector3> vertexNormals;
    private List<int> faces;

    private float maxXStep = 1.0f;
    private float maxYStep = 2.0f;

    Material mat;
    float emission;


    void Start()
    {
        // Listen initialisieren
        faces = new List<int>();
        verts = new List<Vector3>();
        faceNormals = new List<Vector3>();
        vertexNormals = new List<Vector3>();

        lightningSpawner = FindObjectOfType<LightningSpawner>();

        GetComponent<MeshFilter>().mesh = meinMesh = new Mesh();        //Gleiche Instanz Zuweisung
        mat = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        //Blitz erzeugen
        GenerateLightning(new Vector3(0f, 20f, 0f));


        // Mesh Vertices hinzufuegen
        meinMesh.vertices = verts.ToArray();

        // Mesh Faces hinzufuegen     
        meinMesh.triangles = makeFaces(meinMesh.vertices); ;

        // Vertices Normals aus Facenormals berechnen 
        meinMesh.normals = setVertexNormals(faceNormals);

        emission = 1f;
        Color finalColor = Color.white * Mathf.LinearToGammaSpace(emission * 3f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.EnableKeyword("_EMISSION");

        Destroy(gameObject, 0.5f);
        Destroy(turtle, 0.5f);
    }
    
    int[] makeFaces(Vector3[] vertArr)
    {
        for (var i = 0; i < vertArr.Length - 3; i += 2)
        {
            faces.Add(i);
            faces.Add(i + 1);
            faces.Add(i + 2);
            Vector3 norm = calcNormal(vertArr[i], vertArr[i + 1], vertArr[i + 2]);
            faceNormals.Add(norm);

            faces.Add(i + 1);
            faces.Add(i + 3);
            faces.Add(i + 2);
        }
        return faces.ToArray();
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


    // Turtle bewegen
    void move(float length, int secondStep, float offset)
    {
        turtle.transform.Translate(length, 0, 0);
        Vector3 P = turtle.transform.position;
        if(secondStep == 1)
        {
            verts.Add(P);
            verts.Add(P + new Vector3(offset, 0, 0));
        }
    }

    void turn(float deg)
    {
        turtle.transform.Rotate(0, 0, deg);
    }

    //Rekursiv?
    public void GenerateLightning(Vector3 pos)
    {
        // GameObject initialisieren
        turtle = new GameObject("Turtle");
        turtle.transform.Translate(pos);
        Vector3 P = turtle.transform.position;
        verts.Add(P);
        verts.Add(P + new Vector3(1, 0, 0));

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
                        GameObject lc = Instantiate(lightningLChild_Prefab, lightningSpawner.thunderPos + new Vector3(verts[verts.Count - 2].x + 0.4f, turtle.transform.position.y, 0), rot);
                        lc.transform.parent = gameObject.transform;
                        LightningLeftChildScript lcs = lc.GetComponent<LightningLeftChildScript>();
                        lcs.init();
                        lcs.GenerateLeftChildLightning(new Vector3(0, 0, 0), i);
                    }
                    else
                    {
                        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
                        GameObject rc = Instantiate(lightningRChild_Prefab, lightningSpawner.thunderPos + new Vector3(verts[verts.Count - 2].x + 0.4f, turtle.transform.position.y, 0), rot);
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




