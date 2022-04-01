using Constants;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
  internal class PlayerAnimationSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();

      EcsFilter playerFilter = world.Filter<PlayerMoveComponent>().Inc<PlayerAnimationComponent>().End();

      EcsPool<PlayerMoveComponent> movePool = world.GetPool<PlayerMoveComponent>();
      EcsPool<PlayerAnimationComponent> animPool = world.GetPool<PlayerAnimationComponent>();

      foreach (int pEntity in playerFilter)
      {
        ref PlayerMoveComponent moveComponent = ref movePool.Get(pEntity);
        ref PlayerAnimationComponent animationComponent = ref animPool.Get(pEntity);

        animationComponent.animator.SetBool(AnimatorNames.MOVING_BOOL, moveComponent.navMeshAgent.hasPath);
      }
    }
  }
}