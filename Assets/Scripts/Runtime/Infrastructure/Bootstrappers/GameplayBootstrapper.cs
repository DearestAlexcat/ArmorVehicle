using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        GameplayStateMachine gameplayStateMachine;

        [Inject]
        void Construct(GameplayStateMachine gameplayStateMachine)
        {
            this.gameplayStateMachine = gameplayStateMachine;
        }

        void Start()
        {
            gameplayStateMachine.Enter<GameplayBootstrapState>().Forget();
        }

        void Update()
        {
            gameplayStateMachine.UpdateState();
        }
    }
}