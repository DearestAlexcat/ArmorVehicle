using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ArmorVehicle
{
    public class SceneNameSwitchButton : MonoBehaviour
    {
        [SerializeField] SceneName sceneName;
        [SerializeField] Button button;

        GameStateMachine gameStateMachine;

        [Inject]
        void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        void OnClick()
        {
            switch (sceneName)
            {
                case SceneName.Level: 
                    gameStateMachine.Enter<LoadSceneState, string>(sceneName.ToString()).Forget(); 
                    break;
            }
        }
    }
}