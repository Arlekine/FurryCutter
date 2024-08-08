using UnityEngine;

namespace FurryCutter.Meta
{
    [CreateAssetMenu(menuName = "Data/Meta/AllFurriesConfig", fileName = "AllFurries")]
    public class AllFurriesConfig : ScriptableObject
    {
        [SerializeField] private FurryGroupConfig _commonFurries;
        [SerializeField] private FurryGroupConfig _rareFurries;
        [SerializeField] private FurryGroupConfig _epicFurries;
        
        public FurryGroupConfig CommonFurries => _commonFurries;
        public FurryGroupConfig RareFurries => _rareFurries;
        public FurryGroupConfig EpicFurries => _epicFurries;
    }
}