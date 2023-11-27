using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public void TeleportLevel1()
    {
        SceneController.Instance.LoadLevelOne();
    }

    public void TeleportLevel2()
    {
        SceneController.Instance.LoadLevelTwo();
    }

    public void TeleportLevel3()
    {
        SceneController.Instance.LoadLevelThree();
    }



}
