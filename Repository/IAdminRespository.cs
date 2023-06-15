using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldenSand_WebAPI.DTOs.Admin.Notice;
using GoldenSand_WebAPI.DTOs.Admin.Events;
using GoldenSand_WebAPI.DTOs.Admin.Income;
using GoldenSand_WebAPI.DTOs.Admin.Expense;
using GoldenSand_WebAPI.DTOs.Admin.Meeting;
using GoldenSand_WebAPI.DTOs.Admin.Tenant;
using GoldenSand_WebAPI.DTOs.Admin.Employee;
using GoldenSand_WebAPI.DTOs.Admin.Property;
using GoldenSand_WebAPI.DTOs.Admin.Duplex;
using GoldenSand_WebAPI.Models;


namespace GoldenSand_WebAPI.Repository
{
    public interface IAdminRespository
    {
        #region 1.Notice Srvices
        Task<ServiceResponse<List<GetNoticeDto>>> GetAllNotice();
        Task<ServiceResponse<GetNoticeDto>> AddNoticeDetails(AddNoticeDto category);
        Task<ServiceResponse<GetNoticeDto>> UpdateNoticeDetails(string noticeid, UpdateNoticeDto category);
        Task<ServiceResponse<GetNoticeDto>> DeleteNotice(string noticeid);
        Task<ServiceResponse<GetNoticeDto>> GetNoticeById(string noticeid);

        #endregion
        #region 2.Event Services
        Task<ServiceResponse<List<GetEventsDto>>> GetAllEvents();
        Task<ServiceResponse<GetEventsDto>> AddEventsDetails(AddEventsDto events);
        Task<ServiceResponse<GetEventsDto>> UpdateEventsDetails(string eventid, UpdateEventDto updateEventDto);
        Task<ServiceResponse<GetEventsDto>> DeleteEvent(string eventid);
        Task<ServiceResponse<GetEventsDto>> GetEventById(string eventid);

        #endregion
        #region 3.Expense Serveices
        Task<ServiceResponse<List<GetExpenseDetailsDto>>> GetAllExpenses();
        Task<ServiceResponse<GetExpenseDetailsDto>> AddExpenseDetails(AddExpenseDetailsDto events);
        Task<ServiceResponse<GetExpenseDetailsDto>> UpdateExpenseDetails(string expenseid, UpdateExpenseDetailsDto updateEventDto);
        Task<ServiceResponse<GetExpenseDetailsDto>> DeleteExpense(string expenseid);
        Task<ServiceResponse<GetExpenseDetailsDto>> GetExpenseByExpenseId(string expenseid);
        #endregion
        #region 4.Duplex Details
        Task<ServiceResponse<List<GetDuplexDetailsDto>>> GetAllDuplex();
        Task<ServiceResponse<GetDuplexDetailsDto>> AddDuplexDetails(AddDuplexDetailsDto duplexDetails);
        Task<ServiceResponse<GetDuplexDetailsDto>> UpdateDuplexDetails(string duplexid, UpdateDuplexDetailsDto updateDuplexDetailsDto);
        Task<ServiceResponse<GetDuplexDetailsDto>> DeleteDuplexDetails(string duplexid);
        Task<ServiceResponse<GetDuplexDetailsDto>> GetDuplexDetailsById(string duplexid);
        #endregion
        #region 5. Meeting Services
        Task<ServiceResponse<List<GetMeetingDetailsDto>>> GetAllMeetings();
        Task<ServiceResponse<GetMeetingDetailsDto>> AddMeetingDetails(AddMeetingDetailsDto addMeetingDetailsDto);
        Task<ServiceResponse<GetMeetingDetailsDto>> UpdateMeetinfDetails(string meetingid, UpdateMeetingDetailsDto updateMeetingDetailsDto);
        Task<ServiceResponse<GetMeetingDetailsDto>> DeleteMeetingDetails(string meetingid);
        Task<ServiceResponse<GetMeetingDetailsDto>> GetMeetingByMeetingId(string meetingid);
        #endregion
        #region 6. Income Services
        Task<ServiceResponse<List<GetIncomeDetailsDto>>> GetAllIncome();
        Task<ServiceResponse<GetIncomeDetailsDto>> AddIncomeDetails(AddIncomeDetailsDto addMeetingDetailsDto);
        Task<ServiceResponse<GetIncomeDetailsDto>> UpdateIncomeDetails(string incomeid, UpdateIncomeDetailsDto updateIncomeDetailsDto);
        Task<ServiceResponse<GetIncomeDetailsDto>> DeleteIncomeDetails(string incomeid);
        Task<ServiceResponse<GetIncomeDetailsDto>> GetIncomeDetailsByIncomeId(string incomeid);
        #endregion
        #region 7. Employee Services
        Task<ServiceResponse<List<GetEmployeeDetailsDto>>> GetAllEmployees();
        Task<ServiceResponse<GetEmployeeDetailsDto>> AddEmployeeDetails(AddEmployeeDetailsDto addEmployeeDetailsDto);
        Task<ServiceResponse<GetEmployeeDetailsDto>> UpdateEmployeeDetails(string empid, UpdateEmployeeDetailsDto updateEmployeeDetailsDto);
        Task<ServiceResponse<GetEmployeeDetailsDto>> DeleteEmployeeDetails(string empid);
        Task<ServiceResponse<GetEmployeeDetailsDto>> GetEmployeeDetails(string empid);
        #endregion
        #region 8. Tenant Services
        Task<ServiceResponse<List<GetTenantDetailsDto>>> GetAllTenant();
        Task<ServiceResponse<GetTenantDetailsDto>> AddTenantDetails(AddTenantDetailsDto addTenantDetailsDto);
        Task<ServiceResponse<GetTenantDetailsDto>> UpdateTenantDetails(string tenantid, UpdateTenantDetailsDto updateTenantDetailsDto);
        Task<ServiceResponse<GetTenantDetailsDto>> DeleteTenantDetails(string tenantid);
        Task<ServiceResponse<GetTenantDetailsDto>> GetTenantDetailsByTenantId(string tenantid);
        #endregion
        #region 9. Proprty declartion Services
        Task<ServiceResponse<List<GetProprtyDetailsDto>>> GetPropertyDetails();
        Task<ServiceResponse<GetProprtyDetailsDto>> AddPropertyDetails(AddPropertyDeclarationDto addPropertyDeclarationDto);
        Task<ServiceResponse<GetProprtyDetailsDto>> UpdatePropertyDetails(string propertyid, UpdatePropertyDeclarationDto updatePropertyDeclarationDto);
        Task<ServiceResponse<GetProprtyDetailsDto>> DeletePropertyDetails(string prpid);
        Task<ServiceResponse<GetProprtyDetailsDto>> GetPropertydetailsById(string prpid);
        #endregion

    }
}
