using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle
{
    public class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] Button startGameButton;

        public event Action OnStartGame;

        void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartGame);
        }

        public void Display(bool value)
        {
            gameObject.SetActive(value);
        }

        void StartGame()
        {
            OnStartGame?.Invoke();
        }
    }
}