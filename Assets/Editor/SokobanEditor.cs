using System.Collections;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SokobanEditorSetup))]
public class SokobanEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		SokobanEditorSetup t = (SokobanEditorSetup)target;
		GUILayout.Label("Control:", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Create / Update Mesh")) t.Create();
		if (GUILayout.Button("Clear map")) t.ClearMap();
		GUILayout.EndHorizontal();
	}

	void OnSceneGUI()
	{
		SokobanEditorSetup t = (SokobanEditorSetup)target;

		HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

		if (Event.current.button == 0 && Event.current.type == EventType.MouseDown || Event.current.button == 0 && Event.current.type == EventType.MouseDrag)
		{
			RaycastHit2D hit = Physics2D.Raycast(SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(new Vector2(Event.current.mousePosition.x,
				SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y)), Vector2.zero);

			if (hit.collider != null)
			{
				if (!Event.current.shift)
				{
					if (hit.collider.name.CompareTo(t.prefabsNames[t.index]) != 0) t.SetPrefab(hit.transform.gameObject);
				}
				else
				{
					if (hit.collider.tag.CompareTo("EditorOnly") != 0) DestroyImmediate(hit.transform.gameObject);
				}
			}
		}

		Handles.BeginGUI();
		GUILayout.BeginArea(new Rect(t.position.x, t.position.y, t.width, t.height), EditorStyles.helpBox);

		if (GUILayout.Button("Load / Update Prefabs")) t.LoadResources();

		GUILayout.TextArea("Help: set selected object with LMB, remove object with Shift + LMB.");

		GUILayout.BeginHorizontal();
		GUILayout.TextField("Prefab selection:");
		t.index = EditorGUILayout.Popup(t.index, t.prefabsNames);
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
		Handles.EndGUI();
	}
}
