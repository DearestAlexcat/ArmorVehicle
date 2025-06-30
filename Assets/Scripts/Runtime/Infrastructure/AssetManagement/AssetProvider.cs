using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ArmorVehicle
{
    public class AssetProvider : IAssetProvider
    {
        readonly Dictionary<string, AsyncOperationHandle> assetRequests = new();

        public async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync().ToUniTask();
        }

        public async UniTask<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            AsyncOperationHandle handle;

            if (!assetRequests.TryGetValue(key, out handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();

            return handle.Result as TAsset;
        }

        public async UniTask<List<string>> GetAssetPrimaryKeysByLabel<TAsset>(string label)
        {
            return await GetAssetPrimaryKeysByLabel(label, typeof(TAsset));
        }

        public async UniTask<List<string>> GetAssetPrimaryKeysByLabels<TAsset>(IList<string> labels, Addressables.MergeMode mergeMode)
        {
            return await GetAssetPrimaryKeysByLabels(labels, mergeMode, typeof(TAsset));
        }

        public async UniTask<List<string>> GetAssetPrimaryKeysByLabel(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);
            var metadatas = await operationHandle.ToUniTask();

            List<string> primaryKeys = new(metadatas.Count);
            foreach (var metadata in metadatas)
                primaryKeys.Add(metadata.PrimaryKey);

            Addressables.Release(operationHandle);
            return primaryKeys;
        }

        public async UniTask<List<string>> GetAssetPrimaryKeysByLabels(IList<string> labels, Addressables.MergeMode mergeMode, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(labels, mergeMode, type);
            var metadatas = await operationHandle.ToUniTask();

            List<string> primaryKeys = new(metadatas.Count);
            foreach (var metadata in metadatas)
                primaryKeys.Add(metadata.PrimaryKey);

            Addressables.Release(operationHandle);
            return primaryKeys;
        }

        public async UniTask PreloadAssetByLabel(string label)
        {
            var primaryKeys = await GetAssetPrimaryKeysByLabel(label);
            await LoadAll<object>(primaryKeys);
        }

        public async UniTask<TAsset[]> LoadAll<TAsset>(List<string> primaryKeys) where TAsset : class
        {
            List<UniTask<TAsset>> tasks = new();

            foreach (var primaryKey in primaryKeys)
                tasks.Add(Load<TAsset>(primaryKey));

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
        {
            return await Load<TAsset>(assetReference.AssetGUID);
        }


        public async UniTask ReleaseAssetsByLabel(string label)
        {
            var primaryKeys = await GetAssetPrimaryKeysByLabel(label);

            foreach (var primaryKey in primaryKeys)
            {
                if (assetRequests.TryGetValue(primaryKey, out var handle))
                {
                    Addressables.Release(handle);
                    assetRequests.Remove(primaryKey);
                }
            }
        }

        public void Cleanup()
        {
            foreach (var asset in assetRequests)
                Addressables.Release(asset.Value);

            assetRequests.Clear();
        }
    }
}