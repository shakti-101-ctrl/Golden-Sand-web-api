using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.Repository
{
    public class ServiceResponse<T>    
    {
        public T Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = null;
        public int? Response { get; set; }
    }

    public enum ResponseType
    {
        Ok = 200,
        InternalServerError = 500,
        NoConnect = 204,
        AlreadyExits = 409,
        UnAuthenticatedAccess = 403,
        ServerNotfound = 404
    }
    public class MessaageType
    {
        public static string Saved = "Record Added Successfully!";
        public static string Updated = "Record Updated Successfully!";
        public static string FailureOnSave = "Insertion Failed";
        public static string FailureOnUpdate = "Updatation Failed";
        public static string Deleted = "Record Deleted Successfully!";
        public static string DeletionFailed = "Deletion Failed";
        public static string RecordFound = "Record(s) Found";
        public static string NoRecordFound = "No Record(s) Found";
        public static string FailureOnException = "Some Exception Occured";
        public static string LoginSuccess = "Valid User";
        public static string UnAuthenticatedRequest = "Invalid Token";

    }
}
