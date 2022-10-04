using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartLevelButton : MonoBehaviour
{
    public void StartGameLevel()
    {
        SceneManager.LoadScene("1-MainScene");
    }
}
