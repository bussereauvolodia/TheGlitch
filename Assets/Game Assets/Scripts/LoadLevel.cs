using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadLevels(int level)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(level);
    }
}
