using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningScript : MonoBehaviour
{
    GameObject turtle;
    GameObject turtleChild;
    
    public int offset = 1;

    private Mesh meinMesh;
    private Mesh meinMeshChild;
    private List<Vector3> verts;
    private List<Vector3> vertsChild;
    private List<Vector3> faceNormals;
    private List<Vector3> faceNormalsChild;
    private List<Vector3> vertexNormals;
    private List<Vector3> vertexNormalsChild;
    private List<int> faces;
    private List<int> facesChild;
    //private int offset = 1;
    public GameObject lightning_prefab;

    private float maxXStep = 1.0f;
    private float maxYStep = 2.0f;

    private int lightningDepth = 20;
    //private int childLightningDepth = 10;

    Material mat;
    float emission;

    private bool makeChild = true;

    void Start()
    {
        // Listen initialisieren
        faces = new List<int>();
        verts = new List<Vector3>();
        faceNormals = new List<Vector3>();
        vertexNormals = new List<Vector3>();
        facesChild = new List<int>();
        vertsChild = new List<Vector3>();
        faceNormalsChild = new List<Vector3>();
        vertexNormalsChild = new List<Vector3>();

        GetComponent<MeshFilter>().mesh = meinMesh = new Mesh();        //Gleiche Instanz Zuweisung
        meinMeshChild = new Mesh();        
        mat = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        //Blitz erzeugen
        //GenerateLightning(new Vector3(0f, 15f, -20f));
        GenerateLightning(new Vector3(0f, 15f, 0f));


        // Mesh Vertices hinzufuegen
        meinMesh.vertices = verts.ToArray();        

        // Mesh Faces hinzufuegen     
        meinMesh.triangles = makeFaces(meinMesh.vertices, faces, faceNormals);
        
        // Vertices Normals aus Facenormals berechnen 
        meinMesh.normals = setVertexNormals(faceNormals, vertexNormals);
       

        // Debug
        //showVerts();
        //showFaces();
        //showNormals(normalsMesh);
        //showNormals(meinMesh.normals
        //Vector3[] fnA = faceNormals.ToArray();
        //showNormals(fnA);

        Destroy(gameObject, 0.5f);
        Destroy(turtle, 0.5f);
    }

    private void Update()
    {
        emission = Mathf.PingPong(Time.time, 1f);
        Color finalColor = Color.white * Mathf.LinearToGammaSpace(emission * 3f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.EnableKeyword("_EMISSION");
        /*
        //Vector3[] fnA = faceNormals.ToArray();
        for (int i = 0; i < meinMesh.vertices.Length; i++)
        {
            Vector3 norm = transform.TransformDirection(meinMesh.normals[i]);
            Vector3 vert = transform.TransformPoint(meinMesh.vertices[i]);
            //Debug.DrawRay(vert, norm * 0.3f, Color.red);
        }
        */
    }
    
    int[] makeFaces(Vector3[] vertArr, List<int> f, List<Vector3> fN)
    {
        for (var i = 0; i < vertArr.Length - 3; i += 2)
        {
            f.Add(i);
            f.Add(i + 1);
            f.Add(i + 2);
            Vector3 norm = calcNormal(vertArr[i], vertArr[i + 1], vertArr[i + 2]);
            fN.Add(norm);

            f.Add(i + 1);
            f.Add(i + 3);
            f.Add(i + 2);
        }
        return f.ToArray();
    }

    Vector3[] setVertexNormals(List<Vector3> fNorms, List<Vector3> vertNorm)
    {
        Vector3[] fNormalsArray = fNorms.ToArray();
        vertNorm.Add(fNormalsArray[0]);
        vertNorm.Add(fNormalsArray[0]);

        for (int i = 1; i < fNormalsArray.Length; i++)
        {
            Vector3 vN = fNormalsArray[i] + fNormalsArray[i - 1];
            vN = vN.normalized;
            vertNorm.Add(vN);
            vertNorm.Add(vN);
        }
        vertNorm.Add(fNormalsArray[fNormalsArray.Length - 1]);
        vertNorm.Add(fNormalsArray[fNormalsArray.Length - 1]);

        return vertNorm.ToArray();
    }

    Vector3 calcNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 ab = b - a;
        Vector3 ac = c - a;

        return Vector3.Cross(ab, ac).normalized;
    }


    // Kochkurve
    void move(float length, int secondStep, float offs, GameObject t, List<Vector3> vertices)
    {
        t.transform.Translate(length, 0, 0);
        Vector3 P = t.transform.position;
        if(secondStep == 1)
        {
            vertices.Add(P);
            vertices.Add(P + new Vector3(offs, 0, 0));
        }
    }

    void turn(float deg, GameObject t)
    {
        t.transform.Rotate(0, 0, deg);
    }

    //Rekursiv?
    public void GenerateLightning(Vector3 pos)
    {
        // GameObject initialisieren
        turtle = new GameObject("Turtle");
        turtle.transform.Translate(pos);
        Vector3 P = turtle.transform.position;
        //Zwei Anfangsvertices (oberste Kante) werden gespeichert
        verts.Add(P);
        verts.Add(P + new Vector3(1, 0, 0));

        //Turtle bewegt sich nach rechts, Vertice wird nicht gespeichert
        move(Random.Range(0.0f, maxXStep), 0, 1f, turtle, verts);
        //Drehung im Uhrzeigersinn nach unten
        turn(-90f, turtle);
        //Turtle bewegt sich nach unten, Vertices werden gespeichert!
        move(Random.Range(0.0f, maxYStep), 1, 1f, turtle, verts);

        for(int i = lightningDepth; i > 0; i--)
        {
            float off = i / (float)lightningDepth;
            // 50/50 Chance, ob Turtle sich nach links oder rechts bewegt  (<0.5=rechts)
            if (Random.value < 0.5f)
            {
                turn(90f, turtle);
                move(Random.Range(0.0f, maxXStep), 0, off, turtle, verts);
                turn(-90f, turtle);
                move(Random.Range(0.0f, maxYStep), 1, off, turtle, verts);
                
            }
            else
            {
                turn(-90f, turtle);
                move(Random.Range(0.0f, maxXStep), 0, off, turtle, verts);
                turn(90f, turtle);
                move(Random.Range(0.0f, maxYStep), 1, off, turtle, verts);

                //ChildLightning
                int randomChildLightningProb = Mathf.RoundToInt(Random.value * 12);
                Debug.Log(randomChildLightningProb);
                if (randomChildLightningProb == 6 && makeChild)
                {
                    Instantiate(lightning_prefab, turtle.transform.position, Quaternion.identity);
                    //GenerateChildLightning(off);
                    makeChild = false;
                }
            }
        }
    }
    public void GenerateChildLightning(float off)
    {
        Debug.Log("SSS");
        turtleChild = new GameObject("TurtleChild");

        turtleChild.transform.position = turtle.transform.position;
        Vector3 PC = turtle.transform.position;

        vertsChild.Add(PC);
        vertsChild.Add(PC + new Vector3(off / 2f, 0, 0));

        turn(-180f, turtleChild);
        move(Random.Range(0.0f, maxXStep), 0, 1f, turtleChild, vertsChild);
        turn(90f, turtle);
        move(Random.Range(0.0f, maxYStep), 1, 1f, turtleChild, vertsChild);

        int childLightningDepth = Mathf.RoundToInt((off/20f) / 2);
        for (int j = childLightningDepth; j > 0; j--)
        {
            float offChild = j / (float)childLightningDepth;
            // 50/50 Chance, ob Turtle sich nach links oder rechts bewegt  (<0.5=rechts)
            if (Random.value < 0.5f)
            {
                turn(90f, turtleChild);
                move(Random.Range(0.0f, maxXStep), 0, offChild, turtleChild, vertsChild);
                turn(-90f, turtleChild);
                move(Random.Range(0.0f, maxYStep), 1, offChild, turtleChild, vertsChild);

            }
            else
            {
                turn(-90f, turtleChild);
                move(Random.Range(0.0f, maxXStep), 0, offChild, turtleChild, vertsChild);
                turn(90f, turtleChild);
                move(Random.Range(0.0f, maxYStep), 1, offChild, turtleChild, vertsChild);
            }
        }
        meinMeshChild.vertices = vertsChild.ToArray();
        meinMeshChild.triangles = makeFaces(meinMeshChild.vertices, facesChild, faceNormalsChild);
        meinMeshChild.normals = setVertexNormals(faceNormalsChild, vertexNormalsChild);
    }


    // Debug Methods
    // void showVerts()
    // {
    //     Vector3[] vertArray = verts.ToArray();
    //     for (int i = 0; i < vertArray.Length; i++)
    //     {
    //         Debug.Log(vertArray[i]);
    //     }
    // }

    // void showFaces()
    // {
    //     int[] faceArray = faces.ToArray();
    //     for (int i = 0; i < faceArray.Length; i++)
    //     {
    //         Debug.Log(faceArray[i]);
    //     }
    // }
    // void showNormals(Vector3[] normA)
    // {
    //     for (int i = 0; i < normA.Length; i++)
    //     {
    //         Debug.Log(normA[i]);
    //     }
    // }
}