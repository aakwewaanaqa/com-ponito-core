namespace Ponito.Core
{
    public partial struct FloatRange
    {
        /// <summary>
        ///     <see cref="min"/> + <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator +(FloatRange r, float f)
        {
            return new FloatRange(r.min + f, r.max + f);
        }

        /// <summary>
        ///     <see cref="min"/> - <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator -(FloatRange r, float f)
        {
            return new FloatRange(r.min - f, r.max - f);
        }

        /// <summary>
        ///     <see cref="min"/> * <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator *(FloatRange r, float f)
        {
            return new FloatRange(r.min * f, r.max * f);
        }

        /// <summary>
        ///     <see cref="min"/> / <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator /(FloatRange r, float f)
        {
            return new FloatRange(r.min / f, r.max / f);
        }
        
        /// <summary>
        ///     <see cref="min"/> + <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator +(float f, FloatRange r)
        {
            return new FloatRange(r.min + f, r.max + f);
        }

        /// <summary>
        ///     <see cref="min"/> - <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator -(float f, FloatRange r)
        {
            return new FloatRange(r.min - f, r.max - f);
        }

        /// <summary>
        ///     <see cref="min"/> * <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator *(float f, FloatRange r)
        {
            return new FloatRange(r.min * f, r.max * f);
        }

        /// <summary>
        ///     <see cref="min"/> / <see cref="f"/> and <see cref="max"/> + <see cref="f"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator /(float f, FloatRange r)
        {
            return new FloatRange(r.min / f, r.max / f);
        }

        /// <summary>
        ///     <see cref="a"/>.<see cref="min"/> + <see cref="b"/>.<see cref="min"/> and <see cref="a"/>.<see cref="max"/> + <see cref="b"/>.<see cref="max"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator +(FloatRange a, FloatRange b)
        {
            return new FloatRange(a.min + b.min, a.max + b.max);
        }

        /// <summary>
        ///     <see cref="a"/>.<see cref="min"/> - <see cref="b"/>.<see cref="min"/> and <see cref="a"/>.<see cref="max"/> - <see cref="b"/>.<see cref="max"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator -(FloatRange a, FloatRange b)
        {
            return new FloatRange(a.min - b.min, a.max - b.max);
        }

        /// <summary>
        ///     <see cref="a"/>.<see cref="min"/> * <see cref="b"/>.<see cref="min"/> and <see cref="a"/>.<see cref="max"/> * <see cref="b"/>.<see cref="max"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator *(FloatRange a, FloatRange b)
        {
            return new FloatRange(a.min * b.min, a.max * b.max);
        }

        /// <summary>
        ///     <see cref="a"/>.<see cref="min"/> / <see cref="b"/>.<see cref="min"/> and <see cref="a"/>.<see cref="max"/> / <see cref="b"/>.<see cref="max"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static FloatRange operator /(FloatRange a, FloatRange b)
        {
            return new FloatRange(a.min / b.min, a.max / b.max);
        }

        /// <summary>
        ///     takes <see cref="float"/> then gives <see cref="FloatRange"/>
        /// </summary>
        /// <returns><see cref="FloatRange"/></returns>
        public static implicit operator FloatRange(float f)
        {
            return One * f;
        }
    }
}