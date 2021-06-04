using EquipmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentViewModel
    {

        private GOT_EquipmentContext _DBContext;
        public string SerialNo{ get; set; }
        
        public string GOTSerialNo { get; set; }

        public string SerialDescription { get; set; }

        public byte? EquipmentTypeID { get; set; }

        private string GetDescription(Models.EquipmentLookup eqplkup )
        {
            string Descdata = string.Empty;
            Descdata = (eqplkup.Status == true) ? Descdata + "Active " : Descdata + "Inactive";
            Descdata = (eqplkup.PurchaseDate == null) ? Descdata + "" : Descdata + "  Purchased On " + Convert.ToDateTime(eqplkup.PurchaseDate).ToString("yyyy-MM-dd");
            Descdata = (eqplkup.GrindProgram == true) ? Descdata + " In Grid Pgm" : Descdata + "";
            Descdata = (eqplkup.Os == null) ? Descdata + "" : Descdata + " OS is " + eqplkup.Os;
            Descdata = (eqplkup.Note== null ) ? Descdata + "" : Descdata +  " " +  eqplkup.Note;

            return Descdata;
        }

        public EquipmentViewModel()
        {
           
        }
        public EquipmentViewModel(GOT_EquipmentContext context)
        {
            this._DBContext = context;
        }
        public void CopyDataFromModel(Models.EquipmentLookup equipmentdm)
        {
            this.SerialNo= equipmentdm.SerialNo.Trim();
            this.SerialDescription = GetDescription(equipmentdm);
            this.EquipmentTypeID = equipmentdm.EquipmentType;

            if (equipmentdm.GotSerialNo != null)
            this.GOTSerialNo= equipmentdm.GotSerialNo.Trim();            
        }
        public bool LoadDataForGotSerialNo(string gotSerialNo)
        {
            EquipmentLookup eqlookup = this._DBContext.EquipmentLookup.Where(e => e.GotSerialNo == gotSerialNo).SingleOrDefault();
            if (eqlookup == null) return false;
            
            CopyDataFromModel(eqlookup);
            
            return true;
        }

        public bool LoadDataForSerialNo(string SerialNo)
        {
            EquipmentLookup eqlookup = this._DBContext.EquipmentLookup.Where(e => e.SerialNo == SerialNo).SingleOrDefault();
            if (eqlookup == null) return false;
            
            CopyDataFromModel(eqlookup);
            
            return true;
        }
    }
}
