using Leopotam.EcsLite;
using Systems;
using Tools;
using UniRx;

namespace Core
{
  internal class Root : BaseDisposable
  {
    public struct Ctx
    {
      // init params for game if needed - configuration release dev etc.
    }

    private readonly Ctx _ctx;
    private readonly EcsWorld _ecsWorld;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _physSystems;

    public Root(Ctx ctx)
    {
      _ctx = ctx;
      _ecsWorld = new EcsWorld();
      _systems = new EcsSystems(_ecsWorld);
      _physSystems = new EcsSystems(_ecsWorld);
      
      // add systems to global systems
      _systems.Add(new PlayerInitSystem());
      
      // init global systems
      _systems.Init();
      _physSystems.Init();
      
      // start global update and fixed update systems
      AddDispose(Observable.EveryUpdate().Subscribe(_ => _systems.Run()));
      AddDispose(Observable.EveryFixedUpdate().Subscribe(_ => _physSystems.Run()));
    }

    protected override void OnDispose()
    {
      _systems?.Destroy();
      _physSystems?.Destroy();
      _ecsWorld?.Destroy();
      base.OnDispose();
    }
  }
}