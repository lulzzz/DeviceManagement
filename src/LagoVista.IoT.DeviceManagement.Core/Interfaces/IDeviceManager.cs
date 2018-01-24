﻿using LagoVista.Core.Models;
using LagoVista.Core.Models.Geo;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.Core.Validation;
using LagoVista.IoT.DeviceManagement.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagoVista.IoT.DeviceManagement.Core
{
    public interface IDeviceManager
    {
        Task<InvokeResult> UpdateDeviceAsync(DeviceRepository deviceRepo, Device device, EntityHeader org, EntityHeader user);

        /// <summary>
        /// Get the Device by the unique identifier that was generated for the device (not device id)
        /// </summary>
        /// <param name="deviceRepo">Repository connection data</param>
        /// <param name="id">Unique id for the device</param>
        /// <param name="org">User Org</param>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<Device> GetDeviceByIdAsync(DeviceRepository deviceRepo, string id, EntityHeader org, EntityHeader user, bool populateMetaData = false);

        /// <summary>
        /// Add a device note for a device
        /// </summary>
        /// <param name="deviceRepo"></param>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <param name="org"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<InvokeResult> AddNoteAsync(DeviceRepository deviceRepo, string id, DeviceNote note, EntityHeader org, EntityHeader user);

        /// <summary>
        /// Get the Device by the unique identifier that was generated for the device (not device id)
        /// </summary>
        /// <param name="deviceRepo">Repository connection data</param>
        /// <param name="id">Unique id for the device</param>
        /// <param name="status">Status of the device</param>
        /// <param name="org">User Org</param>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<InvokeResult> UpdateDeviceStatusAsync(DeviceRepository deviceRepo, string id, string status, EntityHeader org, EntityHeader user);

        /// <summary>
        /// Get the Device by the unique identifier that was generated for the device (not device id)
        /// </summary>
        /// <param name="deviceRepo">Repository connection data</param>
        /// <param name="id">Unique id for the device</param>
        /// <param name="customstatus">Custom defined status for the device</param>
        /// <param name="org">User Org</param>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<InvokeResult> UpdateDeviceCustomStatusAsync(DeviceRepository deviceRepo, string id, string customstatus, EntityHeader org, EntityHeader user);

        /// <summary>
        /// Get the Device by the unique identifier that was generated for the device (not device id)
        /// </summary>
        /// <param name="deviceRepo">Repository connection data</param>
        /// <param name="id">Unique id for the device</param>
        /// <param name="geoLocation">Geo Location of device</param>
        /// <param name="org">User Org</param>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<InvokeResult> UpdateGeoLocationAsync(DeviceRepository deviceRepo, string id, GeoLocation geoLocation, EntityHeader org, EntityHeader user);


        /// <summary>
        /// Get the Device by the given Device ID that was entered by the user, this is the one that the device will send.
        /// </summary>
        /// <param name="deviceRepo">Repository connection data</param>
        /// <param name="deviceId">Assigned Device Id</param>
        /// <param name="org">User Org</param>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        Task<Device> GetDeviceByDeviceIdAsync(DeviceRepository deviceRepo, string deviceId, EntityHeader org, EntityHeader user, bool populateMetaData = false);

        Task<InvokeResult> AddDeviceAsync(DeviceRepository deviceRepo, Device device, EntityHeader org, EntityHeader user);
        
        /// <summary>
        /// Delete the device id by the generated device id.
        /// </summary>
        /// <param name="deviceRepo"></param>
        /// <param name="id"></param>
        /// <param name="org"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<InvokeResult> DeleteDeviceAsync(DeviceRepository deviceRepo, string id, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesForOrgIdAsync(DeviceRepository deviceRepo, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesForLocationIdAsync(DeviceRepository deviceRepo, string locationId, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesInStatusAsync(DeviceRepository deviceRepo, string status, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesInCustomStatusAsync(DeviceRepository deviceRepo, string customStatus, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesWithConfigurationAsync(DeviceRepository deviceRepo, string deviceConfigId, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> GetDevicesWithDeviceTypeAsync(DeviceRepository deviceRepo, string deviceTypeId, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<DeviceSummary>> SearchByDeviceIdAsync(DeviceRepository deviceRepo, string searchString, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<ListResponse<Device>> GetFullDevicesWithConfigurationAsync(DeviceRepository deviceRepo, string configurationId, ListRequest listRequest, EntityHeader org, EntityHeader user);

        Task<DependentObjectCheckResult> CheckIfDeviceIdInUse(DeviceRepository deviceRepo, string id, EntityHeader org, EntityHeader user);
    }
}
