/*** Created by Han Bi
 * Used for UI, details about the class
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Class Description", menuName = "Scriptable Object/Class Description")]
public class ClassDescriptionSO : ScriptableObject
{
    public string className;

    [TextArea]
    public string classDescription;

    public bool isAvaliable;
}
