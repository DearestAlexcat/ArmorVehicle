using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class EnemyPlacer
    {
        Dictionary<Chunk, List<Enemy>> chunkEnemies = new();

        IInteractorPoolContainer interactorPool;

        [Inject]
        public EnemyPlacer(IInteractorPoolContainer interactorPool)
        {
            this.interactorPool = interactorPool;
        }

        public void SpawnEnemy(Chunk chunk, int enemiesPerChunk)
        {
            chunkEnemies.Add(chunk, new List<Enemy>());
            Enemy newEnemy;

            for (int i = 0; i < enemiesPerChunk; i++)
            {
                newEnemy = interactorPool.Get<Enemy>().Get();
                
                newEnemy.spawnZonePosition = chunk.GetSpawnZonePosition();
                newEnemy.spawnZoneRadius = chunk.spawnZoneRadius;

                newEnemy.transform.position = GetRandomPointInCircle(newEnemy.spawnZonePosition, chunk.spawnZoneRadius);
                
                chunkEnemies[chunk].Add(newEnemy);
            }
        }

        Vector3 GetRandomPointInCircle(Vector3 center, float radius)
        {
            Vector2 randomPoint2D = Random.insideUnitCircle * radius;
            return center + new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        }

        public void DestroyEnemy(Enemy enemy)
        {
            foreach (var item in chunkEnemies)
            {
                if(item.Value.Contains(enemy))
                {
                    enemy.ReturnToPool();
                    item.Value.Remove(enemy);
                    return;
                }
            }
        }

        public void DestroyEnemies(Chunk chunk)
        {
            if (!chunkEnemies.TryGetValue(chunk, out List<Enemy> enemyList)) return;

            foreach (Enemy enemy in enemyList)
                enemy.ReturnToPool();

            enemyList.Clear();
            chunkEnemies.Remove(chunk);
        }
    }
}