﻿using LagoVista.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.DeviceManagement.Core.Models
{

    public interface IPEMIndex
    {
        string DeviceId { get; set; }
        String MessageId { get; set; }
        String Status { get; set; }
        String MessageType { get; set; }
        String CreatedTimeStamp { get; set; }
        double TotalProcessingMS { get; set; }
        string JSON { get; set; }
    }
}
