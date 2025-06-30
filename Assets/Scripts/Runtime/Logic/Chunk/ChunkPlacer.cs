using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class ChunkPlacer : ITickable
    {
        List<Chunk> chunks = new List<Chunk>();

        readonly Car car;
        readonly EnemyPlacer enemyPlacer;
        readonly IInteractorPoolContainer interactorPool;
        readonly MovementTracker movementTracker;

        LevelConfig config;
        EnvironmentObjectPlacer environmentObjectPlacer;

        int chunkPointerStart, chunkPointerEnd;

        [Inject]
        public ChunkPlacer(IInteractorPoolContainer interactorPool, Car car, EnemyPlacer enemyPlacer, MovementTracker movementTracker)
        {
            this.car = car;
            this.enemyPlacer = enemyPlacer;
            this.interactorPool = interactorPool;
            this.movementTracker = movementTracker;

            environmentObjectPlacer = new EnvironmentObjectPlacer(interactorPool);
        }

        public void Initialize(IStaticDataService staticDataService)
        {
            config = staticDataService.GetLevelConfig(0);
            SpawnInitialChunks(staticDataService.CarConfig.carStartPosition);
        }

        void SpawnInitialChunks(Vector3 spawnPosition)
        {
            SpawnChunk(config.startChunkPrefab, spawnPosition);

            movementTracker.AddDistance(chunks[^1].GetDistance() * 0.5f);

            int initialVisibleChunkCount = config.initialVisibleChunkCount;

            chunkPointerStart = 0;        
            chunkPointerEnd = initialVisibleChunkCount;         

            foreach (var chunkSettings in config.chunkSpawnSettings)
            {
                SpawnChunk(chunkSettings.chunkPrefab);

                movementTracker.AddDistance(chunks[^1].GetDistance());

                if (initialVisibleChunkCount > 0)
                {
                    environmentObjectPlacer.SpawnObject(chunks[^1]);
                    enemyPlacer.SpawnEnemy(chunks[^1], chunkSettings.enemiesPerChunk);
                }
                else
                {
                    chunks[^1].gameObject.SetActive(false);
                }

                initialVisibleChunkCount--;
            }

            SpawnChunk(config.finishChunkPrefab);

            movementTracker.AddDistance(chunks[^1].GetDistance() * 0.5f);

            if (chunks.Count - 2 > config.initialVisibleChunkCount)
                chunks[^1].gameObject.SetActive(false);
        }

        void SpawnChunk(Chunk chunkPrefab, Vector3? spawnPosition = null)
        {
            Chunk chunk = Object.Instantiate(chunkPrefab);
            chunk.transform.position = spawnPosition ?? chunks[^1].End.position - chunk.Begin.localPosition;
            chunk.transform.rotation = Quaternion.identity;
            chunks.Add(chunk);
        }

        void DeactivateChunk()
        {
            if (chunkPointerStart < chunks.Count)
            {
                chunks[chunkPointerStart].gameObject.SetActive(false);
                enemyPlacer.DestroyEnemies(chunks[chunkPointerStart]);
                environmentObjectPlacer.DestroyEnvironmentObject(chunks[chunkPointerStart]);
                chunkPointerStart++;
            }
        }

        void ActivateChunk()
        {
            if (chunkPointerEnd + 1 >= chunks.Count) return;
            
            chunkPointerEnd++;

            chunks[chunkPointerEnd].gameObject.SetActive(true);

            // Start chunk is at position 0, finish chunk is the last
            // Spawn enemies only if the current chunk is not finish
            bool isGameplayChunk = chunkPointerEnd > 0 && chunkPointerEnd < chunks.Count - 1;
            if (isGameplayChunk)
            {
                int spawnSettingsIndex = chunkPointerEnd - 1;

                environmentObjectPlacer.SpawnObject(chunks[chunkPointerEnd]);
                enemyPlacer.SpawnEnemy(chunks[chunkPointerEnd], config.chunkSpawnSettings[spawnSettingsIndex].enemiesPerChunk);
            }
        }

        public void Tick()
        {
            if (config != null && chunks.Count - 2 < config.initialVisibleChunkCount) return;

            if (chunks.Count == 0 || chunkPointerEnd + 1 >= chunks.Count) return;

            if (Vector3.Distance(chunks[chunkPointerEnd].End.position, car.transform.position) <= config.chunkActivationDistance)
            {
                DeactivateChunk();
                ActivateChunk();
            }
        }
    }
}