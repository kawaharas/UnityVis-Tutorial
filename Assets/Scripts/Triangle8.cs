using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Action<T>
using System.IO;
using UnityEngine.Networking; // UnityWebRequest

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Triangle8 : MonoBehaviour
{
	public string filename = "triangle.bin";
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

		StartCoroutine(ReadBinary(filename, ParseBinary));
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

	IEnumerator ReadBinary(string filename, Action<byte[]> callback = null)
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
			callback(www.downloadHandler.data);
		}
#else
        using var stream = new FileStream(path, FileMode.Open);
        using var reader = new BinaryReader(stream);

		var length = (int)stream.Length;
		var data = new byte[length];
		reader.Read(data, 0, length);
		yield return null;

		callback(data);
#endif
	}

	void ParseBinary(byte[] inputdata)
	{
		using var stream = new MemoryStream(inputdata);
		using var reader = new BinaryReader(stream);

		int vertID = 0; // current index of vertices
		var v = new float[6]; // input values
		while (reader.BaseStream.Position != reader.BaseStream.Length)
		{
			for (int i = 0; i < 6; i++)
			{
				v[i] = reader.ReadSingle();
			}
			vertices.Add(new Vector3(v[0], v[1], v[2]));
			colors.Add(new Color(v[3], v[4], v[5]));
			indices.Add(vertID);
			vertID++;
		}
	}
}
