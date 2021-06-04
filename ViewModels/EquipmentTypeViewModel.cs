using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{
    public class EquipmentTypeViewModel
    {
        private Models.GOT_EquipmentContext _DBContext;
        public string Code { get; set; }
        public string Description { get; set; }

        public EquipmentTypeViewModel()
        {
           
        }

        public EquipmentTypeViewModel(Models.GOT_EquipmentContext dbcontext)
        {
            this._DBContext = dbcontext;
        }
        public EquipmentTypeViewModel(string code, string description )
        {
            this.Code = code;
            this.Description = description;
        }

        public bool LoadDataForID(byte? equipmentId)

        {
            

            Models.EquipmentType eqtype = this._DBContext.EquipmentType.Find(equipmentId);
            if (eqtype == null || eqtype.Id == 0)
                return false;

            this.Code = eqtype.Id.ToString();
            this.Description = eqtype.Description;

            return true;
        }

        public bool LoadDataForGotSerialNo(string gotSerialNo)
        {

            short? equipmentType = this._DBContext.EquipmentLookup.Where(e => e.SerialNo == gotSerialNo).FirstOrDefault().EquipmentType;

            if (equipmentType == null || equipmentType == 0)
                return false;

            
                Models.EquipmentType eqtype = this._DBContext.EquipmentType.Find(equipmentType);
                this.Code = eqtype.Id.ToString();
                this.Description = eqtype.Description;
            

            return true;
        }

        public bool LoadDataForSerialNo(string SerialNo)
        {

            short? equipmentType = this._DBContext.EquipmentLookup.Where(e => e.SerialNo == SerialNo).FirstOrDefault().EquipmentType;

            if (equipmentType == null || equipmentType == 0)
                return false;

            
                Models.EquipmentType eqtype = this._DBContext.EquipmentType.Find(equipmentType);
                this.Code = eqtype.Id.ToString();
                this.Description = eqtype.Description;
            

            return true;
        }


    }
}
