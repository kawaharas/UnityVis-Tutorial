using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle1 : MonoBehaviour
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
	}

	// Update is called once per frame
	void Update()
	{
	}
}
