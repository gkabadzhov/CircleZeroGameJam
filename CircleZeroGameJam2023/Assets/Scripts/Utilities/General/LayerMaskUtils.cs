using UnityEngine;

namespace OTBG.Utilities.General
{
    public static class LayerMaskUtils
    {
        // Combines two LayerMasks into one LayerMask that includes all layers from both.
        public static LayerMask CombineLayerMasks(LayerMask firstLayerMask, LayerMask secondLayerMask)
        {
            return firstLayerMask | secondLayerMask;
        }
    }
}