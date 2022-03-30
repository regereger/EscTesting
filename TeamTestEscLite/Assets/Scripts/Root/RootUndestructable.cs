using Tools;
using UniRx;
using UnityEngine;

namespace Core
{
  internal class RootUndestructable : BaseMonoBehaviour
  {
    public struct Ctx
    {
      // init params for game if needed - configuration release dev etc.
    }

    // set here some global objects you need at the start
    [SerializeField]
    private GameObject debugObjects;

    private Ctx _ctx;

    public void SetCtx(Ctx ctx)
    {
      _ctx = ctx;
      Root.Ctx rootCtx = new Root.Ctx();
      Root root = new Root(rootCtx);
      root.AddTo(this);
      // dispose root when app is closed
      Observable.OnceApplicationQuit().Subscribe(_ => root?.Dispose()).AddTo(this);
    }
  }
}