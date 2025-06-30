using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace ArmorVehicle
{
    public interface IAssetProvider
    {
        UniTask InitializeAsync();
        UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        UniTask<List<string>> GetAssetPrimaryKeysByLabel<TAsset>(string label);
        UniTask<List<string>> GetAssetPrimaryKeysByLabels<TAsset>(IList<string> labels, Addressables.MergeMode mergeMode);
        UniTask<List<string>> GetAssetPrimaryKeysByLabels(IList<string> labels, Addressables.MergeMode mergeMode, Type type = null);
        UniTask<List<string>> GetAssetPrimaryKeysByLabel(string label, Type type = null);
        UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        UniTask PreloadAssetByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        void Cleanup();
    }
}

