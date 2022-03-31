using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
  internal class PositionInputSystem : IEcsRunSystem
  {
    public void Run(EcsSystems systems)
    {
      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
 
        if(Physics.Raycast (ray, out hit))
        {
          if(hit.transform.name == "Player")
          {
            Debug.Log ("This is a Player");
          }
          else {
            Debug.Log ("This isn't a Player");                
          }
        }
      }
    }
  }
}