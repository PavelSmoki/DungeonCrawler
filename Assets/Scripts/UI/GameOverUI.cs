using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _victoryScreen;
        public async UniTaskVoid ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            Time.timeScale = 0;
        }
        
        public async UniTaskVoid ShowWinScreen()
        {
            _victoryScreen.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            Time.timeScale = 0;
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}