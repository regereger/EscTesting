using UnityEngine;

namespace Ecs.Components.UnityPhysics
{
  public struct OnTriggerEnterEvent
  {
    public GameObject Sender;
    public GameObject Reciever;
  }
}