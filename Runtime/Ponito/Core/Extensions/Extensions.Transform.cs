using UnityEngine;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static RectTransform SetMinX(this RectTransform self, float x)
        {
            var min = self.anchorMin;

            min.x          = x;
            self.anchorMin = min;
            return self;
        }

        public static RectTransform SetMinY(this RectTransform self, float y)
        {
            var min = self.anchorMin;

            min.y          = y;
            self.anchorMin = min;
            return self;
        }

        public static RectTransform SetMaxX(this RectTransform self, float x)
        {
            var max = self.anchorMax;

            max.x          = x;
            self.anchorMax = max;
            return self;
        }

        public static RectTransform SetMaxY(this RectTransform self, float y)
        {
            var max = self.anchorMax;

            max.y          = y;
            self.anchorMax = max;
            return self;
        }

        public static RectTransform SetSizeX(this RectTransform self, float x)
        {
            var size = self.sizeDelta;

            size.x         = x;
            self.sizeDelta = size;
            return self;
        }

        public static RectTransform SetSizeY(this RectTransform self, float y)
        {
            var size = self.sizeDelta;

            size.y         = y;
            self.sizeDelta = size;
            return self;
        }

        public static RectTransform SetRight(this RectTransform self, float right)
        {
            var offset = self.offsetMax;

            offset.x       = -right;
            self.offsetMax = offset;
            return self;
        }

        public static RectTransform SetUp(this RectTransform self, float up)
        {
            var offset = self.offsetMax;

            offset.y       = -up;
            self.offsetMax = offset;
            return self;
        }

        public static RectTransform SetLeft(this RectTransform self, float left)
        {
            var offset = self.offsetMin;

            offset.x       = left;
            self.offsetMin = offset;
            return self;
        }

        public static RectTransform SetBottom(this RectTransform self, float bottom)
        {
            var offset = self.offsetMin;

            offset.y       = bottom;
            self.offsetMin = offset;
            return self;
        }

        public static RectTransform SetPosX(this RectTransform self, float x)
        {
            var pos = self.anchoredPosition;

            pos.x                 = x;
            self.anchoredPosition = pos;
            return self;
        }

        public static RectTransform SetPosY(this RectTransform self, float y)
        {
            var pos = self.anchoredPosition;

            pos.y                 = y;
            self.anchoredPosition = pos;
            return self;
        }

        public static RectTransform SetPivotX(this RectTransform self, float x)
        {
            var pivot = self.pivot;
            pivot.x    = x;
            self.pivot = pivot;
            return self;
        }

        public static RectTransform SetPivotY(this RectTransform self, float y)
        {
            var pivot = self.pivot;
            pivot.y    = y;
            self.pivot = pivot;
            return self;
        }
    }
}