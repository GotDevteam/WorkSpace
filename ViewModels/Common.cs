using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentAPI.Models;

namespace EquipmentAPI.ViewModels
{
    public static class Common
    {
        public static short?  UserID { get; set; }

        

        public static void WriteToErrorLog(GOT_EquipmentContext context, System.Exception ex , string errormessage, string errorcode, string functionname)
        {
            try
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.ErrorMessage = errormessage;
                if (ex.InnerException == null)
                {
                    errorLog.ExceptionStackTrace = ex.Message.ToString();
                        }
                else
                {
                    errorLog.ExceptionStackTrace = ex.Message.ToString() + ex.InnerException.ToString();
                }
                errorLog.AddDate = DateTime.Now;
                errorLog.UserId = Common.UserID;
                
                errorLog.ErrorCode = errorcode;
                errorLog.FunctionName = functionname;
                context.ErrorLog.Add(errorLog);
                context.SaveChanges();

               // errorLog.ErrorMessage 

            }
            catch(Exception exp)
            {

                //ignore the error while error logging
            }

        }

    }
}
