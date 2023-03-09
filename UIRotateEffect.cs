using UnityEngine;
using UnityEngine.UI;

namespace Plugins.mitaywalle.Runtime.UI.Effects
{
    [RequireComponent(typeof(RectTransform), typeof(Graphic)), DisallowMultipleComponent]
    public class UIRotateEffect : BaseMeshEffect
    {
        [SerializeField] private bool _rotate;

        public override void ModifyMesh(VertexHelper verts)
        {
            if (!_rotate) return;
            if (!isActiveAndEnabled) return;

            RectTransform rt = transform as RectTransform;
            Rect rect = rt.rect;
            float xMin = rect.xMin;
            float xMax = rect.xMax;
            float yMin = rect.yMin;
            float yMax = rect.yMax;

            for (int i = 0; i < verts.currentVertCount; ++i)
            {
                UIVertex uiVertex = new UIVertex();
                verts.PopulateUIVertex(ref uiVertex, i);

                Vector3 position = uiVertex.position;
                float x = InverseLerp(yMin, yMax, position.y, xMin, xMax);
                float y = InverseLerp(xMin, xMax, position.x, yMin, yMax);
                uiVertex.position = new Vector3(x, y, position.z);

                verts.SetUIVertex(uiVertex, i);
            }
        }

        private float InverseLerp(float min, float max, float value, float minResult, float maxResult)
        {
            return Mathf.LerpUnclamped(minResult, maxResult, Mathf.InverseLerp(min, max, value));
        }
    }
}
