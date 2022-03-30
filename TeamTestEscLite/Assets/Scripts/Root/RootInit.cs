using Tools;
using UnityEngine;

namespace Core
{
  internal class RootInit : BaseMonoBehaviour
  {
    private static bool _inited;
    private const string ROOT_UNDESTR = "RootDontDestroy";

    protected override void Awake()
    {
      if (_inited)
        return;
      _inited = true;
      
      // load main undestructable object and insert context into it
      GameObject undestrPrefab = Resources.Load<GameObject>(ROOT_UNDESTR);
      GameObject undestrObj = Instantiate(undestrPrefab);
      DontDestroyOnLoad(undestrObj);
      RootUndestructable rootUndestructable = undestrObj.GetComponent<RootUndestructable>();
      // create context 
      RootUndestructable.Ctx ctx = new RootUndestructable.Ctx();
      rootUndestructable.SetCtx(ctx);
    }
  }
}