using Leopotam.EcsLite;

namespace Systems
{
  internal class PlayerInitSystem : IEcsInitSystem
  {
    public void Init(EcsSystems systems)
    {
      EcsWorld ecsWorld = systems.GetWorld();
      // int playerEntity = ecsWorld.NewEntity();
      DDebug.Log("hi");
    }
  }
}