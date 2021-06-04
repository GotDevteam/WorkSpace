using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class StoreInfo
    {
        [Key]
        public int StoreId { get; set; }

        public int Uid { get; set; }
        
        public int? StoreId1 { get; set; }
        public bool? LoadDatFile { get; set; }
        public bool? BuildFile { get; set; }
        public bool? BuildAcca { get; set; }
        public short? GroupStoreUid { get; set; }
        public int? SubGroupId { get; set; }
        public short? AdTypeGroupId { get; set; }
        public short? DistrictManagerId { get; set; }
        public short? KindId { get; set; }
        public short? LoginGroupId { get; set; }
        public string StoreName { get; set; }
        public bool? LoadToPackDirty { get; set; }
        public bool? UseDefaultCommodity { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public byte? NextDays { get; set; }
        public byte? Status { get; set; }
        public bool? SetRetail { get; set; }
        public bool? SetCost { get; set; }
        public bool? SetDepartment { get; set; }
        public string TxPhone1 { get; set; }
        public string TxPhone2 { get; set; }
        public string IoeFilePath { get; set; }
        public int? IoeDays { get; set; }
        public string CPrimarySoftwareVersion { get; set; }
        public int? IPrimaryListenerId { get; set; }
        public string CSecondarySoftwareVersion { get; set; }
        public int? ISecondaryListenerId { get; set; }
        public byte? LoqPercent { get; set; }
        public bool? UseLocation { get; set; }
        public bool? Rotate { get; set; }
        public bool? DontMoveDeliveryDate { get; set; }
        public DateTime? DeliveryDateStart { get; set; }
        public DateTime? DeliveryDateEnd { get; set; }
        public bool? UseLoqCase { get; set; }
        public bool? GameCanPlay { get; set; }
        public string AeRoute { get; set; }
        public int? CustomerMilk { get; set; }
        public int? CustomerCulture { get; set; }
        public decimal? PhoneNumber1NotusedOld { get; set; }
        public byte? BuildOmniFilesBaketNumber { get; set; }
        public byte? LongDistance2NotusedOld { get; set; }
        public int? AreaCode2NotusedOld { get; set; }
        public decimal? PhoneNumber2NotusedOld { get; set; }
        public byte? Days { get; set; }
        public byte? Retry { get; set; }
        public decimal? DeliveryChargePercent { get; set; }
        public string PullFileUserId { get; set; }
        public string PullFilePassword { get; set; }
        public int? PullFileGroup { get; set; }
        public byte? AccaVersion { get; set; }
        public byte? PriceZoneId { get; set; }
        public bool? MasterStoreInZone { get; set; }
        public byte? GameId { get; set; }
        public bool? BlockAllVendors { get; set; }
        public bool? DoWhRouting { get; set; }
        public string StoreNameSave { get; set; }
        public bool? Autoload { get; set; }
        public bool? HasJanamUnit { get; set; }
        public byte? TagOptionOnOrder { get; set; }
        public byte? TagOptionOnOrderTag { get; set; }
        public byte? TagOptionOnFileLoad { get; set; }
        public byte? TagOptionOnPlacement { get; set; }
        public int? GroceryId { get; set; }
        public int? DeliBakeryId { get; set; }
        public bool? LoadMovement { get; set; }
        public bool? Check2dPriceOnPlacement { get; set; }
        public bool? MessageOos { get; set; }
        public int? PriceStoreId { get; set; }
        public string FirmwareVersion { get; set; }
        public string Note { get; set; }
        public bool? InvoiceMovementLabel { get; set; }
        public bool? DisableMilkScreen { get; set; }
        public string AccaUserId { get; set; }
        public string AccaPw { get; set; }
        public byte? MilkAddOnCnt { get; set; }
        public bool? Encrypted2d { get; set; }
        public decimal? EstCostPercent { get; set; }
        public decimal? EstRetailPercent { get; set; }
        public byte? CommServer { get; set; }
        public bool? GrindProgram { get; set; }
        public bool? TrainingOn { get; set; }
        public byte? BillTo { get; set; }
        public short? DistrictManagerId2 { get; set; }
        public short? DistrictManagerId3 { get; set; }
        public short? DistrictManagerId4 { get; set; }
        public short? DistrictManagerId5 { get; set; }
        public byte? WsOrderx3 { get; set; }
        public bool? BuildCredit { get; set; }
        public bool? LoadProof { get; set; }
        public bool? Markdown { get; set; }
        public bool? UpdatePlacement { get; set; }
        public DateTime? AddDate { get; set; }
        public bool? HasStaticIp { get; set; }
        public string StaticIpList { get; set; }
        public bool? BuildPlacementFile { get; set; }
        public int? AwgMaxQtyPerDelivery { get; set; }
        public bool? AwgCalculateMaxQtyByDepartment { get; set; }
        public int? VmcMaxQtyPerDelivery { get; set; }
        public bool? VmcCalculateMaxQtyByDepartment { get; set; }
        public bool? OrderOos { get; set; }
        public bool? OrderCv19 { get; set; }
        public bool? Cv19ErrorCode { get; set; }
        public bool? InvoiceOosErrorCode { get; set; }
        public bool? EnterCreditsOldWay { get; set; }
    }
}
