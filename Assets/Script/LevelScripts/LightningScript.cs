using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningScript : MonoBehaviour
{
    GameObject turtle;
    
    public int depth = 1;

    private Mesh meinMesh;
    private List<Vector3> verts;
    private List<Vector3> faceNormals;
    private List<Vector3> vertexNormals;
    private List<int> faces;
    //private int offset = 1;

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

        GetComponent<MeshFilter>().mesh = meinMesh = new Mesh();        //Gleiche Instanz Zuweisung
        mat = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        
        //Blitz erzeugen
        //GenerateLightning(new Vector3(0f, 15f, -20f));
        GenerateLightning(new Vector3(0f, 15f, 0f));


        // Mesh Vertices hinzufuegen
        meinMesh.vertices = verts.ToArray();

        // Mesh Faces hinzufuegen     
        meinMesh.triangles = makeFaces(meinMesh.vertices); ;

        // Vertices Normals aus Facenormals berechnen 
        meinMesh.normals = setVertexNormals(faceNormals);

        // Debug
        //showVerts();
        //showFaces();
        //showNormals(normalsMesh);
        //showNormals(meinMesh.normals
        //Vector3[] fnA = faceNormals.ToArray();
        //showNormals(fnA);

        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {
        emission = Mathf.PingPong(Time.time, 1f);
        Color finalColor = Color.white * Mathf.LinearToGammaSpace(emission * 3f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.EnableKeyword("_EMISSION");

        //Vector3[] fnA = faceNormals.ToArray();
        for (int i = 0; i < meinMesh.vertices.Length; i++)
        {
            Vector3 norm = transform.TransformDirection(meinMesh.normals[i]);
            Vector3 vert = transform.TransformPoint(meinMesh.vertices[i]);
            //Debug.DrawRay(vert, norm * 0.3f, Color.red);
        }
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


    // Kochkurve
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

        for(int i = 20; i > 0; i--)
        {
            if (Random.value < 0.5f)
            {
                turn(90f);
                move(Random.Range(0.0f, maxXStep), 0, i/20f);
                turn(-90f);
                move(Random.Range(0.0f, maxYStep), 1, i/20f);
            }
            else
            {
                turn(-90f);
                move(Random.Range(0.0f, maxXStep), 0, i/20f);
                turn(90f);
                move(Random.Range(0.0f, maxYStep), 1, i/20f);
            }
        }
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




