using Ecs.Components;
using Ecs.Views;
using Leopotam.EcsLite;
using UnityEditor.Animations;
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

      // prepare components
      EcsWorld world = systems.GetWorld();
      int playerEntity = world.NewEntity();
      
      EcsPool<PlayerMoveComponent> poolMove = world.GetPool<PlayerMoveComponent> ();
      ref PlayerMoveComponent playerMove = ref poolMove.Add(playerEntity);
      playerMove.navMeshAgent = playerObj.GetComponent<NavMeshAgent>();
      
      EcsPool<PlayerAnimationComponent> poolAnim = world.GetPool<PlayerAnimationComponent> ();
      ref PlayerAnimationComponent playerAnimation = ref poolAnim.Add(playerEntity);
      playerAnimation.animator = playerObj.GetComponentInChildren<Animator>();

      EcsPool<PlayerTransformComponent> poolT = world.GetPool<PlayerTransformComponent>();
      ref PlayerTransformComponent transformComponent = ref poolT.Add(playerEntity);
      transformComponent.transform = playerObj.transform;
      
      // put context to scripts
      CollisionDetectionView detectionView = playerObj.GetComponent<CollisionDetectionView>();
      detectionView.SetCtx(new CollisionDetectionView.Ctx
      {
        ecsWorld = world
      });
    }
  }
}