using Newtonsoft.Json;
using SmartHouse.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace SmartHouse.Models
{
    [Serializable]

    // public class BaseEntity<IDType> : BaseObject, IUnique<IDType>
    public class BaseEntity: BaseObject, IUnique<int>
    {
        /* private static Dictionary<Type, object> ids = new Dictionary<Type, object>();
        public static IDType NewID<IDType>()
        {
            Type t = typeof(IDType);
            if (ids.ContainsKey(t))
            {
                IConvertible v = (IDType)ids[t] as IConvertible;
                int iv = v.ToInt32(null);
                iv++;
                ids[t] = iv;
                return (IDType)iv;
            }
        } */

        public static IDGenerator<int> IntID = new IDGenerator<int>((v) => { return (int)v + 1; });
        public static IDGenerator<UID> UIDID = new IDGenerator<UID>((v) => { int i = (int)(UID)v; i++;  return new UID(i); });

        // public static explicit operator string(BaseEntity<IDType> e)
        public static explicit operator string(BaseEntity e)
        {
            return String.Format("(ID={0}, Sec={1})", e.ID, e.SecurityLevel);
        }

        [JsonIgnore]
        public Boolean IsAdmin { get { return Settings.Instance.IsAdmin; } }
        [JsonIgnore]
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

        [JsonIgnore]
        // private IDType id;
        private int id;

        [JsonProperty(PropertyName = "ID")]
        // public virtual IDType ID
        public virtual int ID
        {
            get { return id; }
            set {
                CheckIsDirty(id, value, "ID", () => { id = value; });
            }
        }


        [JsonIgnore]
        private string stringId = null;

        [JsonIgnore]
        public virtual string StringID
        {
            get {
                if (stringId == null)
                    stringId = ID.ToString();
                return stringId;
            }
            set {
                CheckIsDirty(stringId, value, "StringID", () => { stringId = value; });
            }
        }

        [JsonIgnore]
        private byte securityLevel;

        [JsonProperty(PropertyName = "SecurityLevel")]
        public virtual byte SecurityLevel
        {
            get { return securityLevel; }
            set {
                CheckIsDirty(securityLevel, value, "SecurityLevel", () => { securityLevel = value; });
            }
        }

        public BaseEntity()
        {

        }

        // public BaseEntity(IDType id)
        public BaseEntity(int id)
        {
            this.ID = id;
        }

        // public virtual void Assign(BaseEntity<IDType> source)
        public virtual void Assign(BaseEntity source)
        {
            this.ID = source.ID;
            this.SecurityLevel = source.SecurityLevel;
        }

        // public virtual BaseEntity<IDType> Clone()
        public virtual BaseEntity Clone()
        {
            throw new Exception("Not implemented");
        }

    }
}
