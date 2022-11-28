using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle2 : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// vertices
		var vertices = new List<Vector3>();
		var v0 = new Vector3(-1, 0, 0);
		var v1 = new Vector3( 0, 2, 0);
		var v2 = new Vector3( 1, 0, 0);
		vertices.Add(v0);
		vertices.Add(v1);
		vertices.Add(v2);

		// indices
		var indices = new List<int>();
		indices.Add(0);
		indices.Add(1);
		indices.Add(2);

		// set vertices and indices to Mesh class
		var mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);
		mesh.RecalculateNormals();

		// set Mesh class data to MeshFilter component
		var filter = GetComponent<MeshFilter>();
		if (filter != null) filter.mesh = mesh;
	}

	// Update is called once per frame
	void Update()
	{
	}
}
