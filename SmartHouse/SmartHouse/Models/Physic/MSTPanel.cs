﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    // Panel with Motion sensor and termostat
    [DeviceType(0x70)]
    public class MSTPanel: PDevice
    {
        public override string Icon { get => "pdevice_mstpanel.png"; }
        public override string TypeName { get => "Панель с датчиками"; }

        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public MSTPanel()
        {

        }

        /* public MSTPanel(UID id, int inputs, int outputs)
        {
            Init(id, inputs, outputs, true);
        } */

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new MSTPanel()).Assign(this);
        }
    }
}