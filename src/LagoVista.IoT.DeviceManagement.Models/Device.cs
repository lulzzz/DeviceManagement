﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LagoVista.Core.Models;
using LagoVista.Core.Interfaces;
using LagoVista.Core.Validation;
using LagoVista.IoT.DeviceAdmin.Models;
using LagoVista.Core.Attributes;
using LagoVista.IoT.DeviceManagement.Core.Resources;
using Newtonsoft.Json;
using LagoVista.IoT.DeviceAdmin.Resources;
using LagoVista.IoT.DeviceManagement.Models.Resources;
using LagoVista.IoT.DeviceAdmin.Models.Resources;
using LagoVista.Core.Models.Geo;

namespace LagoVista.IoT.DeviceManagement.Core.Models
{
    public enum DeviceStates
    {
        [EnumLabel(Device.New, DeviceManagementResources.Names.Device_Status_New, typeof(DeviceManagementResources))]
        New,
        [EnumLabel(Device.Commissioned, DeviceManagementResources.Names.Device_Status_Commissioned, typeof(DeviceManagementResources))]
        Commissioned,
        [EnumLabel(Device.Ready, DeviceManagementResources.Names.Device_Status_Ready, typeof(DeviceManagementResources))]
        Ready,
        [EnumLabel(Device.Degraded, DeviceManagementResources.Names.Device_Status_Degraded, typeof(DeviceManagementResources))]
        Degraded,
        [EnumLabel(Device.PastDue, DeviceManagementResources.Names.Device_Status_PastDue, typeof(DeviceManagementResources))]
        PastDue,
        [EnumLabel(Device.Error, DeviceManagementResources.Names.Device_Status_Error, typeof(DeviceManagementResources))]
        Error,
        [EnumLabel(Device.Decommissioned, DeviceManagementResources.Names.Device_Status_Decommissioned, typeof(DeviceManagementResources))]
        Decommissioned

    }

    [EntityDescription(DeviceManagementDomain.DeviceManagement, DeviceManagementResources.Names.Device_Title, DeviceManagementResources.Names.Device_Help, DeviceManagementResources.Names.Device_Description, EntityDescriptionAttribute.EntityTypes.SimpleModel, typeof(DeviceManagementResources))]
    public class Device : IOwnedEntity, IIDEntity, IValidateable, INoSQLEntity, IAuditableEntity, IFormDescriptor, INamedEntity
    {
        public const string New = "new";
        public const string Commissioned = "commissioned";
        public const string Ready = "ready";
        public const string Degraded = "degraded";
        public const string PastDue = "pastdue";
        public const string Error = "error";
        public const string Decommissioned = "decommissioned";

        public Device()
        {
            Attributes = new List<AttributeValue>();
            PropertyBag = new Dictionary<string, object>();
            Properties = new List<AttributeValue>();
            States = new List<AttributeValue>();
            Status = new EntityHeader<DeviceStates>() { Value = DeviceStates.New, Id = Device.New, Text = DeviceManagementResources.Device_Status_New };
            Notes = new List<DeviceNote>();
        }

        public string DatabaseName { get; set; }
        public string EntityType { get; set; }
        public string CreationDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public EntityHeader CreatedBy { get; set; }
        public EntityHeader LastUpdatedBy { get; set; }


        public EntityHeader DeviceRepository { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_Status, EnumType: (typeof(DeviceStates)), FieldType: FieldTypes.Picker, ResourceType: typeof(DeviceManagementResources), WaterMark: DeviceManagementResources.Names.Device_Status_Select, IsRequired: true, IsUserEditable: true)]
        public EntityHeader<DeviceStates> Status { get; set; }

