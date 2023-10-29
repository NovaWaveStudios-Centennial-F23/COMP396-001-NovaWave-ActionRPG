/* Created by: Han Bi
 * base class for handling displaying various information
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolTipHandler : MonoBehaviour
{
    public abstract void DisplayDetails(SkillTreeNode data);

}
