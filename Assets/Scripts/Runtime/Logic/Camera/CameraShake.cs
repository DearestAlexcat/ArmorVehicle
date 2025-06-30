using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

namespace ArmorVehicle
{
    public class CameraShake : MonoBehaviour
    {
        public float shakeTime = 0.15f;
        public float shakeAmount = 1.1f;
        public float shakeSpeed = 3f;
        public Vector2 shakeInterval = new Vector2(0.3f, 0.7f);

        CancellationTokenSource cts;
        Camera cam;

        void Start()
        {
            cam = Camera.main;
        }

        public void StartShake()
        {
            cts = new CancellationTokenSource();
            Shake(cts.Token).Forget();
        }

        public void StopShake()
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }

        void OnDestroy()
        {
            StopShake();
        }

        async UniTask Shake(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(shakeInterval.x, shakeInterval.y)), cancellationToken: cancellationToken);

                    var origPosition = cam.transform.localPosition;
                    float elapsedTime = 0f;
                    var thisTransform = cam.transform;

                    while (!cancellationToken.IsCancellationRequested && elapsedTime < shakeTime)
                    {
                        //var randomPoint = origPosition + UnityEngine.Random.insideUnitSphere * shakeAmount;
                        var randomPoint = origPosition + RandomService.InsideUnitSphereGaussian() * shakeAmount;

                        thisTransform.localPosition = ExpDecay(thisTransform.localPosition, randomPoint, shakeSpeed, Time.deltaTime);
                        await UniTask.NextFrame();

                        elapsedTime += Time.deltaTime;
                    }

                    while (!cancellationToken.IsCancellationRequested && (thisTransform.localPosition - origPosition).sqrMagnitude > 0.0001f)
                    {
                        thisTransform.localPosition = ExpDecay(thisTransform.localPosition, origPosition, shakeSpeed, Time.deltaTime);
                        await UniTask.NextFrame();
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        Vector3 ExpDecay(Vector3 a, Vector3 b, float decay, float dt)
        {
            return b + (a - b) * Mathf.Exp(-decay * dt);
        }
    }
}