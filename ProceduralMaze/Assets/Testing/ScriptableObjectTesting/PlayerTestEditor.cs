using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerTest))]
[CanEditMultipleObjects]
public class PlayerTestEditor : Editor {

	SerializedProperty damageProp;
	SerializedProperty armorProp;
	SerializedProperty gunProp;

	SerializedProperty gun2Prop;

	void OnEnable()
	{
		damageProp = serializedObject.FindProperty("damage");
		armorProp = serializedObject.FindProperty("armor");
		gunProp = serializedObject.FindProperty("gun");
		gun2Prop = serializedObject.FindProperty("gun2");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		serializedObject.Update();

		Color defaultColor = GUI.color;

		GUI.color = Color.grey;
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		GUI.color = defaultColor;

		EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));

		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();

		// New array

		GUI.color = Color.grey;
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		GUI.color = defaultColor;

		EditorGUILayout.LabelField("Unit K", EditorStyles.boldLabel);

		for (int i = 0; i < gunProp.arraySize; i++)
		{
			EditorGUILayout.PropertyField(gunProp.GetArrayElementAtIndex(i), new GUIContent("UnitK_" + i));
		}

		EditorGUILayout.EndVertical();

	}
}
