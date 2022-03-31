using Leopotam.EcsLite;
using Tools;
using UniRx;
using Views;

namespace Root
{
  internal class Root : BaseDisposable
  {
    public struct Ctx
    {
      // init params for game if needed - configuration release dev etc.
      public GlobalConfigView globalConfig;
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

      // init global systems
      _systems.Init();
      _physSystems.Init();

      StartSystemExecute();
      
      // here can be complicated logic to choose which level to load ( from UI or smth ) 
      Level.Ctx lvlCtx = new Level.Ctx
      {
        ecsWorld = _ecsWorld,
        globalConfig = _ctx.globalConfig
      };
      Level level = new Level(lvlCtx);
      AddDispose(level);
    }

    private void StartSystemExecute()
    {
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