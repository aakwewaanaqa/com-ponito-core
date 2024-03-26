using System.Diagnostics;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Checks whether <see cref="object"/> is null or not, also knows a destroyed one is null too.
        /// </summary>
        /// <param name="self"><see cref="object"/> to check</param>
        /// <returns>true is null</returns>
        [DebuggerHidden]
        public static bool IsNull(this object self)
        {
            return self is null || self.Equals(null);
        }

        /// <summary>
        ///     Inverted version of <see cref="IsNull"/>
        /// </summary>
        /// <param name="self"><see cref="object"/> to check</param>
        /// <returns>true is not null</returns>
        [DebuggerHidden]
        public static bool IsObject(this object self)
        {
            return self is not null && !self.Equals(null);
        }

        /// <summary>
        ///     Same as <see cref="object.ReferenceEquals"/>
        /// </summary>
        public static bool SameReference(this object self, object target)
        {
            return ReferenceEquals(self, target);
        }
    }
}