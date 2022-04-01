using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
  internal class PlayerMoveVisualSystem : IEcsRunSystem
  {
    private readonly GameObject _cube;

    public PlayerMoveVisualSystem()
    {
      _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      _cube.transform.localScale *= 0.2f;
      _cube.GetComponent<Renderer>().material.color = Color.green;
      _cube.transform.position = new Vector3(-1000, -1000, -1000);
    }

    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();

      EcsFilter moveFilter = world.Filter<MoveRequest>().End();

      EcsPool<MoveRequest> movePool = world.GetPool<MoveRequest>();

      foreach (int entity in moveFilter)
      {
        ref MoveRequest request = ref movePool.Get(entity);
        _cube.transform.position = request.destination;
      }
    }
  }
}