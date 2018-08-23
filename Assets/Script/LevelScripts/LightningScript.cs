using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningScript : MonoBehaviour
{
    GameObject turtle;

    private float l = 3f;
    public int depth = 1;

    private Mesh meinMesh;
    private List<Vector3> verts;
    private List<Vector3> faceNormals;
    private List<Vector3> vertexNormals;
    private List<int> faces;
    private int offset = 1;


    
    /*
    void Start()
    {
        // Listen initialisieren
        faces = new List<int>();
        verts = new List<Vector3>();
        faceNormals = new List<Vector3>();
        vertexNormals = new List<Vector3>();

        GetComponent<MeshFilter>().mesh = meinMesh = new Mesh();        //Gleiche Instanz Zuweisung
        Material mat = GetComponent<MeshRenderer>().material;

        // GameObject initialisieren
        turtle = new GameObject("Turtle");
        Vector3 P = turtle.transform.position;
        verts.Add(P);
        verts.Add(P + new Vector3(0, 0, offset));

        // Kochkurve ausfuehren
        //kochkurve(l, depth);


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
        Vector3[] fnA = faceNormals.ToArray();
        showNormals(fnA);
    }

    private void Update()
    {
        Vector3[] fnA = faceNormals.ToArray();
        for (int i = 0; i < meinMesh.vertices.Length; i++)
        {
            Vector3 norm = transform.TransformDirection(meinMesh.normals[i]);
            Vector3 vert = transform.TransformPoint(meinMesh.vertices[i]);
            Debug.DrawRay(vert, norm * 0.3f, Color.red);
        }
    }
    */



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

    void move(float length)
    {
        turtle.transform.Translate(length, 0, 0);
        Vector3 P = turtle.transform.position;
        verts.Add(P);
        verts.Add(P + new Vector3(0, 0, offset));
    }

    void turn(float deg)
    {
        turtle.transform.Rotate(0, 0, deg);
    }

    public void GenerateLightning()
    {
        turtle = new GameObject("Turtle");
        Vector3 P = turtle.transform.position;
        verts.Add(P);
        verts.Add(P + new Vector3(0, 0, offset));
        move(Random.Range(0.0f, 3.0f));
        turn(-90f);
        move(Random.Range(0.0f, 5.0f));
        //Vertice erstellen, nur jedes zweiteMal! function move aendern
        if (Random.value < 0.5f) {
            turn(90f);
            move(Random.Range(0.0f, 3.0f));
            turn(-90f);
        } else {
            turn(-90f);
            move(Random.Range(0.0f, 5.0f));
            turn(90f);
        }
    }

    void kochkurve(float length, int depth)
    {
        if (depth == 0)
        {
            move(length);
        }
        else
        {
            kochkurve(length / 3, depth - 1);
            turn(60f);
            kochkurve(length / 3, depth - 1);
            turn(-120f);
            kochkurve(length / 3, depth - 1);
            turn(60f);
            kochkurve(length / 3, depth - 1);
        }
    }



    // Debug Methods

    void showVerts()
    {
        Vector3[] vertArray = verts.ToArray();
        for (int i = 0; i < vertArray.Length; i++)
        {
            Debug.Log(vertArray[i]);
        }
    }

    void showFaces()
    {
        int[] faceArray = faces.ToArray();
        for (int i = 0; i < faceArray.Length; i++)
        {
            Debug.Log(faceArray[i]);
        }
    }
    void showNormals(Vector3[] normA)
    {
        for (int i = 0; i < normA.Length; i++)
        {
            Debug.Log(normA[i]);
        }
    }
}




