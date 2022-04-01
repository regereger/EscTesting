using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
  internal class PlayerMoveSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();

      EcsFilter moveFilter = world.Filter<MoveRequest>().End();
      EcsFilter playerFilter = world.Filter<PlayerMoveComponent>().End();

      EcsPool<PlayerMoveComponent> playerPool = world.GetPool<PlayerMoveComponent>();
      EcsPool<MoveRequest> playerInputPool = world.GetPool<MoveRequest>();

      foreach (int entity in moveFilter)
      {
        ref MoveRequest playerInputComponent = ref playerInputPool.Get(entity);
        foreach (int pEntity in playerFilter)
        {
          ref PlayerMoveComponent playerComponent = ref playerPool.Get(pEntity);
          playerComponent.navMeshAgent.SetDestination(playerInputComponent.destination);
        }
        world.DelEntity(entity);
      }
    }
  }
}