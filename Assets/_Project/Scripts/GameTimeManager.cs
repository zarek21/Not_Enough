using UnityEngine;

namespace NotEnough.Systems
{
    public class GameTimeManager : MonoBehaviour
    {
        private void Awake()
        {
            // Limitamos a 80 FPS
            Application.targetFrameRate = 80;
            
            // Desactivamos VSync para que el target sea exacto
            QualitySettings.vSyncCount = 0;
        }
    }
}
