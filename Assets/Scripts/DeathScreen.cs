using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    
    public void MainMenu()
    {
        SceneTransition.SwitchScene(0);
    }

    public void Restart()
    {
        SceneTransition.SwitchScene(1);
        Time.timeScale = 1;
    }
}
