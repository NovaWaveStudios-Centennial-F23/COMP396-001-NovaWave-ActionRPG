using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetworkingTest
{

    public class GameListScene : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
