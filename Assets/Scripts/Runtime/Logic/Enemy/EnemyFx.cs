using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class EnemyFx : MonoBehaviour
    {
        [SerializeField] ParticleSystem bloodDebris;

        EnemyConfig config;

        [Inject]
        void Construct(IStaticDataService staticDataService)
        {
            config = staticDataService.EnemyConfig;
        }

        public void PlayBloodDebrisFX(Vector3 launchDirection)
        {
            ParticleSystem ps = Instantiate(bloodDebris, transform.position, Quaternion.identity);

            var velocity = ps.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;

            Vector2 speed = config.bloodDebrisSpeed;
            float spread = config.bloodDebrisSpread;

            velocity.x = new ParticleSystem.MinMaxCurve(
                (launchDirection.x - spread) * speed.x,
                (launchDirection.x + spread) * speed.y
            );

            velocity.y = new ParticleSystem.MinMaxCurve(
                (launchDirection.y - spread) * speed.x,
                (launchDirection.y + spread) * speed.y
            );

            velocity.z = new ParticleSystem.MinMaxCurve(
                (launchDirection.z - spread) * speed.x,
                (launchDirection.z + spread) * speed.y
            );

            ps.Play();
        }

    }
}