using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		if (GUILayout.Button("Regenerate")) {
			((Level) target).GenerateLevel();
		}
	}
}
