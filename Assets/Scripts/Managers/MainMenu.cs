using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
    public class MainMenu : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}