using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits: MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Title_screen");
        }
    }
}