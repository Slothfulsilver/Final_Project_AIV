using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Instrucciones()
        {
            SceneManager.LoadScene(1);
        }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detener el juego en Unity
        #else
            Application.Quit(); // Cerrar el juego en la build
        #endif
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
}
