#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Triangle6))]
public class Triangle6Editor : Editor
{
	public override void OnInspectorGUI()
	{
		var triangle = target as Triangle6;
		triangle.v0x = EditorGUILayout.Slider("v0x:", triangle.v0x, -3f, 0);
	}
}
#endif
