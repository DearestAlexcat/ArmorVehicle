using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class DamageFlashEffect : MonoBehaviour
{
    [SerializeField] Renderer targetRenderer;
    [SerializeField] float flashDuration = 0.1f;

    Material originalMaterial;
    [SerializeField] Material whiteFlashMaterial;

    void Start()
    {
        originalMaterial = targetRenderer.material;
    }

    CancellationTokenSource cts;

    public void FlashWhite()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new CancellationTokenSource();
        FlashCoroutine(cts.Token).Forget();
    }

    async UniTask FlashCoroutine(CancellationToken cancellationToken)
    {
        try
        {
            targetRenderer.material = whiteFlashMaterial;
            await UniTask.Delay(TimeSpan.FromSeconds(flashDuration), cancellationToken: cancellationToken);
            targetRenderer.material = originalMaterial;

            await UniTask.Delay(TimeSpan.FromSeconds(flashDuration), cancellationToken: cancellationToken);

            targetRenderer.material = whiteFlashMaterial;
            await UniTask.Delay(TimeSpan.FromSeconds(flashDuration), cancellationToken: cancellationToken);
            targetRenderer.material = originalMaterial;
        }
        catch (OperationCanceledException)
        {
        }
    }
}
