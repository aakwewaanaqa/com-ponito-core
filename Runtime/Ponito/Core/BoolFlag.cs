using System;

namespace Ponito.Core
{
    [Serializable]
    public struct BoolFlag : IEquatable<bool>, IEquatable<BoolFlag>
    {
        private enum FlagType
        {
            Nether = -1,
            Both   = 0,
            True   = 1,
            False  = 2,
        }

        private FlagType flag;

        private BoolFlag(FlagType flag = FlagType.Both)
        {
            this.flag = flag;
        }

        public static BoolFlag True   => new BoolFlag(FlagType.True);
        public static BoolFlag False  => new BoolFlag(FlagType.False);
        public static BoolFlag Both   => new BoolFlag(FlagType.Both);
        public static BoolFlag Nether => new BoolFlag(FlagType.Nether);

        public static bool operator ==(BoolFlag f, bool b)
        {
            return f.Equals(b);
        }

        public static bool operator !=(BoolFlag f, bool b)
        {
            return !f.Equals(b);
        }

        public static bool operator ==(bool b, BoolFlag f)
        {
            return f.Equals(b);
        }

        public static bool operator !=(bool b, BoolFlag f)
        {
            return !f.Equals(b);
        }

        public static implicit operator BoolFlag(bool b)
        {
            return b ? True : False;
        }

        public bool Equals(bool other)
        {
            return other ? flag is FlagType.True or FlagType.Both : flag is FlagType.False or FlagType.Both;
        }

        public bool Equals(BoolFlag other)
        {
            return flag == other.flag;
        }

        public override bool Equals(object obj)
        {
            return obj is BoolFlag other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)flag;
        }
    }
}