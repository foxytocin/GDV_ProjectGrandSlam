using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class LightningRightChildScript : MonoBehaviour
{
    GameObject turtleLC;
    

    public int depth = 1;
    //public float lightningDepth;

    private Mesh meinMesh;
    private List<Vector3> vertsLC;
    private List<Vector3> faceNormalsLC;
    private List<Vector3> vertexNormalsLC;
    private List<int> faces;
    //private int offset = 1;

    private float maxXStep = 1.5f;
    private float maxYStep = 1.2f;

    Material mat;
    float emission;

    public void init()
    {
        //GenerateLeftChildLightning(transform.position);        
        faces = new List<int>();
        vertsLC = new List<Vector3>();
        faceNormalsLC = new List<Vector3>();
        vertexNormalsLC = new List<Vector3>();
    }

    public void makeMesh()
    {
        GetComponent<MeshFilter>().mesh = meinMesh = new Mesh();        //Gleiche Instanz Zuweisung
        mat = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // Mesh Vertices hinzufuegen
        meinMesh.vertices = vertsLC.ToArray();

        // Mesh Faces hinzufuegen     
        meinMesh.triangles = makeFaces(meinMesh.vertices); ;

        // Vertices Normals aus Facenormals berechnen 
        meinMesh.normals = setVertexNormals(faceNormalsLC);

        emission = 1f;
        Color finalColor = Color.white * Mathf.LinearToGammaSpace(emission * 1.5f);
        mat.SetColor("_EmissionColor", finalColor);
        mat.EnableKeyword("_EMISSION");

        Destroy(gameObject, 0.5f);
        Destroy(turtleLC, 0.5f);
    }

    int[] makeFaces(Vector3[] vertArr)
    {
        for (var i = 0; i < vertArr.Length - 3; i += 2)
        {
            faces.Add(i);
            faces.Add(i + 1);
            faces.Add(i + 2);
            Vector3 norm = calcNormal(vertArr[i], vertArr[i + 1], vertArr[i + 2]);
            faceNormalsLC.Add(norm);

            faces.Add(i + 1);
            faces.Add(i + 3);
            faces.Add(i + 2);
        }
        return faces.ToArray();
    }

    Vector3[] setVertexNormals(List<Vector3> fNorms)
    {
        Vector3[] fNormalsArray = fNorms.ToArray();
        vertexNormalsLC.Add(fNormalsArray[0]);
        vertexNormalsLC.Add(fNormalsArray[0]);

        for (int i = 1; i < fNormalsArray.Length; i++)
        {
            Vector3 vN = fNormalsArray[i] + fNormalsArray[i - 1];
            vN = vN.normalized;
            vertexNormalsLC.Add(vN);
            vertexNormalsLC.Add(vN);
        }
        vertexNormalsLC.Add(fNormalsArray[fNormalsArray.Length - 1]);
        vertexNormalsLC.Add(fNormalsArray[fNormalsArray.Length - 1]);

        return vertexNormalsLC.ToArray();
    }

    Vector3 calcNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 ab = b - a;
        Vector3 ac = c - a;

        return Vector3.Cross(ab, ac).normalized;
    }


    // Move Turtle
    void move(float length, int secondStep, float offset)
    {
        turtleLC.transform.Translate(length, 0, 0);
        Vector3 P = turtleLC.transform.position;
        if (secondStep == 1)
        {
            vertsLC.Add(P);
            vertsLC.Add(P + new Vector3(0, offset, 0));
        }
    }

    void turn(float deg)
    {
        turtleLC.transform.Rotate(0, 0, deg);
    }

    //Rekursiv?
    public void GenerateRightChildLightning(Vector3 pos, float lightningDepth)
    {

        // GameObject initialisieren
        turtleLC = new GameObject("TurtleLC");
        turtleLC.transform.Translate(pos);
        Vector3 P = turtleLC.transform.position;
        turn(90f);
        vertsLC.Add(P);
        vertsLC.Add(P + new Vector3(0, lightningDepth/20f, 0));

        move(Random.Range(0.0f, maxXStep), 0, lightningDepth / 20f);
        turn(-90f);
        move(Random.Range(0.0f, maxYStep), 1, lightningDepth / 20f);

        for (int i = Mathf.RoundToInt(lightningDepth * (lightningDepth / 20f)); i > 0; i--)
        {
            float off = i / 20f;
            if (Random.value < 0.1f)
            {
                turn(90f);
                move(Random.Range(0.0f, maxXStep), 0, off);
                turn(-90f);
                move(Random.Range(0.0f, maxYStep), 1, off);
            }
            else
            {
                turn(-90f);
                move(Random.Range(0.0f, maxXStep), 0, off);
                turn(90f);
                move(Random.Range(0.0f, maxYStep), 1, off);
            }
        }
        makeMesh();
    }
}




