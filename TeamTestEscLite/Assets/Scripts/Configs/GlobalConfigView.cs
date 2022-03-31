using UnityEngine;

namespace Views
{
  [CreateAssetMenu(fileName = "GlobalConfig", menuName = "ScriptableObjects/CreateGlobalConfig", order = 1)]
  internal class GlobalConfigView : ScriptableObject
  {
    public GameObject playerPrefab;
  }
}