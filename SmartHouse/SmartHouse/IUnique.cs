using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public interface IUnique<I>
    {
        I ID { get; set; }
    }
}
