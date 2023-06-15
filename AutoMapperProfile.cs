using AutoMapper;
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

namespace GoldenSand_WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Notice
            CreateMap<NoticeDetails,GetNoticeDto>();
            CreateMap<AddNoticeDto, NoticeDetails>();
            CreateMap<UpdateNoticeDto,NoticeDetails>();
            #endregion

            #region Events
            CreateMap<EventsDetails, GetEventsDto>();
            CreateMap<AddEventsDto, EventsDetails>();
            CreateMap<UpdateEventDto, EventsDetails>();
            #endregion

            #region Duplex
            CreateMap<DuplexDetails, GetDuplexDetailsDto>();
            CreateMap<AddDuplexDetailsDto, DuplexDetails>();
            CreateMap<UpdateDuplexDetailsDto, DuplexDetails>();
            #endregion

            #region Employee
            CreateMap<EmployeeDetails, GetEmployeeDetailsDto>();
            CreateMap<AddEmployeeDetailsDto, EmployeeDetails>();
            CreateMap<UpdateEmployeeDetailsDto, EmployeeDetails>();
            #endregion

            #region Expense
            CreateMap<ExpenseDetails, GetExpenseDetailsDto>();
            CreateMap<AddExpenseDetailsDto, ExpenseDetails>();
            CreateMap<UpdateExpenseDetailsDto, ExpenseDetails>();
            #endregion

            #region Income
            CreateMap<IncomeDetails, GetIncomeDetailsDto>();
            CreateMap<AddIncomeDetailsDto, IncomeDetails>();
            CreateMap<UpdateIncomeDetailsDto, IncomeDetails>();
            #endregion

            #region Meeting
            CreateMap<MeetingDetails, GetMeetingDetailsDto>();
            CreateMap<AddMeetingDetailsDto, MeetingDetails>();
            CreateMap<UpdateMeetingDetailsDto, MeetingDetails>();
            #endregion

            #region Property
            CreateMap<PropertyDeclaration, GetProprtyDetailsDto>();
            CreateMap<AddPropertyDeclarationDto, PropertyDeclaration>();
            CreateMap<UpdatePropertyDeclarationDto, PropertyDeclaration>();
            #endregion

            #region Tenant
            CreateMap<TenantDetails, GetTenantDetailsDto>();
            CreateMap<AddTenantDetailsDto, TenantDetails>();
            CreateMap<UpdateTenantDetailsDto, TenantDetails>();
            #endregion


        }
    }
}
