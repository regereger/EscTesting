using DG.Tweening;
using Ecs.Components;
using Ecs.Views;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ecs.Systems
{
  internal class OpenDoorSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();
      EcsFilter filter = world.Filter<OpenDoorRequest>().End();
      EcsPool<OpenDoorRequest> pool = world.GetPool<OpenDoorRequest>();

      foreach (int entity in filter)
      {
        ref OpenDoorRequest request = ref pool.Get(entity);

        GroundButtonView buttonView = request.buttonObject.GetComponent<GroundButtonView>();
        if (buttonView)
        {
          buttonView.MyCollider.enabled = false;
          buttonView.transform.DOMoveY(0.05f, 0.5f);
          buttonView.Door.transform.DOMoveY(-1f, 0.5f);
          // you can cache components on door object with view on it
          buttonView.Door.GetComponent<Collider>().enabled = false;
          buttonView.Door.GetComponent<NavMeshObstacle>().enabled = false;
        }
        world.DelEntity(entity);
      }
    }
  }
}