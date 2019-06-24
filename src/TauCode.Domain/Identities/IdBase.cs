using System;
using System.ComponentModel;

namespace TauCode.Domain.Identities
{
    [TypeConverter(typeof(IdTypeConverter))]
    [Serializable]
    public abstract class IdBase : IEquatable<IdBase>, IId
    {
        #region Constructors

        protected IdBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected IdBase(Guid id)
        {
            this.Id = id;
        }

        protected IdBase(string id)
        {
            this.Id = new Guid(id);
        }

        #endregion

        #region IEquatable<IdBase> Members

        public bool Equals(IdBase other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            return
                this.GetType() == other.GetType() &&
                this.Id.Equals(other.Id);
        }

        #endregion

        #region Overridden

        public override bool Equals(object another)
        {
            return this.Equals(another as IdBase);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() ^ this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        #endregion

        #region IId Members

        public Guid Id { get; }

        #endregion

        #region Operators

        public static bool operator ==(IdBase id1, IdBase id2)
        {
            return Equals(id1, id2);
        }

        public static bool operator !=(IdBase id1, IdBase id2)
        {
            return !Equals(id1, id2);
        }

        #endregion
    }
}
