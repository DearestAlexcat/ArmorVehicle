using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.VisualScripting;
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
        ResetCancellationTokenSource();
        cts = new CancellationTokenSource();
        FlashCoroutine(cts.Token).Forget();
    }

    void ResetCancellationTokenSource()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }

    void OnDisable()
    {
        ResetCancellationTokenSource();
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
