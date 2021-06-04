using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EquipmentAPI.Models
{
    public partial class GOT_EquipmentContext : DbContext
    {
        public GOT_EquipmentContext()
        {
        }

        public GOT_EquipmentContext(DbContextOptions<GOT_EquipmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EquipmentCalls> EquipmentCalls { get; set; }
        public virtual DbSet<EquipmentCheckList> EquipmentCheckList { get; set; }
        public virtual DbSet<EquipmentChecklistMapping> EquipmentChecklistMapping { get; set; }
        public virtual DbSet<EquipmentFilterDate> EquipmentFilterDate { get; set; }
        public virtual DbSet<EquipmentLocation> EquipmentLocation { get; set; }
        public virtual DbSet<EquipmentLookup> EquipmentLookup { get; set; }
        public virtual DbSet<EquipmentNeedToSend> EquipmentNeedToSend { get; set; }
        public virtual DbSet<EquipmentNeedToSendChecklist> EquipmentNeedToSendChecklist { get; set; }
        public virtual DbSet<EquipmentNeedToSendHeader> EquipmentNeedToSendHeader { get; set; }
        public virtual DbSet<EquipmentStoreStaticIpAddress> EquipmentStoreStaticIpAddress { get; set; }
        public virtual DbSet<EquipmentTransaction> EquipmentTransaction { get; set; }
        public virtual DbSet<EquipmentTransactionCheckList> EquipmentTransactionCheckList { get; set; }
        public virtual DbSet<EquipmentTransactionReason> EquipmentTransactionReason { get; set; }
        public virtual DbSet<EquipmentTransactionType> EquipmentTransactionType { get; set; }
        public virtual DbSet<EquipmentTurnOff> EquipmentTurnOff { get; set; }
        public virtual DbSet<EquipmentTurnOffStore> EquipmentTurnOffStore { get; set; }
        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<EquipmentUpsDashBoardWaringDate> EquipmentUpsDashBoardWaringDate { get; set; }
        public virtual DbSet<EquipmentUpsDashBoardWarningDateLog> EquipmentUpsDashBoardWarningDateLog { get; set; }
        public virtual DbSet<EquipmentUpsReportDaysSetup> EquipmentUpsReportDaysSetup { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<StatusLookup> StatusLookup { get; set; }
        public virtual DbSet<StoreInfo> StoreInfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=localhost;Database=GOT_Equipment;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Server=192.168.10.73;Database=GOT_Equipment;User Id= Got; Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentCalls>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B19602552811A1");

                entity.ToTable("Equipment_Calls");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CallDate)
                    .HasColumnName("Call_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.CallMessage)
                    .HasColumnName("Call_Message")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CallMessage1)
                    .HasColumnName("Call_Message1")
                    .HasMaxLength(2000)
                    .IsFixedLength();

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentCheckList>(entity =>
            {
                entity.ToTable("Equipment_CheckList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckListName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentChecklistMapping>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Equipment_Checklist_Mapping");

                entity.HasIndex(e => new { e.TransactionScreen, e.EquipmentTypeId, e.CheckListId })
                    .HasName("IX_Equipment_Checklist_Mapping")
                    .IsUnique();

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.CheckListId).HasColumnName("CheckListID");

                entity.Property(e => e.EquipmentTypeId).HasColumnName("EquipmentTypeID");

                entity.Property(e => e.TransactionScreen)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentFilterDate>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B1960241E8BE98");

                entity.ToTable("Equipment_FilterDate");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FilterDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");
            });

            modelBuilder.Entity<EquipmentLocation>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B196027D12F27E");

                entity.ToTable("Equipment_Location");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndService)
                    .HasColumnName("End_Service")
                    .HasColumnType("datetime");

                entity.Property(e => e.EquipmentType).HasColumnName("Equipment_Type");

                entity.Property(e => e.Location)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StartService)
                    .HasColumnName("Start_Service")
                    .HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");
            });

            modelBuilder.Entity<EquipmentLookup>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B1960221EC0317");

                entity.ToTable("Equipment_Lookup");

                entity.HasIndex(e => e.GotSerialNo);

                entity.HasIndex(e => e.SerialNo);

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.EquipmentType).HasColumnName("Equipment_Type");

                entity.Property(e => e.ExtendedWarrantyEndDate)
                    .HasColumnName("Extended_Warranty_End_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.ExtendedWarrantyStartDate)
                    .HasColumnName("Extended_Warranty_Start_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.ExtendedWarrantyType).HasColumnName("Extended_Warranty_Type");

                entity.Property(e => e.GotSerialNo)
                    .HasColumnName("GOT_Serial_No")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GrindProgram).HasColumnName("Grind_Program");

                entity.Property(e => e.MenuId).HasColumnName("Menu_ID");

                entity.Property(e => e.Note)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Os)
                    .HasColumnName("OS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("Purchase_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("Update_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.WarrantyEndDate)
                    .HasColumnName("Warranty_End_Date")
                    .HasColumnType("smalldatetime");

                //Mapping
                /*
                modelBuilder.Entity<EquipmentLookup>()
                    .HasOne(a => a.eqType)
                    .WithOne(b => b.eqLookup)
                    .HasForeignKey<EquipmentType>(b => b.eqLookup);
                */
                    
                    

            });

            modelBuilder.Entity<EquipmentNeedToSend>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B196027C4E3730");

                entity.ToTable("Equipment_Need_To_Send");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DateToSend)
                    .HasColumnName("Date_to_Send")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.EquipmentType).HasColumnName("Equipment_Type");

                entity.Property(e => e.NeedToSendHeaderId).HasColumnName("Need_To_Send_HeaderID");

                entity.Property(e => e.Note)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentNeedToSendChecklist>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_Equipment_Need_ToSend_Checklist");

                entity.ToTable("Equipment_Need_To_Send_Checklist");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.CheckListId).HasColumnName("CheckListID");

                entity.Property(e => e.NeedToSendId).HasColumnName("NeedToSendID");

                entity.Property(e => e.Note)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentNeedToSendHeader>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Equipment_Need_To_Send_Header");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.ChecklistCompletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessingStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingLabel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TotalWeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentStoreStaticIpAddress>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B19602DBB191CF");

                entity.ToTable("Equipment_Store_Static_IP_Address");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Gateway)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasColumnName("IP_Address")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubnetMask)
                    .HasColumnName("Subnet_Mask")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EquipmentTransaction>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B1960270CC7C94");

                entity.ToTable("Equipment_Transaction");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonId).HasColumnName("Reason_ID");

                entity.Property(e => e.RecordType).HasColumnName("Record_Type");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo1)
                    .HasColumnName("Serial_No1")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentTransactionCheckList>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("Equipment_Transaction_CheckList");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.ChecklistId).HasColumnName("ChecklistID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            });

            modelBuilder.Entity<EquipmentTransactionReason>(entity =>
            {
                entity.ToTable("Equipment_Transaction_Reason");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RecordType).HasColumnName("Record_Type");
            });

            modelBuilder.Entity<EquipmentTransactionType>(entity =>
            {
                entity.ToTable("Equipment_Transaction_Type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InGot).HasColumnName("In_Got");

                entity.Property(e => e.InJanam).HasColumnName("In_Janam");

                entity.Property(e => e.InStore).HasColumnName("In_Store");

                entity.Property(e => e.InTransit).HasColumnName("In_Transit");
            });

            modelBuilder.Entity<EquipmentTurnOff>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B19602958811B8");

                entity.ToTable("Equipment_Turn_Off");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasColumnName("Serial_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.TurnOffDate)
                    .HasColumnName("Turn_Off_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.TurnOnDate)
                    .HasColumnName("Turn_On_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentTurnOffStore>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B1960240F7617A");

                entity.ToTable("Equipment_Turn_Off_Store");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.FileId).HasColumnName("File_ID");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.TrackingNumber)
                    .HasColumnName("Tracking_Number")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.TurnOffDate)
                    .HasColumnName("Turn_Off_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.TurnOnDate)
                    .HasColumnName("Turn_On_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.ToTable("Equipment_Type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");

                entity.Property(e => e.Uid).HasColumnName("UID");
            });

            modelBuilder.Entity<EquipmentUpsDashBoardWaringDate>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B196022A84B756");

                entity.ToTable("Equipment_UPS_DashBoard_Waring_Date");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentUpsDashBoardWarningDateLog>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Equipmen__C5B1960253E01E54");

                entity.ToTable("Equipment_UPS_DashBoard_Warning_Date_Log");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<EquipmentUpsReportDaysSetup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Equipment_UPS_Report_Days_Setup");

                entity.Property(e => e.CheckName)
                    .HasColumnName("Check_Name")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaxDays).HasColumnName("Max_Days");

                entity.Property(e => e.ReportDays).HasColumnName("Report_Days");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Error_Lo__C5B1960245759962");

                entity.ToTable("Error_Log");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BrowserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrowserVersion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("Error_Message")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorSource)
                    .HasColumnName("Error_Source")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionStackTrace).IsUnicode(false);

                entity.Property(e => e.FunctionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("ipaddress")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RequestData).IsUnicode(false);
            });

            modelBuilder.Entity<StatusLookup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Status_Lookup");

                entity.Property(e => e.Description)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.Uid).HasColumnName("UID");
            });

            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Store_Info");

                entity.Property(e => e.AccaPw)
                    .HasColumnName("ACCA_PW")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AccaUserId)
                    .HasColumnName("ACCA_UserID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AccaVersion).HasColumnName("ACCA_Version");

                entity.Property(e => e.AdTypeGroupId).HasColumnName("Ad_Type_Group_ID");

                entity.Property(e => e.AddDate)
                    .HasColumnName("Add_Date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AeRoute)
                    .HasColumnName("AE_Route")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AreaCode2NotusedOld).HasColumnName("Area_Code2_NOTUSED_OLD");

                entity.Property(e => e.AwgCalculateMaxQtyByDepartment).HasColumnName("AWG_Calculate_Max_Qty_byDepartment");

                entity.Property(e => e.AwgMaxQtyPerDelivery).HasColumnName("AWG_Max_Qty_Per_Delivery");

                entity.Property(e => e.BillTo).HasColumnName("Bill_to");

                entity.Property(e => e.BlockAllVendors).HasColumnName("Block_All_Vendors");

                entity.Property(e => e.BuildAcca).HasColumnName("Build_ACCA");

                entity.Property(e => e.BuildCredit).HasColumnName("Build_Credit");

                entity.Property(e => e.BuildFile).HasColumnName("Build_File");

                entity.Property(e => e.BuildOmniFilesBaketNumber).HasColumnName("Build_Omni_Files_Baket_Number");

                entity.Property(e => e.BuildPlacementFile).HasColumnName("Build_Placement_File");

                entity.Property(e => e.CPrimarySoftwareVersion)
                    .HasColumnName("cPrimary_Software_Version")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CSecondarySoftwareVersion)
                    .HasColumnName("cSecondary_Software_Version")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Check2dPriceOnPlacement).HasColumnName("Check_2D_Price_OnPlacement");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CommServer).HasColumnName("Comm_Server");

                entity.Property(e => e.ContactEmail)
                    .HasColumnName("Contact_Email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactFax)
                    .HasColumnName("Contact_Fax")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasColumnName("Contact_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .HasColumnName("Contact_Phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCulture).HasColumnName("Customer_Culture");

                entity.Property(e => e.CustomerMilk).HasColumnName("Customer_Milk");

                entity.Property(e => e.Cv19ErrorCode).HasColumnName("CV19_ErrorCode");

                entity.Property(e => e.DeliBakeryId).HasColumnName("Deli_Bakery_ID");

                entity.Property(e => e.DeliveryChargePercent)
                    .HasColumnName("Delivery_Charge_Percent")
                    .HasColumnType("numeric(4, 2)");

                entity.Property(e => e.DeliveryDateEnd)
                    .HasColumnName("DeliveryDate_End")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DeliveryDateStart)
                    .HasColumnName("DeliveryDate_Start")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DisableMilkScreen).HasColumnName("Disable_Milk_Screen");

                entity.Property(e => e.DistrictManagerId).HasColumnName("District_Manager_ID");

                entity.Property(e => e.DistrictManagerId2).HasColumnName("District_Manager_ID2");

                entity.Property(e => e.DistrictManagerId3).HasColumnName("District_Manager_ID3");

                entity.Property(e => e.DistrictManagerId4).HasColumnName("District_Manager_ID4");

                entity.Property(e => e.DistrictManagerId5).HasColumnName("District_Manager_ID5");

                entity.Property(e => e.DoWhRouting).HasColumnName("Do_WH_Routing");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Encrypted2d).HasColumnName("Encrypted_2D");

                entity.Property(e => e.EnterCreditsOldWay).HasColumnName("Enter_Credits_Old_Way");

                entity.Property(e => e.EstCostPercent)
                    .HasColumnName("Est_Cost_Percent")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.EstRetailPercent)
                    .HasColumnName("Est_Retail_Percent")
                    .HasColumnType("numeric(6, 2)");

                entity.Property(e => e.Fax)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirmwareVersion)
                    .HasColumnName("Firmware_Version")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GameCanPlay).HasColumnName("Game_CanPlay");

                entity.Property(e => e.GameId).HasColumnName("Game_ID");

                entity.Property(e => e.GrindProgram).HasColumnName("Grind_Program");

                entity.Property(e => e.GroceryId).HasColumnName("Grocery_ID");

                entity.Property(e => e.GroupStoreUid).HasColumnName("GroupStore_UID");

                entity.Property(e => e.HasJanamUnit).HasColumnName("Has_Janam_Unit");

                entity.Property(e => e.HasStaticIp).HasColumnName("Has_Static_IP");

                entity.Property(e => e.IPrimaryListenerId).HasColumnName("iPrimary_Listener_ID");

                entity.Property(e => e.ISecondaryListenerId).HasColumnName("iSecondary_Listener_ID");

                entity.Property(e => e.InvoiceMovementLabel).HasColumnName("Invoice_Movement_Label");

                entity.Property(e => e.InvoiceOosErrorCode).HasColumnName("Invoice_OOS_ErrorCode");

                entity.Property(e => e.IoeDays).HasColumnName("IOE_Days");

                entity.Property(e => e.IoeFilePath)
                    .HasColumnName("IOE_File_Path")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KindId).HasColumnName("Kind_ID");

                entity.Property(e => e.LoadDatFile).HasColumnName("Load_dat_file");

                entity.Property(e => e.LoadMovement).HasColumnName("Load_Movement");

                entity.Property(e => e.LoadProof).HasColumnName("Load_Proof");

                entity.Property(e => e.LoadToPackDirty).HasColumnName("Load_To_Pack_Dirty");

                entity.Property(e => e.LoginGroupId).HasColumnName("Login_Group_ID");

                entity.Property(e => e.LongDistance2NotusedOld).HasColumnName("Long_Distance2_NOTUSED_OLD");

                entity.Property(e => e.LoqPercent).HasColumnName("LOQ_Percent");

                entity.Property(e => e.MasterStoreInZone).HasColumnName("Master_Store_In_Zone");

                entity.Property(e => e.MessageOos).HasColumnName("Message_OOS");

                entity.Property(e => e.MilkAddOnCnt).HasColumnName("Milk_AddOn_Cnt");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OrderCv19).HasColumnName("Order_CV19");

                entity.Property(e => e.OrderOos).HasColumnName("Order_OOS");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber1NotusedOld)
                    .HasColumnName("Phone_Number1_NOTUSED_OLD")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PhoneNumber2NotusedOld)
                    .HasColumnName("Phone_Number2_NOTUSED_OLD")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PriceStoreId).HasColumnName("Price_Store_ID");

                entity.Property(e => e.PriceZoneId).HasColumnName("Price_Zone_ID");

                entity.Property(e => e.PullFileGroup).HasColumnName("Pull_File_Group");

                entity.Property(e => e.PullFilePassword)
                    .HasColumnName("Pull_File_Password")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PullFileUserId)
                    .HasColumnName("Pull_File_UserID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SetCost).HasColumnName("Set_Cost");

                entity.Property(e => e.SetDepartment).HasColumnName("Set_Department");

                entity.Property(e => e.SetRetail).HasColumnName("Set_Retail");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StaticIpList)
                    .HasColumnName("Static_IP_List")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("Store_ID");

                entity.Property(e => e.StoreId1).HasColumnName("Store_ID1");

                entity.Property(e => e.StoreName)
                    .HasColumnName("Store_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StoreNameSave)
                    .HasColumnName("Store_Name_Save")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubGroupId).HasColumnName("SubGroup_ID");

                entity.Property(e => e.TagOptionOnFileLoad).HasColumnName("TagOption_OnFileLoad");

                entity.Property(e => e.TagOptionOnOrder).HasColumnName("TagOption_OnOrder");

                entity.Property(e => e.TagOptionOnOrderTag).HasColumnName("TagOption_OnOrderTag");

                entity.Property(e => e.TagOptionOnPlacement).HasColumnName("TagOption_OnPlacement");

                entity.Property(e => e.TrainingOn).HasColumnName("Training_On");

                entity.Property(e => e.TxPhone1)
                    .HasColumnName("txPhone1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TxPhone2)
                    .HasColumnName("txPhone2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.UpdatePlacement).HasColumnName("Update_Placement");

                entity.Property(e => e.UseDefaultCommodity).HasColumnName("Use_Default_Commodity");

                entity.Property(e => e.UseLocation).HasColumnName("Use_Location");

                entity.Property(e => e.UseLoqCase).HasColumnName("Use_LOQ_Case");

                entity.Property(e => e.VmcCalculateMaxQtyByDepartment).HasColumnName("VMC_Calculate_Max_Qty_byDepartment");

                entity.Property(e => e.VmcMaxQtyPerDelivery).HasColumnName("VMC_Max_Qty_Per_Delivery");

                entity.Property(e => e.WsOrderx3).HasColumnName("ws_orderx3");

                entity.Property(e => e.Zip)
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__206D91906C6EFA6E");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentIncomingList)
                    .HasColumnName("Department_Incoming_List")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.MenuId).HasColumnName("Menu_ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Short)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SwitchToUserId).HasColumnName("Switch_To_User_ID");

                entity.Property(e => e.SystemUserName)
                    .HasColumnName("System_USer_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrackLogin).HasColumnName("Track_login");

                entity.Property(e => e.UserName)
                    .HasColumnName("User_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("Vendor_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
