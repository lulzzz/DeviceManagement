﻿using LagoVista.IoT.DeviceManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.DeviceManagement.Core.Repos
{
    public interface IDeviceLogRepo
    {
        Task AddLogEntryAsync(DeviceLog logEntry);

        Task<IEnumerable<DeviceLog>> GetForDateRangeAsync(string deviceId, int maxReturnCount, string start, string end);
    }
}