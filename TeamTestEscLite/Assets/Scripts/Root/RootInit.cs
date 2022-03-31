using Tools;
using UnityEngine;
using Views;

namespace Root
{
  internal class RootInit : BaseMonoBehaviour
  {
    private static bool _inited;
    private const string ROOT_UNDESTR = "RootDontDestroy";
    private const string CONFIG = "GlobalConfig";

    protected override void Awake()
    {
      if (_inited)
        return;
      _inited = true;

      // load config
      GlobalConfigView globalConfig = Resources.Load<ScriptableObject>(CONFIG) as GlobalConfigView;
      // load main undestructable object and insert context into it
      GameObject undestrPrefab = Resources.Load<GameObject>(ROOT_UNDESTR);
      GameObject undestrObj = Instantiate(undestrPrefab);
      DontDestroyOnLoad(undestrObj);
      RootUndestructable rootUndestructable = undestrObj.GetComponent<RootUndestructable>();
      // create context 
      RootUndestructable.Ctx ctx = new RootUndestructable.Ctx
      {
        globalConfig = globalConfig
      };
      rootUndestructable.SetCtx(ctx);
    }
  }
}