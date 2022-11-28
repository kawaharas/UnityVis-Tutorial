using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Action<T>
using System.IO;
using UnityEngine.Networking; // UnityWebRequest

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Triangle7 : MonoBehaviour
{
	public string filename = "triangle.txt";
	public List<Vector3> vertices;
	public List<Color>   colors;
	List<int>     indices;
	Mesh          mesh;

    // Start is called before the first frame update
    void Start()
    {
		vertices = new List<Vector3>();
		colors   = new List<Color>();
		indices  = new List<int>();
		mesh     = new Mesh();

		var filter = GetComponent<MeshFilter>();
		filter.mesh = mesh;

		StartCoroutine(ReadText(filename, ParseText));
    }

    // Update is called once per frame
    void Update()
    {
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.colors = colors.ToArray();
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);
		mesh.RecalculateNormals();
    }

	IEnumerator ReadText(string filename, Action<string> callback = null)
	{
		var path = Path.Combine(Application.streamingAssetsPath, filename);

#if UNITY_ANDROID
		var www = UnityWebRequest.Get(path);
		yield return www.SendWebRequest();

#if UNITY_2020_2_OR_NEWER
		if (www.result == UnityWebRequest.Result.ProtocolError ||
			www.result == UnityWebRequest.Result.ConnectionError)
#else
		if (www.isHttpError || www.isNetworkError)
#endif
		{
			Debug.Log(www.error);
		}
		else
		{
			callback(www.downloadHandler.text);
		}
#else
		using var stream = new StreamReader(path);

		var text = stream.ReadToEnd();
		yield return null;

		callback(text);
#endif
	}

	void ParseText(string inputdata)
	{
		using var reader = new StringReader(inputdata);

		int vertID = 0; // current index of vertices
		var v = new float[6]; // for input values
		while (reader.Peek() > -1)
		{
			var line = reader.ReadLine();
			var stringList = line.Split(',');
			for (int i = 0; i < 6; i++)
			{
				v[i] = float.Parse(stringList[i]);
			}
			vertices.Add(new Vector3(v[0], v[1], v[2]));
			colors.Add(new Color(v[3], v[4], v[5]));
			indices.Add(vertID);
			vertID++;
		}
	}
}
