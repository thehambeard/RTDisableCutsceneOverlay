using UnityModManagerNet;

namespace DisableCSO
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool LetterBox = true;

        public override void Save(UnityModManager.ModEntry modEntry) => Save(this, modEntry);
    }
}
