using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class MenuController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Play()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}