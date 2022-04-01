using Constants;
using Ecs.Components;
using Ecs.Components.UnityPhysics;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
  internal class CollisionSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();
      EcsFilter triggerFilter = world.Filter<OnTriggerEnterEvent>().End();
      EcsPool<OnTriggerEnterEvent> triggerPool = world.GetPool<OnTriggerEnterEvent>();

      foreach (int entity in triggerFilter)
      {
        ref OnTriggerEnterEvent triggerEnter = ref triggerPool.Get(entity);
        
        // if we hit ground button then make request to open door
        if (triggerEnter.Reciever.transform.CompareTag(Tags.GROUND_BUTTON_TAG))
        {
          DDebug.Log("OnTriggerEnter with button");
          int openDoor = world.NewEntity();
          EcsPool<OpenDoorRequest> pool = world.GetPool<OpenDoorRequest>();
          pool.Add(openDoor);
          ref OpenDoorRequest hitComponent = ref pool.Get(openDoor);
          hitComponent.buttonObject = triggerEnter.Reciever;
        }
        
        world.DelEntity(entity);
      }
    }
  }
}