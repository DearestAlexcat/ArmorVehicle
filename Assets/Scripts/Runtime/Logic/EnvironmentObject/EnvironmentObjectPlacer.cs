using System.Collections.Generic;
using Zenject;

namespace ArmorVehicle
{
    public class EnvironmentObjectPlacer
    {
        Dictionary<Chunk, List<EnvironmentObject>> environmentObjects = new();

        IInteractorPoolContainer interactorPool;

        [Inject]
        public EnvironmentObjectPlacer(IInteractorPoolContainer interactorPool)
        {
            this.interactorPool = interactorPool;
        }

        public void SpawnObject(Chunk chunk)
        {
            if (chunk.EnvironmentObjectIsEmpty()) return;

            environmentObjects.Add(chunk, new List<EnvironmentObject>());
            EnvironmentObject newObject;

            foreach (var position in chunk.GetEnvironmentObjectPositions())
            {
                newObject = interactorPool.Get<EnvironmentObject>().Get();
                if (newObject == null) continue;
                newObject.transform.position = position;
                environmentObjects[chunk].Add(newObject);
            }
        }

        public void DestroyEnvironmentObject(Chunk chunk)
        {
            if (!environmentObjects.TryGetValue(chunk, out List<EnvironmentObject> objectList)) return;

            foreach (EnvironmentObject obj in objectList)
                obj.ReturnToPool();

            objectList.Clear();
            environmentObjects.Remove(chunk);
        }
    }
}