using Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Views;

namespace Ecs.Systems
{
  internal class CameraSystem : IEcsInitSystem, IEcsRunSystem
  {
    private Transform _camTransform;
    private Vector3 _offset;

    public void Init(EcsSystems systems)
    {
      GameSettings gameSettings = systems.GetShared<GameSettings>();
      _camTransform = gameSettings.levelConfigView.MainCamera.transform;
      _offset = _camTransform.position - gameSettings.levelConfigView.PlayerSpawn.position;
    }
    
    public void Run(EcsSystems systems)
    {
      EcsWorld world = systems.GetWorld();
      EcsFilter playerFilter = world.Filter<PlayerTransformComponent>().End();
      EcsPool<PlayerTransformComponent> playerPool = world.GetPool<PlayerTransformComponent>();

      foreach (int pEntity in playerFilter)
      {
        ref PlayerTransformComponent playerTransform = ref playerPool.Get(pEntity);
        _camTransform.position = playerTransform.transform.position + _offset;
      }
    }
  }
}