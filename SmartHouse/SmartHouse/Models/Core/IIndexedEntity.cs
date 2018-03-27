using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Core
{
    public interface IUnique<I>
    {
        I ID { get; set; }
    }
}
