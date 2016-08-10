using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public GameObject player;

	public void Play ()
    {
        SceneManager.LoadScene("GameScene");
	}
    public static void Quit ()
    {
        Application.Quit();
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void HowToPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
