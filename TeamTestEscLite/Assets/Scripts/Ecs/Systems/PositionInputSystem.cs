using Constants;
using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Views;

namespace Ecs.Systems
{
  internal class PositionInputSystem : IEcsRunSystem
  {
    private readonly RaycastHit[] _results;

    public PositionInputSystem()
    {
      _results = new RaycastHit[5];
    }

    public void Run(EcsSystems systems)
    {
      GameSettings gameSettings = systems.GetShared<GameSettings>();
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = gameSettings.levelConfigView.MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.RaycastNonAlloc(ray, _results) > 0)
        {
          foreach (RaycastHit hit in _results)
          {
            if (hit.transform && hit.transform.CompareTag(Tags.FLOOR_TAG))
            {
              Debug.Log($"we hit floor at {hit.point}");
              GotDestinationPoint(systems, hit.point);
              break;
            }
          }
        }
      }
    }

    private void GotDestinationPoint(EcsSystems systems, Vector3 destination)
    {
      int entity = systems.GetWorld().NewEntity();
      EcsPool<MoveRequest> movePool = systems.GetWorld().GetPool<MoveRequest>();
      movePool.Add(entity);
      ref MoveRequest moveRequest = ref movePool.Get(entity);
      moveRequest.destination = destination;
    }
  }
}