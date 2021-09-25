﻿using Newtonsoft.Json;
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

    public class BaseEntity
    {

        public static explicit operator string(BaseEntity e)
        {
            return String.Format("(ID={0}, Sec={1})", e.ID, e.SecurityLevel);
        }

        public virtual int ID { get; set; }

        public virtual byte SecurityLevel { get; set; }

        public BaseEntity()
        {

        }

        public BaseEntity(int id)
        {
            this.ID = id;
        }

        public virtual void Assign(BaseEntity source)
        {
            this.ID = source.ID;
            this.SecurityLevel = source.SecurityLevel;
        }

        public virtual BaseEntity Clone()
        {
            throw new Exception("Not implemented");
        }

    }
}