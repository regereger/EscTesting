using Leopotam.EcsLite;
using UnityEngine;
using Views;

namespace Ecs.Systems
{
  internal class PlayerInitSystem : IEcsInitSystem
  {
    public void Init(EcsSystems systems)
    {
      Settings settings = systems.GetShared<Settings>();

      Object.Instantiate(settings.globalConfigView.playerPrefab, settings.levelConfigView.PlayerSpawn.position,
        Quaternion.identity);
    }
  }
}