using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Q3
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;

        // Outlets
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject levelMenu;

        private void Awake()
        {
            instance = this;
            Hide();
        }

        private void SwitchMenu(GameObject someMenu)
        {
            // Turn off all menus
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelMenu.SetActive(false);

            // Turn on requested menu
            someMenu.SetActive(true);
        }

        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }

        public void ShowOptionsMenu()
        {
            SwitchMenu(optionsMenu);
        }

        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Show()
        {
            ShowMainMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if(PlayerController.instance != null)
            {
                PlayerController.instance.isPaused = false;
            }           
        }

        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerController.instance.score = 0;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("Q4");
        }
    }
}
