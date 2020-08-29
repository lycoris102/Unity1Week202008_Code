using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.Utility
{
    public static class GraphicExtension
    {
        public static void SetAlpha(this Graphic self, float alpha)
        {
            var color = self.color;
            color.a = alpha;
            self.color = color;
        }
    }
}