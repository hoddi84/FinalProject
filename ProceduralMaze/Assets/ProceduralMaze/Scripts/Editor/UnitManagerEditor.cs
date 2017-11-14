using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitManager))]
public class UnitManagerEditor : Editor {

	SerializedProperty unitA;
    SerializedProperty unitB;
    SerializedProperty unitC;
    SerializedProperty unitD;
    SerializedProperty unitD1;
    SerializedProperty unitD2;
    SerializedProperty unitD3;
    SerializedProperty unitD4;
    SerializedProperty unitD5;
    SerializedProperty unitD6;
    SerializedProperty unitD7;
    SerializedProperty unitD8;
    SerializedProperty unitD9;
    SerializedProperty unitD10;
    SerializedProperty unitE;
    SerializedProperty unitE1;
    SerializedProperty unitE2;
    SerializedProperty unitE3;
    SerializedProperty unitE4;
    SerializedProperty unitE5;
    SerializedProperty unitE6;
    SerializedProperty unitE7;
    SerializedProperty unitE8;
    SerializedProperty unitE9;
    SerializedProperty unitE10;
    SerializedProperty unitE11;
    SerializedProperty unitF;
    SerializedProperty unitF1;
    SerializedProperty unitF2;
    SerializedProperty unitF3;
    SerializedProperty unitF4;
    SerializedProperty unitG;
    SerializedProperty unitG1;
    SerializedProperty unitG2;
    SerializedProperty unitG3;
    SerializedProperty unitG4;
    SerializedProperty unitG5;
    SerializedProperty unitG6;
    SerializedProperty unitH;
    SerializedProperty unitH1;
    SerializedProperty unitH2;
    SerializedProperty unitH3;
    SerializedProperty unitH4;
    SerializedProperty unitH5;
    SerializedProperty unitH6;
    SerializedProperty unitH7;
    SerializedProperty unitK;
    SerializedProperty unitK1;

