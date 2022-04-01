using Ecs.Components;
using Ecs.Views;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Views;

namespace Ecs.Systems
{
  internal class PlayerInitSystem : IEcsInitSystem
  {
    public void Init(EcsSystems systems)
    {
      GameSettings gameSettings = systems.GetShared<GameSettings>();

      // create player
      GameObject playerObj = Object.Instantiate(gameSettings.globalConfigView.playerPrefab, gameSettings.levelConfigView.PlayerSpawn.position,
        Quaternion.identity);

      // prepare move component
      EcsWorld world = systems.GetWorld();
      int newEntity = world.NewEntity();
      EcsPool<PlayerMoveComponent> pool = world.GetPool<PlayerMoveComponent> ();
      ref PlayerMoveComponent playerMoveComponent = ref pool.Add(newEntity);
      playerMoveComponent.navMeshAgent = playerObj.GetComponent<NavMeshAgent>();
      
      // put context to scripts
      CollisionDetectionView detectionView = playerObj.GetComponent<CollisionDetectionView>();
      detectionView.SetCtx(new CollisionDetectionView.Ctx
      {
        ecsWorld = world
      });
    }
  }
}