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
    private EcsSystems _initSystems;
    private EcsSystems _updateSystems;
    
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

      GameSettings gameSettings = new GameSettings
      {
        globalConfigView = _ctx.globalConfig,
        levelConfigView = levelConfigView
      };

      _initSystems = new EcsSystems(_ctx.ecsWorld, gameSettings);
      _initSystems
        .Add(new PlayerInitSystem());
      _initSystems.Init();
      
      _updateSystems = new EcsSystems(_ctx.ecsWorld, gameSettings);

      _updateSystems
        .Add(new CameraSystem())
        .Add(new PositionInputSystem())
        .Add(new PlayerMoveVisualSystem())
        .Add(new PlayerMoveSystem())
        .Add(new PlayerAnimationSystem())
        .Add(new CollisionSystem())
        .Add(new OpenDoorSystem());

      _updateSystems.Init();

      // start update and fixed update systems
      AddDispose(Observable.EveryUpdate().Subscribe(_ => _updateSystems.Run()));
    }
    
    protected override void OnDispose()
    {
      _updateSystems?.Destroy();
      base.OnDispose();
    }
  }
}