using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]

public class WallScript : MonoBehaviour {

		private Mesh wallPiece;
		List<Vector3> vtx = new List<Vector3>();
		List<int> tris = new List<int>();
		Vector2[] uvs = new Vector2[4];

		public float heightWall = 1f;

	// Use this for initialization
	void Start ()
	{
		wallPiece = new Mesh();
		GetComponent<MeshFilter>().mesh = wallPiece;
		wallPiece.name = "WallPiece";
		createWallPiece(heightWall);
		makeFace();
		makeUVS();
		drawWallPiece();
	}

	void createWallPiece(float height)
	{
		vtx.Add(transform.position);
		vtx.Add(transform.position + new Vector3(1f, 0, 0));
		// vtx.Add(transform.position + new Vector3(1f, 0, 1f));
		// vtx.Add(transform.position + new Vector3(0, 0, 1f));
		transform.Translate(0 , height, 0);
		vtx.Add(transform.position);
		vtx.Add(transform.position + new Vector3(1f, 0, 0));
		// vtx.Add(transform.position + new Vector3(1f, 0, 1f));
		// vtx.Add(transform.position + new Vector3(0, 0, 1f));
	}

	void drawWallPiece()
	{
		wallPiece.vertices = vtx.ToArray();
		wallPiece.triangles = tris.ToArray();
		wallPiece.uv = uvs;
		//wallPiece.normals = verticesNormals.ToArray();
		wallPiece.RecalculateNormals();

		MeshRenderer rend = GetComponent<MeshRenderer>();
		rend.material.SetColor("_Color", Color.grey);
	}

	void makeFace()
	{
		tris.Add(0);
		tris.Add(2);
		tris.Add(3);

		tris.Add(0);
		tris.Add(3);
		tris.Add(1);

		// tris.Add(1);
		// tris.Add(5);
		// tris.Add(6);
		//
		// tris.Add(1);
		// tris.Add(6);
		// tris.Add(2);
		//
		// tris.Add(2);
		// tris.Add(6);
		// tris.Add(7);
		//
		// tris.Add(2);
		// tris.Add(7);
		// tris.Add(3);
		//
		// tris.Add(3);
		// tris.Add(7);
		// tris.Add(4);
		//
		// tris.Add(3);
		// tris.Add(4);
		// tris.Add(0);
		//
		// tris.Add(5);
		// tris.Add(4);
		// tris.Add(7);
		//
		// tris.Add(5);
		// tris.Add(7);
		// tris.Add(6);
		//
		// tris.Add(2);
		// tris.Add(3);
		// tris.Add(0);
		//
		// tris.Add(1);
		// tris.Add(2);
		// tris.Add(0);
	}


	void makeUVS() {
   uvs[0] = new Vector2(0,0); //top-left
   uvs[1] = new Vector2(0,1); //top-right
   uvs[2] = new Vector2(1,0); //bottom-left
   uvs[3] = new Vector2(1,1); //bottom-right
	}

	// void makeFaceNormals()
	// {
	// 	Vector3 norm = new Vector3();
	// 	norm = GetNormal(vtx[i], vtx[i+4], vtx[i+5]);
	// 	faceNormals.Add(norm);
	// 	faceNormals.Add(norm);
	//
	// 	// verticesNormals.Add(norm);
	// 	// verticesNormals.Add(norm);
	// 	// verticesNormals.Add(norm);
	// 	// verticesNormals.Add(norm);
	// }

	// //Berechnet FaceNormals
	// Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
	// {
  //   Vector3 side1 = b - a;
  //   Vector3 side2 = c - a;
  //   return Vector3.Cross(side1, side2).normalized;
  // }
}