	void OnEnable()
	{

		unitA = serializedObject.FindProperty("unitA");
		unitB = serializedObject.FindProperty("unitB");
		unitC = serializedObject.FindProperty("unitC");
		unitD = serializedObject.FindProperty("unitD");
		unitD1 = serializedObject.FindProperty("unitD1");
		unitD2 = serializedObject.FindProperty("unitD2");
		unitD3 = serializedObject.FindProperty("unitD3");
		unitD4 = serializedObject.FindProperty("unitD4");
		unitD5 = serializedObject.FindProperty("unitD5");
		unitD6 = serializedObject.FindProperty("unitD6");
		unitD7 = serializedObject.FindProperty("unitD7");
		unitD8 = serializedObject.FindProperty("unitD8");
		unitD9 = serializedObject.FindProperty("unitD9");
		unitD10 = serializedObject.FindProperty("unitD10");
		unitE = serializedObject.FindProperty("unitE");
		unitE1 = serializedObject.FindProperty("unitE1");
		unitE2 = serializedObject.FindProperty("unitE2");
		unitE3 = serializedObject.FindProperty("unitE3");
		unitE4 = serializedObject.FindProperty("unitE4");
		unitE5 = serializedObject.FindProperty("unitE5");
		unitE6 = serializedObject.FindProperty("unitE6");
		unitE7 = serializedObject.FindProperty("unitE7");
		unitE8 = serializedObject.FindProperty("unitE8");
		unitE9 = serializedObject.FindProperty("unitE9");
		unitE10 = serializedObject.FindProperty("unitE10");
		unitE11 = serializedObject.FindProperty("unitE11");
		unitF = serializedObject.FindProperty("unitF");
		unitF1 = serializedObject.FindProperty("unitF1");
		unitF2 = serializedObject.FindProperty("unitF2");
		unitF3 = serializedObject.FindProperty("unitF3");
		unitF4 = serializedObject.FindProperty("unitF4");
		unitG = serializedObject.FindProperty("unitG");
		unitG1 = serializedObject.FindProperty("unitG1");
		unitG2 = serializedObject.FindProperty("unitG2");
		unitG3 = serializedObject.FindProperty("unitG3");
		unitG4 = serializedObject.FindProperty("unitG4");
		unitG5 = serializedObject.FindProperty("unitG5");
		unitG6 = serializedObject.FindProperty("unitG6");
		unitH = serializedObject.FindProperty("unitH");
		unitH1 = serializedObject.FindProperty("unitH1");
		unitH2 = serializedObject.FindProperty("unitH2");
		unitH3 = serializedObject.FindProperty("unitH3");
		unitH4 = serializedObject.FindProperty("unitH4");
		unitH5 = serializedObject.FindProperty("unitH5");
		unitH6 = serializedObject.FindProperty("unitH6");
		unitH7 = serializedObject.FindProperty("unitH7");
		unitK = serializedObject.FindProperty("unitK");
		unitK1 = serializedObject.FindProperty("unitK1");
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();

		serializedObject.Update();

		Color defaultColor = GUI.color;

		CreateGUIUnits("Unit A", unitA, defaultColor);
		CreateGUIUnits("Unit B", unitB, defaultColor);
		CreateGUIUnits("Unit C", unitC, defaultColor);

		CreateGUIUnits("Unit D", unitD, defaultColor);
		CreateGUIUnits("Unit D1", unitD1, defaultColor);
		CreateGUIUnits("Unit D2", unitD2, defaultColor);
		CreateGUIUnits("Unit D3", unitD3, defaultColor);
		CreateGUIUnits("Unit D4", unitD4, defaultColor);
		CreateGUIUnits("Unit D5", unitD5, defaultColor);
		CreateGUIUnits("Unit D6", unitD6, defaultColor);
		CreateGUIUnits("Unit D7", unitD7, defaultColor);
		CreateGUIUnits("Unit D8", unitD8, defaultColor);
		CreateGUIUnits("Unit D9", unitD9, defaultColor);
		CreateGUIUnits("Unit D10", unitD10, defaultColor);

		CreateGUIUnits("Unit E", unitE, defaultColor);
		CreateGUIUnits("Unit E1", unitE1, defaultColor);
		CreateGUIUnits("Unit E2", unitE2, defaultColor);
		CreateGUIUnits("Unit E3", unitE3, defaultColor);
		CreateGUIUnits("Unit E4", unitE4, defaultColor);
		CreateGUIUnits("Unit E5", unitE5, defaultColor);
		CreateGUIUnits("Unit E6", unitE6, defaultColor);
		CreateGUIUnits("Unit E7", unitE7, defaultColor);
		CreateGUIUnits("Unit E8", unitE8, defaultColor);
		CreateGUIUnits("Unit E9", unitE9, defaultColor);
		CreateGUIUnits("Unit E10", unitE10, defaultColor);
		CreateGUIUnits("Unit E11", unitE11, defaultColor);

		CreateGUIUnits("Unit F", unitF, defaultColor);
		CreateGUIUnits("Unit F1", unitF1, defaultColor);
		CreateGUIUnits("Unit F2", unitF2, defaultColor);
		CreateGUIUnits("Unit F3", unitF3, defaultColor);
		CreateGUIUnits("Unit F4", unitF4, defaultColor);

		CreateGUIUnits("Unit G", unitG, defaultColor);
		CreateGUIUnits("Unit G1", unitG1, defaultColor);
		CreateGUIUnits("Unit G2", unitG2, defaultColor);
		CreateGUIUnits("Unit G3", unitG3, defaultColor);
		CreateGUIUnits("Unit G4", unitG4, defaultColor);
		CreateGUIUnits("Unit G5", unitG5, defaultColor);
		CreateGUIUnits("Unit G6", unitG6, defaultColor);

		CreateGUIUnits("Unit H", unitH, defaultColor);
		CreateGUIUnits("Unit H1", unitH1, defaultColor);
		CreateGUIUnits("Unit H2", unitH2, defaultColor);
		CreateGUIUnits("Unit H3", unitH3, defaultColor);
		CreateGUIUnits("Unit H4", unitH4, defaultColor);
		CreateGUIUnits("Unit H5", unitH5, defaultColor);
		CreateGUIUnits("Unit H6", unitH6, defaultColor);
		CreateGUIUnits("Unit H7", unitH7, defaultColor);

		CreateGUIUnits("Unit K", unitK, defaultColor);
		CreateGUIUnits("Unit K1", unitK1, defaultColor);
	}

	private void CreateGUIUnits(string label, SerializedProperty unit, Color defaultColor)
	{
		GUI.color = Color.grey;
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		GUI.color = defaultColor;

		for (int i = 0; i < unit.arraySize; i++)
		{
			EditorGUILayout.PropertyField(unit.GetArrayElementAtIndex(i), new GUIContent(label + " Var_" + i));
		}

		EditorGUILayout.EndVertical();
	}
}
