using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class PlayerUIManager : Singleton<PlayerUIManager>
    {
        [HideInInspector] public PlayerController Player;
        [HideInInspector] public PlayerHUD HUD;
        [HideInInspector] public PlayerMenuUI MenuUI;

        protected override void Awake()
        {
            base.Awake();
            HUD = GetComponentInChildren<PlayerHUD>();
            MenuUI = GetComponentInChildren<PlayerMenuUI>();
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == 0)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }

        public void ShowUI()
        {
            HUD.ShowHud();
        }

        public void HideUI()
        {
            HUD.HideHUD();
        }
    }
}