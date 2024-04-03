using System;
using System.Diagnostics;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Checks whether <see cref="object" /> is null or not, also knows a destroyed one is null too.
        /// </summary>
        /// <param name="self"><see cref="object" /> to check</param>
        /// <returns>true is null</returns>
        [DebuggerHidden]
        public static bool IsNull(this object self)
        {
            return self is null || self.Equals(null);
        }

        /// <summary>
        ///     Inverted version of <see cref="IsNull" />
        /// </summary>
        /// <param name="self"><see cref="object" /> to check</param>
        /// <returns>true is not null</returns>
        [DebuggerHidden]
        public static bool IsObject(this object self)
        {
            return self is not null && !self.Equals(null);
        }

        /// <summary>
        ///     Chains <see cref="Action{T}" /> with null or destroyed checking <see cref="IsObject" />
        /// </summary>
        /// <param name="self">to check and apply</param>
        /// <param name="apply">the apply <see cref="Action{T}" /></param>
        /// <typeparam name="T"><see cref="Type" /> of <see cref="self" /></typeparam>
        /// <returns>
        ///     <see cref="self" />
        /// </returns>
        [DebuggerHidden]
        public static T Apply<T>(this T self, Action<T> apply)
        {
            if (self.IsObject()) apply?.Invoke(self);
            return self;
        }

        /// <summary>
        ///     Same as <see cref="object.ReferenceEquals" />
        /// </summary>
        [DebuggerHidden]
        public static bool SameReference(this object self, object target)
        {
            return ReferenceEquals(self, target);
        }

        /// <summary>
        ///     Tries to cast <see cref="self" /> to <see cref="T" />
        /// </summary>
        /// <param name="self">the cast source</param>
        /// <param name="result">the cast result</param>
        /// <typeparam name="T">type of <see cref="result" /></typeparam>
        [DebuggerHidden]
        public static void OfType<T>(this object self, out T result)
        {
            if (self is T t) result = t;
            result = (T)self;
        }

        /// <summary>
        ///     Tries to cast <see cref="self" /> to <see cref="T" />
        /// </summary>
        /// <param name="self">the cast source</param>
        /// <param name="result">the cast result</param>
        /// <typeparam name="T">type of <see cref="result" /></typeparam>
        /// <returns>passed or failed</returns>
        [DebuggerHidden]
        public static bool TryCast<T>(this object self, out T result)
        {
            try
            {
                self.OfType(out result);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}