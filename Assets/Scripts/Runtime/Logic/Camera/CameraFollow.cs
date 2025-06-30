using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform player;

        IStaticDataService staticDataService;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public void SetOrigin(Transform origin)
        {
            transform.position = origin.position;
            transform.LookAt(player.position);
        }

        void LateUpdate()
        {
            var config = staticDataService.CarConfig;
            Vector3 target = player.position + config.lookOffset;

            Vector3 targetPosition = target + new Vector3(0f, config.height, config.zOffset);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, config.smoothSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}