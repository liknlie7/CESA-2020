//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// SonarFx改造
// 2020/06/03
// 山口寛雅
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SonarFx)), CanEditMultipleObjects]
public class SonarFxEditor : Editor
{
    SerializedProperty propShader;
    SerializedProperty propMode;
    //SerializedProperty propOrigin;
    //SerializedProperty propDirection;
    SerializedProperty propBaseColor;
    SerializedProperty propWaveColor;
    SerializedProperty propWaveAmplitude;
    SerializedProperty propWaveExponent;
    SerializedProperty propWaveInterval;
    SerializedProperty propWaveSpeed;
    SerializedProperty propAddColor;
    SerializedProperty propWaveRadius;
    //SerializedProperty propWaves;

    void OnEnable()
    {
        propShader        = serializedObject.FindProperty("shader");
        propMode          = serializedObject.FindProperty("_mode");
        //propOrigin        = serializedObject.FindProperty("_origin");
        //propDirection     = serializedObject.FindProperty("_direction");
        propBaseColor     = serializedObject.FindProperty("_baseColor");
        propWaveColor     = serializedObject.FindProperty("_waveColor");
        propWaveAmplitude = serializedObject.FindProperty("_waveAmplitude");
        propWaveExponent  = serializedObject.FindProperty("_waveExponent");
        propWaveInterval  = serializedObject.FindProperty("_waveInterval");
        propWaveSpeed     = serializedObject.FindProperty("_waveSpeed");
        propAddColor      = serializedObject.FindProperty("_addColor");
        propWaveRadius    = serializedObject.FindProperty("_waveRadius");
        //propWaves         = serializedObject.FindProperty("_sonarWaves");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propShader);
        EditorGUILayout.PropertyField(propMode);

        EditorGUI.indentLevel++;

        //if (propMode.hasMultipleDifferentValues ||
        //    propMode.enumValueIndex == (int)SonarFx.SonarMode.Directional)
        //    EditorGUILayout.PropertyField(propDirection);

        //if (propMode.hasMultipleDifferentValues ||
        //    propMode.enumValueIndex == (int)SonarFx.SonarMode.Spherical)
        //    EditorGUILayout.PropertyField(propOrigin);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Base Color");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propBaseColor, new GUIContent("Albedo"));
        EditorGUILayout.PropertyField(propAddColor, new GUIContent("Emission"));
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Wave Parameters");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propWaveColor, new GUIContent("Color"));
        EditorGUILayout.PropertyField(propWaveAmplitude, new GUIContent("まぶしさ(x:Amplitude)"));
        EditorGUILayout.PropertyField(propWaveExponent, new GUIContent("内側の塗り(y:Exponent)"));
        EditorGUILayout.PropertyField(propWaveInterval, new GUIContent("ソナーのぼかし(z:Interval)"));
        EditorGUILayout.PropertyField(propWaveSpeed, new GUIContent("Speed(w)"));
        EditorGUILayout.PropertyField(propWaveRadius, new GUIContent("範囲のぼかし(Radius)"));
        EditorGUI.indentLevel--;

        //EditorGUILayout.LabelField("Timings");
        //EditorGUI.indentLevel++;
        //if (GUILayout.Button("Reset Timing"))
        //{
        //    ((SonarFx)target).Pulse(Vector3.zero);
        //}
        //EditorGUILayout.PropertyField(propWaves, new GUIContent("Wave Offsets"), true);
        //EditorGUI.indentLevel--;
        
        serializedObject.ApplyModifiedProperties();
    }
}
