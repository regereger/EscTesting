using Tools;
using UnityEngine;

namespace Ecs.Views
{
  internal class GroundButtonView : BaseMonoBehaviour
  {
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private Collider myCollider;

    public GameObject Door => door;
    public Collider MyCollider => myCollider;
  }
}