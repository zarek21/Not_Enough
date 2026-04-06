using UnityEngine;
using NotEnough.Systems;

namespace NotEnough.World
{
    public class PurpleLight : MonoBehaviour
    {
        public void Collect(GameObject player)
        {
            var uiManager = FindFirstObjectByType<GameUIManager>();
            if (uiManager != null)
            {
                uiManager.AddPurpleOrb();
            }

            Destroy(gameObject);
        }
    }
}