        [JsonProperty("id")]
        [FormField(LabelResource: DeviceLibraryResources.Names.Common_UniqueId, IsUserEditable: false, ResourceType: typeof(DeviceLibraryResources), IsRequired: true)]
        public string Id { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Common_Name, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsRequired: true)]
        public string Name { get; set; }


        /* Device ID is the ID associated with the device by the user, it generally will be unique, but can't assume it to be, it's primarily read only, it must however be unique for a device configuration. */
        [FormField(LabelResource: DeviceManagementResources.Names.Device_DeviceId, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsRequired: true)]
        public string DeviceId { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_DeviceConfiguration, FieldType: FieldTypes.EntityHeaderPicker, WaterMark: DeviceManagementResources.Names.Device_DeviceConfiguration_Select, ResourceType: typeof(DeviceManagementResources), IsRequired: true)]
        public EntityHeader DeviceConfiguration { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_DeviceType, FieldType: FieldTypes.EntityHeaderPicker, WaterMark: DeviceManagementResources.Names.Device_DeviceType_Select, ResourceType: typeof(DeviceManagementResources), IsRequired: true)]
        public EntityHeader DeviceType { get; set; }


        public bool IsPublic { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_Organization, EnumType: (typeof(DeviceStates)), FieldType: FieldTypes.EntityHeaderPicker, ResourceType: typeof(DeviceManagementResources), IsRequired: true, WaterMark: DeviceManagementResources.Names.Device_Organization_Select)]
        public EntityHeader OwnerOrganization { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_Location, EnumType: (typeof(DeviceStates)), FieldType: FieldTypes.EntityHeaderPicker, ResourceType: typeof(DeviceManagementResources), WaterMark: DeviceManagementResources.Names.Device_Location_Select)]
        public EntityHeader Location { get; set; }

        public EntityHeader OwnerUser { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_DeviceURI, HelpResource: DeviceManagementResources.Names.Device_DeviceURI_Help, FieldType: FieldTypes.Text, IsUserEditable: false, ResourceType: typeof(DeviceManagementResources))]
        public string DeviceURI { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_ShowDiagnostics, HelpResource: DeviceManagementResources.Names.Device_ShowDiagnostics_Help, FieldType: FieldTypes.CheckBox, ResourceType: typeof(DeviceManagementResources))]
        public string ShowDiagnostics { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_SerialNumber, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources))]
        public string SerialNumber { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_FirmwareVersion, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources))]
        public string FirmwareVersion { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_IsConnected, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsUserEditable: false)]
        public string IsConnected { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_LastContact, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsUserEditable: false)]
        public string LastContact { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_PrimaryKey, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: true)]
        public string PrimaryAccessKey { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_SecondaryKey, FieldType: FieldTypes.Text, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: true)]
        public string SecondaryAccessKey { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_GeoLocation, HelpResource: DeviceManagementResources.Names.Device_GeoLocation_Help, FieldType: FieldTypes.GeoLocation, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: false)]
        public GeoLocation GeoLocation { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_Heading, HelpResource: DeviceManagementResources.Names.Device_Heading_Help, FieldType: FieldTypes.Decimal, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: false)]
        public double Heading { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_Speed, HelpResource: DeviceManagementResources.Names.Device_Speed_Help, FieldType: FieldTypes.Decimal, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: false)]
        public double Speeed { get; set; }

        [FormField(LabelResource: DeviceManagementResources.Names.Device_CustomStatus, HelpResource: DeviceManagementResources.Names.Device_CustomStatus_Help, FieldType: FieldTypes.ChildItem, ResourceType: typeof(DeviceManagementResources), IsUserEditable: true, IsRequired: false)]
        public EntityHeader CustomStatus { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_DebugMode, HelpResource: DeviceManagementResources.Names.Device_DebugMode_Help, FieldType: FieldTypes.Bool, ResourceType: typeof(DeviceManagementResources), IsRequired: true)]
        public bool DebugMode { get; set; }

        /// <summary>
        /// Properties are design time/values added with device configuration
        /// </summary>
        [FormField(LabelResource: DeviceManagementResources.Names.Device_Properties, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_Properties_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<CustomField> PropertiesMetaData { get; set; }

        /// <summary>
        /// Properties are design time/values added with device configuration
        /// </summary>
        [FormField(LabelResource: DeviceManagementResources.Names.Device_AttributeMetaData, FieldType: FieldTypes.ChildList, ResourceType: typeof(DeviceManagementResources))]
        public List<DeviceAdmin.Models.Attribute> AttributeMetaData { get; set; }

        /// <summary>
        /// Properties are design time/values added with device configuration
        /// </summary>
        [FormField(LabelResource: DeviceManagementResources.Names.Device_StateMachineMetaData, FieldType: FieldTypes.ChildList, ResourceType: typeof(DeviceManagementResources))]
        public List<StateMachine> StateMachineMetaData { get; set; }

        public Dictionary<string, object> PropertyBag { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_States, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_States_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<AttributeValue> Properties { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_States, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_States_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<AttributeValue> States { get; set; }

        /// <summary>
        /// Attributes are values that have been set by message or workflow
        /// </summary>
        [FormField(LabelResource: DeviceManagementResources.Names.Device_Attributes, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_Attributes_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<AttributeValue> Attributes { get; set; }

        /// <summary>
        /// Values as pull from the most recent messages
        /// </summary>
        [FormField(LabelResource: DeviceManagementResources.Names.Device_MessageValues, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_MessageValues_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<AttributeValue> MessageValues { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_inputCommandEndPoints, FieldType: FieldTypes.ChildList, HelpResource: DeviceManagementResources.Names.Device_inputCommandEndPoints_Help, ResourceType: typeof(DeviceManagementResources))]
        public List<InputCommandEndPoint> InputCommandEndPoints { get; set; }


        [FormField(LabelResource: DeviceManagementResources.Names.Device_Notes, FieldType: FieldTypes.ChildList, ResourceType: typeof(DeviceManagementResources))]
        public List<DeviceNote> Notes { get; set; }

        public DeviceSummary CreateSummary()
        {
            return new DeviceSummary()
            {
                Id = this.Id,
                DeviceName = string.IsNullOrEmpty(this.Name) ? this.DeviceId : this.Name,
                DeviceId = this.DeviceId,
                SerialNumber = SerialNumber,
                DeviceConfiguration = DeviceConfiguration.Text,
                DeviceConfigurationId = DeviceConfiguration.Id,
                Status = Status.Text,
                DeviceType = DeviceType.Text,
                DeviceTypeId = DeviceType.Id,
                CustomStatus = CustomStatus,
                GeoLocation = GeoLocation,
            };
        }

        public List<string> GetFormFields()
        {
            return new List<string>()
            {
                nameof(Device.Name),
                nameof(Device.DeviceId),
                nameof(Device.SerialNumber),
                nameof(Device.Status),
                nameof(Device.DeviceType),
                nameof(Device.DeviceConfiguration)
            };
        }

        public EntityHeader<Device> ToEntityHeader()
        {
            return new EntityHeader<Device>()
            {
                Id = Id,
                Text = DeviceId,
                Value = this
            };
        }

        [CustomValidator]
        public void Validate(ValidationResult result, Actions action)
        {
            if(PropertiesMetaData != null)
            {
                foreach(var propertyMetaData in PropertiesMetaData)
                {
                    var property = Properties.Where(prop => prop.Key == propertyMetaData.Key).FirstOrDefault();
                    propertyMetaData.Validate(property?.Value, result, action);
                }
            }
        }
    }

    public class DeviceSummary
    {
        public string Id { get; set; }
        public string DeviceConfiguration { get; set; }
        public string DeviceConfigurationId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string DeviceTypeId { get; set; }
        public string DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }

        public GeoLocation GeoLocation { get; set; }

        public EntityHeader CustomStatus { get; set; }
    }
}
