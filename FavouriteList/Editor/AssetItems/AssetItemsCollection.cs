using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SmallEditorTools.FavouriteList.AssetItems
{
    [Serializable]
    public class AssetItemsCollection : ItemsCollection<AssetItem>
    {
        public static AssetItemsCollection TryCreateFromObject(Object obj)
        {
            AssetItemsCollection item = new AssetItemsCollection();
            return item.TryAddItem(obj) ? item : null;
        }

        public override bool TryAddItem(Object obj)
        {
            AssetItem item = AssetItem.TryCreateFromObject(obj);

            if (item == null)
                return false;

            Add(item);
            return true;
        }

        public override void Render(Rect rect)
        {
            Color prevColor = GUI.backgroundColor;

            GUI.backgroundColor = Color.cyan;
            {
                base.Render(rect);
            }
            GUI.backgroundColor = prevColor;
        }

        protected override void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Assets", EditorStyles.boldLabel);
        }

        public override bool ShouldBeRendered()
        {
            return true;
        }
    }
}