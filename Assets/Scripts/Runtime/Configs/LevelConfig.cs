using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelN")]
    public class LevelConfig : ScriptableObject
    {
        public int levelIndex;

        [Space]
        public Chunk startChunkPrefab;              
        public Chunk finishChunkPrefab;             

        [Tooltip("Environment objects are randomly selected and spawned on the chunk")]
        public EnvironmentObject[] environmentObjects;
        [Tooltip("Number of chunks displayed at game start")]
        [Min(5)] public int initialVisibleChunkCount;
        [Tooltip("After how many meters to show the next chunk")]
        [Min(0)] public int chunkActivationDistance;

        [Space]
        [Tooltip("Chunk prefabs that spawn one after the other")]
        public ChunkSpawnSettings[] chunkSpawnSettings;
    }

    [System.Serializable]
    public class ChunkSpawnSettings
    {
        [Tooltip("Chunk prefab")]
        public Chunk chunkPrefab;

        [Min(0)]
        [Tooltip("The number of enemies that spawn in a chunk")]
        public int enemiesPerChunk;
    }
}