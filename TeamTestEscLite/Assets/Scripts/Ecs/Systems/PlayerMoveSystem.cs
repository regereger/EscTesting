using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
  internal class PlayerMoveSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();
      
      // EcsFilter requestFliter = world.Filter<MoveRequest>().End();
      // foreach (int entity in requestFliter)
      // {
      //   ref PlayerMoveComponent weapon = ref movers.Get(entity);
      //   
      // }

      // get player move component
      EcsFilter filter = world.Filter<PlayerMoveComponent>().End();
      EcsPool<PlayerMoveComponent> movers = world.GetPool<PlayerMoveComponent>();
      foreach (int entity in filter)
      {
        ref PlayerMoveComponent weapon = ref movers.Get(entity);
        // weapon.navMeshAgent.SetDestination(new Vector3(1,0.5f,1));
      }
    }
  }
}