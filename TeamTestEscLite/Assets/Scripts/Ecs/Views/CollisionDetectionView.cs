using Constants;
using Ecs.Components.UnityPhysics;
using Leopotam.EcsLite;
using Tools;
using UnityEngine;

namespace Ecs.Views
{
  internal class CollisionDetectionView : BaseMonoBehaviour
  {
    public struct Ctx
    {
      public EcsWorld ecsWorld;
    }

    private Ctx _ctx;

    public void SetCtx(Ctx ctx)
    {
      _ctx = ctx;
    }
    
    private void OnTriggerEnter(Collider other)
    {
      Log.Info("OnTriggerEnter");
      
      int hit = _ctx.ecsWorld.NewEntity();
      EcsPool<OnTriggerEnterEvent> hitPool = _ctx.ecsWorld.GetPool<OnTriggerEnterEvent>();
      hitPool.Add(hit);
      ref OnTriggerEnterEvent hitComponent = ref hitPool.Get(hit);

      hitComponent.Sender = transform.root.gameObject;
      hitComponent.Reciever = other.gameObject;
    }
  }
}