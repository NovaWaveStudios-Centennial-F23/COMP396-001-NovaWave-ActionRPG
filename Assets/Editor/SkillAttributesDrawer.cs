using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SkillSelector))]
public class SkillSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Find all classes that derive from Skill
            var skillTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Skill)));

            // Create an array of class names
            var skillTypeNames = skillTypes.Select(type => type.Name).ToArray();

            //Adding player (the default option)
            var allTypeNames = new string[skillTypeNames.Length + 1];
            allTypeNames[0] = "Player";
            Array.Copy(skillTypeNames, 0, allTypeNames, 1, skillTypeNames.Length);

            // Get the currently selected skill name
            var currentSkillName = property.stringValue;

            // Find the index of the currently selected skill
            var selectedIndex = Array.IndexOf(allTypeNames, currentSkillName);

            // Show a dropdown with the available skills
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, allTypeNames);

            // Update the selected skill based on the index
            if (selectedIndex >= 0 && selectedIndex < allTypeNames.Length)
            {
                property.stringValue = allTypeNames[selectedIndex];
            }

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use SkillSelector with strings.");
        }
    }
}
