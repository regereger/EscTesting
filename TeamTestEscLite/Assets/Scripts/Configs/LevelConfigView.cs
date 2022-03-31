using Tools;
using UnityEngine;

namespace Views
{
  internal class LevelConfigView : BaseMonoBehaviour
  {
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform playerSpawn;

    public Camera MainCamera 
      => mainCamera;
    
    public Transform PlayerSpawn 
      => playerSpawn;
  }
}