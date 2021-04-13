using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Logic
{
    public interface IUnique<I>
    {
        I ID { get; set; }
    }
}
