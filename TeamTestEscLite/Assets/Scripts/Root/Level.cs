using Ecs.Systems;
using Leopotam.EcsLite;
using Tools;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;

namespace Root
{
  internal class Level : BaseDisposable
  {
    public struct Ctx
    {
      public EcsWorld ecsWorld;
      public GlobalConfigView globalConfig;
    }

    private readonly Ctx _ctx;
    private EcsSystems _systems;
    private EcsSystems _physSystems;
    
    private const string SCENE_NAME = "Level";

    public Level(Ctx ctx)
    {
      _ctx = ctx;
      
      // load scene somehow
      SceneManager.LoadScene(SCENE_NAME);
      SceneManager.sceneLoaded += (scene, mode) =>
      {
        if (scene.name != SCENE_NAME)
          return;
        LevelLoaded();
      };
    }

    private void LevelLoaded()
    {
      // find scene config
      LevelConfigView levelConfigView = Object.FindObjectOfType<LevelConfigView>();

      Settings settings = new Settings
      {
        globalConfigView = _ctx.globalConfig,
        levelConfigView = levelConfigView
      };

      _systems = new EcsSystems(_ctx.ecsWorld, settings);
      _physSystems = new EcsSystems(_ctx.ecsWorld, settings);

      _systems.Add(new PlayerInitSystem());
        
      _systems.Init();
      _physSystems.Init();

      // start update and fixed update systems
      AddDispose(Observable.EveryUpdate().Subscribe(_ => _systems.Run()));
      AddDispose(Observable.EveryFixedUpdate().Subscribe(_ => _physSystems.Run()));
    }
    
    protected override void OnDispose()
    {
      _systems?.Destroy();
      _physSystems?.Destroy();
      base.OnDispose();
    }
  }
}