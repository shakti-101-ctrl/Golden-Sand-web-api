using AutoMapper;
using GoldenSand_WebAPI.DTOs.Admin.Duplex;
using GoldenSand_WebAPI.DTOs.Admin.Employee;
using GoldenSand_WebAPI.DTOs.Admin.Events;
using GoldenSand_WebAPI.DTOs.Admin.Expense;
using GoldenSand_WebAPI.DTOs.Admin.Income;
using GoldenSand_WebAPI.DTOs.Admin.Meeting;
using GoldenSand_WebAPI.DTOs.Admin.Notice;
using GoldenSand_WebAPI.DTOs.Admin.Property;
using GoldenSand_WebAPI.DTOs.Admin.Tenant;
using GoldenSand_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.Repository
{
    public class AdminRepository : IAdminRespository
    {

        public readonly GOLDEN_SAND_SOCIETYContext _context;
        private readonly IMapper _mapper;
        public AdminRepository(IMapper mapper,GOLDEN_SAND_SOCIETYContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        #region 1.Notice [Cpmplete]
        public async Task<ServiceResponse<GetNoticeDto>> DeleteNotice(string noticeid)
        {
            ServiceResponse<GetNoticeDto> serviceResponse = new ServiceResponse<GetNoticeDto>();

            NoticeDetails notice = await _context.NoticeDetails.FindAsync(noticeid);
            try
            {
                if (notice != null)
                {
                    notice.ActiveStatus = false;
                    notice.DeleteDate = DateTime.Now;
                    notice.DeleteStatus = true;
                    _context.Entry(notice).State = EntityState.Modified;
                    _context.Update(notice).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetNoticeDto>(notice);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetNoticeDto>> AddNoticeDetails(AddNoticeDto notice)
        {
            ServiceResponse<GetNoticeDto> serviceResponse = new ServiceResponse<GetNoticeDto>();
            try
            {
                if(_context!=null)
                {
                    //create the customize the notice id Ex: NOT001
                    var notices = await _context.NoticeDetails.ToListAsync();
                    int rowcount = notices.Count+1;
                    if (rowcount.ToString().Length == 1)
                    {
                        notice.NoticeId = "NOT00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        notice.NoticeId = "NOT0" + rowcount;
                    }
                    else if(rowcount.ToString().Length == 3)
                    {
                        notice.NoticeId = "NOT" + rowcount;
                    }
                    notice.PostedDate = notice.PostedDate;
                    notice.EndDate =notice.EndDate;
                    notice.EntryDate = DateTime.Now;
                    notice.DeleteStatus = false;
                    notice.ActiveStatus = true;
                    await _context.NoticeDetails.AddAsync(_mapper.Map<NoticeDetails>(notice));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetNoticeDto>(_context.NoticeDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetNoticeDto>>> GetAllNotice()
        {
            ServiceResponse<List<GetNoticeDto>> serviceResponse = new ServiceResponse<List<GetNoticeDto>>();
            var notices = await _context.NoticeDetails.Where(x=>x.ActiveStatus==true).ToListAsync();
            if (notices.Count>0)
            {
                serviceResponse.Data = _mapper.Map<List<GetNoticeDto>>(notices);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;
            }
            else
            {
                //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetNoticeDto>> GetNoticeById(string noticeid)
        {
            ServiceResponse<GetNoticeDto> serviceResponse = new ServiceResponse<GetNoticeDto>();
            var category = await _context.NoticeDetails.Where(x => x.NoticeId == noticeid && x.ActiveStatus==true).FirstOrDefaultAsync();
            if (category != null)
            {
                serviceResponse.Data = _mapper.Map<GetNoticeDto>(category);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetNoticeDto>> UpdateNoticeDetails(string noticeid, UpdateNoticeDto notice)
        {
            ServiceResponse<GetNoticeDto> serviceResponse = new ServiceResponse<GetNoticeDto>();
            try
            {
                NoticeDetails noticeDetails = await _context.NoticeDetails.FindAsync(noticeid);
                if (noticeDetails != null)
                {
                    noticeDetails.HeadingText = notice.HeadingText;
                    noticeDetails.Description = notice.Description;
                    notice.PostedDate = notice.PostedDate;
                    notice.EntryDate = notice.PostedDate;
                    noticeDetails.EndDate = notice.EndDate;
                    noticeDetails.LastModifiedDate = DateTime.Now;
                    _context.Entry(noticeDetails).State = EntityState.Modified;
                    _context.Update(noticeDetails).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetNoticeDto>(noticeDetails);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }


        #endregion

        #region 2.Event Details [Complete]
        public async Task<ServiceResponse<List<GetEventsDto>>> GetAllEvents()
        {
            ServiceResponse<List<GetEventsDto>> serviceResponse = new ServiceResponse<List<GetEventsDto>>();
            var events = await _context.EventsDetails.Where(x => x.ActiveStatus == true).ToListAsync();
            if (events.Count>0)
            {
                serviceResponse.Data = _mapper.Map<List<GetEventsDto>>(events);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;
            }
            else
            {
                //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventsDto>> GetEventById(string eventid)
        {
            ServiceResponse<GetEventsDto> serviceResponse = new ServiceResponse<GetEventsDto>();
            var _event = await _context.EventsDetails.Where(x => x.EventId == eventid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (_event != null)
            {
                serviceResponse.Data = _mapper.Map<GetEventsDto>(_event);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventsDto>> AddEventsDetails(AddEventsDto events)
        {
            ServiceResponse<GetEventsDto> serviceResponse = new ServiceResponse<GetEventsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var eventsList = await _context.EventsDetails.ToListAsync();
                    int rowcount = eventsList.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        events.EventId = "EVNT00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        events.EventId = "EVNT0" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 3)
                    {
                        events.EventId = "EVNT" + rowcount;
                    }
                    events.PostedDate = DateTime.Now;
                    events.EntryDate = DateTime.Now;
                    events.DeleteStatus = false;
                    events.ActiveStatus = true;
                    await _context.EventsDetails.AddAsync(_mapper.Map<EventsDetails>(events));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetEventsDto>(_context.EventsDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventsDto>> UpdateEventsDetails(string eventid, UpdateEventDto updateEventDto)
        {
            ServiceResponse<GetEventsDto> serviceResponse = new ServiceResponse<GetEventsDto>();
            try
            {
                EventsDetails eventsDetails = await _context.EventsDetails.FindAsync(eventid);
                if (eventsDetails != null)
                {
                    eventsDetails.HeadingText = updateEventDto.HeadingText;
                    eventsDetails.Description = updateEventDto.Description;
                    eventsDetails.PostedDate = updateEventDto.PostedDate;
                    eventsDetails.EntryDate = updateEventDto.PostedDate;
                    eventsDetails.EventStartDate = updateEventDto.EventStartDate;
                    eventsDetails.EventEndDate = updateEventDto.EventEndDate;
                    eventsDetails.LastModifiedDate = DateTime.Now;
                    _context.Entry(eventsDetails).State = EntityState.Modified;
                    _context.Update(eventsDetails).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetEventsDto>(eventsDetails);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEventsDto>> DeleteEvent(string eventid)
        {
            ServiceResponse<GetEventsDto> serviceResponse = new ServiceResponse<GetEventsDto>();

            EventsDetails eventDetails = await _context.EventsDetails.FindAsync(eventid);
            try
            {
                if (eventDetails != null)
                {
                    eventDetails.ActiveStatus = false;
                    eventDetails.DeleteStatus = true;
                    eventDetails.DeleteDate = DateTime.Now;
                    _context.Entry(eventDetails).State = EntityState.Modified;
                    _context.Update(eventDetails).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetEventsDto>(eventDetails);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        #endregion

        #region 3.Expense Details [Complete]


        public async Task<ServiceResponse<List<GetExpenseDetailsDto>>> GetAllExpenses()
        {
            ServiceResponse<List<GetExpenseDetailsDto>> serviceResponse = new ServiceResponse<List<GetExpenseDetailsDto>>();
            var events = await _context.ExpenseDetails.Where(x => x.ActiveStatus == true).ToListAsync();
            if (events.Count > 0)
            {
                serviceResponse.Data = _mapper.Map<List<GetExpenseDetailsDto>>(events);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;
            }
            else
            {
                //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDetailsDto>> AddExpenseDetails(AddExpenseDetailsDto expenses)
        {
            ServiceResponse<GetExpenseDetailsDto> serviceResponse = new ServiceResponse<GetExpenseDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var expenseList = await _context.ExpenseDetails.ToListAsync();
                    int rowcount = expenseList.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        expenses.ExpenseId = "EXPEN00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        expenses.ExpenseId = "EXPEN0" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 3)
                    {
                        expenses.ExpenseId = "EXPEN" + rowcount;
                    }
                    
                    expenses.ActiveStatus = true ;
                    
                    await _context.ExpenseDetails.AddAsync(_mapper.Map<ExpenseDetails>(expenses));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetExpenseDetailsDto>(_context.ExpenseDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDetailsDto>> UpdateExpenseDetails(string expenseid, UpdateExpenseDetailsDto updateExpenseDetailsDto)
        {
            ServiceResponse<GetExpenseDetailsDto> serviceResponse = new ServiceResponse<GetExpenseDetailsDto>();
            try
            {
                ExpenseDetails expenseDetails = await _context.ExpenseDetails.FindAsync(expenseid);
                if (expenseDetails != null)
                {
                    expenseDetails.ExpenseHead = updateExpenseDetailsDto.ExpenseHead;
                    expenseDetails.PaymentType = updateExpenseDetailsDto.PaymentType;
                    expenseDetails.TowhomOrTransactionId = updateExpenseDetailsDto.TowhomOrTransactionId;
                    expenseDetails.DateEntry = DateTime.Now;
                    expenseDetails.PaymentDate = updateExpenseDetailsDto.PaymentDate;
                    expenseDetails.Narration = updateExpenseDetailsDto.Narration;
                    expenseDetails.LastModifiedDate = DateTime.Now;
                    expenseDetails.ActiveStatus = true;
                    _context.Entry(expenseDetails).State = EntityState.Modified;
                    _context.Update(expenseDetails).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetExpenseDetailsDto>(expenseDetails);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDetailsDto>> DeleteExpense(string expenseid)
        {
            ServiceResponse<GetExpenseDetailsDto> serviceResponse = new ServiceResponse<GetExpenseDetailsDto>();

            ExpenseDetails expense = await _context.ExpenseDetails.FindAsync(expenseid);
            try
            {
                if (expense != null)
                {
                    expense.ActiveStatus = false;
                    expense.DeleteDate = DateTime.Now;

                    _context.Entry(expense).State = EntityState.Modified;
                    _context.Update(expense).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetExpenseDetailsDto>(expense);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetExpenseDetailsDto>> GetExpenseByExpenseId(string expenseid)
        {
            ServiceResponse<GetExpenseDetailsDto> serviceResponse = new ServiceResponse<GetExpenseDetailsDto>();
            var _expense = await _context.ExpenseDetails.Where(x => x.ExpenseId == expenseid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (_expense != null)
            {
                serviceResponse.Data = _mapper.Map<GetExpenseDetailsDto>(_expense);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }


        #endregion

        #region 4. Duplex Service [Complete]
        public async Task<ServiceResponse<List<GetDuplexDetailsDto>>> GetAllDuplex()
        {
            ServiceResponse<List<GetDuplexDetailsDto>> serviceResponse = new ServiceResponse<List<GetDuplexDetailsDto>>();
            try
            {
                var duplexDetails = await _context.DuplexDetails.Where(x => x.ActiveStatus == true).ToListAsync();
                if (duplexDetails.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetDuplexDetailsDto>>(duplexDetails);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }
               
            }
            catch
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDuplexDetailsDto>> AddDuplexDetails(AddDuplexDetailsDto duplexDetails)
        {
            ServiceResponse<GetDuplexDetailsDto> serviceResponse = new ServiceResponse<GetDuplexDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var duplexes = await _context.DuplexDetails.ToListAsync();
                    int rowcount = duplexes.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        duplexDetails.DuplexId = "DUPLEX0" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        duplexDetails.DuplexId = "DUPLEX" + rowcount;
                    }
                   
                    duplexDetails.EntryDate = DateTime.Now;
                    duplexDetails.ActiveStatus = true;

                    await _context.DuplexDetails.AddAsync(_mapper.Map<DuplexDetails>(duplexDetails));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetDuplexDetailsDto>(_context.DuplexDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDuplexDetailsDto>> UpdateDuplexDetails(string duplexid, UpdateDuplexDetailsDto updateDuplexDetailsDto)
        {
            ServiceResponse<GetDuplexDetailsDto> serviceResponse = new ServiceResponse<GetDuplexDetailsDto>();
            try
            {
                DuplexDetails duplex = await _context.DuplexDetails.FindAsync(duplexid);
                if (duplex != null)
                {
                    duplex.DuplexNumber = updateDuplexDetailsDto.DuplexNumber;
                    duplex.OwnerName = updateDuplexDetailsDto.OwnerName;
                    if(updateDuplexDetailsDto.AdharCardCopy==null)
                    {
                        duplex.AdharCardCopy = duplex.AdharCardCopy;
                    }
                    else
                    {
                        duplex.AdharCardCopy = updateDuplexDetailsDto.AdharCardCopy;
                    }

                    if(updateDuplexDetailsDto.PhotoCopy==null)
                    {
                        duplex.PhotoCopy = duplex.PhotoCopy;
                    }
                    else
                    {
                        duplex.PhotoCopy = updateDuplexDetailsDto.PhotoCopy;
                    }
                    duplex.Contact = updateDuplexDetailsDto.Contact;
                    duplex.AlternateContact = updateDuplexDetailsDto.AlternateContact;
                    duplex.EmailId = updateDuplexDetailsDto.EmailId;
                    duplex.LastModifiedDate = DateTime.Now;
                    duplex.ActiveStatus = true;
                    _context.Entry(duplex).State = EntityState.Modified;
                    _context.Update(duplex).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetDuplexDetailsDto>(duplex);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDuplexDetailsDto>> DeleteDuplexDetails(string duplexid)
        {
            ServiceResponse<GetDuplexDetailsDto> serviceResponse = new ServiceResponse<GetDuplexDetailsDto>();

            DuplexDetails duplex = await _context.DuplexDetails.FindAsync(duplexid);
            try
            {
                if (duplex != null)
                {
                    duplex.ActiveStatus = false;
                    duplex.DeleteDate = DateTime.Now;

                    _context.Entry(duplex).State = EntityState.Modified;
                    _context.Update(duplex).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetDuplexDetailsDto>(duplex);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDuplexDetailsDto>> GetDuplexDetailsById(string duplexid)
        {
            ServiceResponse<GetDuplexDetailsDto> serviceResponse = new ServiceResponse<GetDuplexDetailsDto>();
            var duplex = await _context.DuplexDetails.Where(x => x.DuplexId == duplexid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (duplex != null)
            {
                serviceResponse.Data = _mapper.Map<GetDuplexDetailsDto>(duplex);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }


        #endregion 

        #region 5.Meeting Services
        public async Task<ServiceResponse<List<GetMeetingDetailsDto>>> GetAllMeetings()
        {
            ServiceResponse<List<GetMeetingDetailsDto>> serviceResponse = new ServiceResponse<List<GetMeetingDetailsDto>>();
            try
            {
                var meetings = await _context.MeetingDetails.Where(x => x.ActiveStatus == true).ToListAsync();
                if (meetings.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetMeetingDetailsDto>>(meetings);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }

            }
            catch
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMeetingDetailsDto>> AddMeetingDetails(AddMeetingDetailsDto addMeetingDetailsDto)
        {
            ServiceResponse<GetMeetingDetailsDto> serviceResponse = new ServiceResponse<GetMeetingDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var meetings = await _context.MeetingDetails.ToListAsync();
                    int rowcount = meetings.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        addMeetingDetailsDto.MeetingId = "MT00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        addMeetingDetailsDto.MeetingId = "MT0" + rowcount;
                    }
                    else
                    {
                        addMeetingDetailsDto.MeetingId = "MT" + rowcount;
                    }
                   
                    addMeetingDetailsDto.EntryDate = DateTime.Now;
                    addMeetingDetailsDto.ActiveStatus = true;
                    addMeetingDetailsDto.PostedDate = DateTime.Now;

                    await _context.MeetingDetails.AddAsync(_mapper.Map<MeetingDetails>(addMeetingDetailsDto));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetMeetingDetailsDto>(_context.MeetingDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMeetingDetailsDto>> UpdateMeetinfDetails(string meetingid, UpdateMeetingDetailsDto updateMeetingDetailsDto)
        {
            ServiceResponse<GetMeetingDetailsDto> serviceResponse = new ServiceResponse<GetMeetingDetailsDto>();
            try
            {
                MeetingDetails meeting = await _context.MeetingDetails.FindAsync(meetingid);
                if (meeting != null)
                {
                    meeting.HeadingText = updateMeetingDetailsDto.HeadingText;
                    meeting.Description = updateMeetingDetailsDto.Description;
                    meeting.PostedDate = updateMeetingDetailsDto.PostedDate;
                    meeting.MeetEndDateTime = updateMeetingDetailsDto.MeetStartDateTime;
                    meeting.MeetStartDateTime = updateMeetingDetailsDto.MeetStartDateTime;
                    meeting.InvitedPersons = updateMeetingDetailsDto.InvitedPersons;
                    meeting.LasteModifiedDate = DateTime.Now;
                    meeting.ActiveStatus = updateMeetingDetailsDto.ActiveStatus;
                    _context.Entry(meeting).State = EntityState.Modified;
                    _context.Update(meeting).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetMeetingDetailsDto>(meeting);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMeetingDetailsDto>> DeleteMeetingDetails(string meetingid)
        {
            ServiceResponse<GetMeetingDetailsDto> serviceResponse = new ServiceResponse<GetMeetingDetailsDto>();

            MeetingDetails meeting = await _context.MeetingDetails.FindAsync(meetingid);
            try
            {
                if (meeting != null)
                {
                    meeting.ActiveStatus = false;
                    meeting.DeleteStatus = true;
                    meeting.DeleteDate = DateTime.Now;

                    _context.Entry(meeting).State = EntityState.Modified;
                    _context.Update(meeting).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetMeetingDetailsDto>(meeting);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMeetingDetailsDto>> GetMeetingByMeetingId(string meetingid)
        {
            ServiceResponse<GetMeetingDetailsDto> serviceResponse = new ServiceResponse<GetMeetingDetailsDto>();
            var duplex = await _context.MeetingDetails.Where(x => x.MeetingId == meetingid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (duplex != null)
            {
                serviceResponse.Data = _mapper.Map<GetMeetingDetailsDto>(duplex);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        
        #endregion

        #region 6. Income Services
        public async Task<ServiceResponse<List<GetIncomeDetailsDto>>> GetAllIncome()
        {
            ServiceResponse<List<GetIncomeDetailsDto>> serviceResponse = new ServiceResponse<List<GetIncomeDetailsDto>>();
            try
            {
                var incomes = await (from duplex in _context.DuplexDetails
                                     join income in _context.IncomeDetails
                                     on duplex.DuplexId equals income.DuplexNumber
                                     select new GetIncomeDetailsDto
                                     {
                                         Id = income.Id,
                                         IncomeId = income.IncomeId,
                                         DuplexNumber = duplex.DuplexNumber,
                                         Purpose = income.Purpose,
                                         PaymentType = income.PaymentType,
                                         TowhomOrTransactionId = income.TowhomOrTransactionId,
                                         OwnerName = income.OwnerName,
                                         DateEntry = income.DateEntry,
                                         PaymentDate = income.PaymentDate,
                                         Narration = income.Narration,
                                         ActiveStatus = income.ActiveStatus,
                                         LastModifiedDate = income.LastModifiedDate,
                                         DeleteDate = income.DeleteDate
                                     }
                              ).Where(x => x.ActiveStatus == true).ToListAsync();


                //var incomes = await _context.IncomeDetails.Where(x => x.ActiveStatus == true).ToListAsync();
                if (incomes.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetIncomeDetailsDto>>(incomes);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }

            }
            catch(Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetIncomeDetailsDto>> AddIncomeDetails(AddIncomeDetailsDto addIncomeDetailsDto)
        {
            ServiceResponse<GetIncomeDetailsDto> serviceResponse = new ServiceResponse<GetIncomeDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var incomes = await _context.IncomeDetails.ToListAsync();
                    int rowcount = incomes.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        addIncomeDetailsDto.IncomeId = "IN000" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        addIncomeDetailsDto.IncomeId = "IN00" + rowcount;
                    }
                    else if(rowcount.ToString().Length==3)
                    {
                        addIncomeDetailsDto.IncomeId = "IN0" + rowcount;
                    }
                    else
                    {
                        addIncomeDetailsDto.IncomeId = "IN" + rowcount;
                    }

                    addIncomeDetailsDto.DateEntry = DateTime.Now;
                    addIncomeDetailsDto.ActiveStatus = true;
                    

                    await _context.IncomeDetails.AddAsync(_mapper.Map<IncomeDetails>(addIncomeDetailsDto));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetIncomeDetailsDto>(_context.IncomeDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetIncomeDetailsDto>> UpdateIncomeDetails(string incomeid, UpdateIncomeDetailsDto updateIncomeDetailsDto)
        {
            ServiceResponse<GetIncomeDetailsDto> serviceResponse = new ServiceResponse<GetIncomeDetailsDto>();
            try
            {
                IncomeDetails income = await _context.IncomeDetails.FindAsync(incomeid);
                if (income != null)
                {
                    income.DuplexNumber = updateIncomeDetailsDto.DuplexNumber;
                    income.Purpose = updateIncomeDetailsDto.Purpose;
                    income.PaymentType = updateIncomeDetailsDto.PaymentType;
                    income.TowhomOrTransactionId = updateIncomeDetailsDto.TowhomOrTransactionId;
                    income.OwnerName = updateIncomeDetailsDto.OwnerName;
                    income.DateEntry = DateTime.Now;
                    income.ActiveStatus = income.ActiveStatus;
                    income.LastModifiedDate = DateTime.Now;
                    _context.Entry(income).State = EntityState.Modified;
                    _context.Update(income).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetIncomeDetailsDto>(income);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetIncomeDetailsDto>> DeleteIncomeDetails(string incomeid)
        {
            ServiceResponse<GetIncomeDetailsDto> serviceResponse = new ServiceResponse<GetIncomeDetailsDto>();

            IncomeDetails income = await _context.IncomeDetails.FindAsync(incomeid);
            try
            {
                if (income != null)
                {
                    income.ActiveStatus = false;
                    income.DeleteDate = DateTime.Now;

                    _context.Entry(income).State = EntityState.Modified;
                    _context.Update(income).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetIncomeDetailsDto>(income);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetIncomeDetailsDto>> GetIncomeDetailsByIncomeId(string incomeid)
        {
            ServiceResponse<GetIncomeDetailsDto> serviceResponse = new ServiceResponse<GetIncomeDetailsDto>();
            //var duplex = await _context.IncomeDetails.Where(x => x.IncomeId == incomeid && x.ActiveStatus == true).FirstOrDefaultAsync();
            var income = await (from duplex in _context.DuplexDetails
                                join _income in _context.IncomeDetails
                                on duplex.DuplexId equals _income.DuplexNumber
                                select new GetIncomeDetailsDto
                                {
                                    Id = _income.Id,
                                    IncomeId = _income.IncomeId,
                                    DuplexNumber = duplex.DuplexNumber,
                                    Purpose = _income.Purpose,
                                    PaymentType = _income.PaymentType,
                                    TowhomOrTransactionId = _income.TowhomOrTransactionId,
                                    OwnerName = _income.OwnerName,
                                    DateEntry = _income.DateEntry,
                                    PaymentDate = _income.PaymentDate,
                                    Narration = _income.Narration,
                                    ActiveStatus = _income.ActiveStatus,
                                    LastModifiedDate = _income.LastModifiedDate,
                                    DeleteDate = _income.DeleteDate

                                }).Where(x => x.ActiveStatus == true && x.IncomeId==incomeid).FirstOrDefaultAsync();
            if (income != null)
            {
                serviceResponse.Data = _mapper.Map<GetIncomeDetailsDto>(income);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        #endregion

        #region 7. Employee Services
        public async Task<ServiceResponse<List<GetEmployeeDetailsDto>>> GetAllEmployees()
        {

            ServiceResponse<List<GetEmployeeDetailsDto>> serviceResponse = new ServiceResponse<List<GetEmployeeDetailsDto>>();
            try
            {
                var employees = await _context.EmployeeDetails.Where(x => x.ActiveStatus == true).ToListAsync();
                if (employees.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetEmployeeDetailsDto>>(employees);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }

            }
            catch
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDetailsDto>> AddEmployeeDetails(AddEmployeeDetailsDto addEmployeeDetailsDto)
        {
            ServiceResponse<GetEmployeeDetailsDto> serviceResponse = new ServiceResponse<GetEmployeeDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var employees = await _context.EmployeeDetails.ToListAsync();
                    int rowcount = employees.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        addEmployeeDetailsDto.EmployeeId = "EMP00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        addEmployeeDetailsDto.EmployeeId = "EMP0" + rowcount;
                    }
                    else
                    {
                        addEmployeeDetailsDto.EmployeeId = "EMP" + rowcount;
                    }

                    addEmployeeDetailsDto.EntryDate = DateTime.Now;
                    addEmployeeDetailsDto.ActiveStatus = true;
                    

                    await _context.EmployeeDetails.AddAsync(_mapper.Map<EmployeeDetails>(addEmployeeDetailsDto));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetEmployeeDetailsDto>(_context.EmployeeDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDetailsDto>> UpdateEmployeeDetails(string empid, UpdateEmployeeDetailsDto updateEmployeeDetailsDto)
        {
            ServiceResponse<GetEmployeeDetailsDto> serviceResponse = new ServiceResponse<GetEmployeeDetailsDto>();
            try
            {
                EmployeeDetails employee = await _context.EmployeeDetails.FindAsync(empid);
                if (employee != null)
                {
                    employee.EmployeeType = updateEmployeeDetailsDto.EmployeeType;
                    employee.EmployeeName = updateEmployeeDetailsDto.EmployeeName;
                    employee.FatherName = updateEmployeeDetailsDto.FatherName;
                    employee.Position = updateEmployeeDetailsDto.Position;
                    employee.Contact = updateEmployeeDetailsDto.Contact;
                    employee.AddressDetails = updateEmployeeDetailsDto.AddressDetails;
                    if(updateEmployeeDetailsDto.Photo==null)
                    {
                        employee.Photo = employee.Photo;
                    }
                    else
                    {
                        employee.Photo = updateEmployeeDetailsDto.Photo;
                    }
                    if(updateEmployeeDetailsDto.ScanCopyOfAdharCard==null)
                    {
                        employee.ScanCopyOfAdharCard = employee.ScanCopyOfAdharCard;
                    }
                    else
                    {
                        employee.ScanCopyOfAdharCard = updateEmployeeDetailsDto.ScanCopyOfAdharCard;
                    }
                    employee.JoiningDate = updateEmployeeDetailsDto.JoiningDate;
                    employee.ProviderName = updateEmployeeDetailsDto.ProviderName;
                    employee.ProviderOwnerName = updateEmployeeDetailsDto.ProviderOwnerName;
                    employee.ProviderAddressdetails = updateEmployeeDetailsDto.ProviderAddressdetails;
                    employee.ProviderAlternateContact = updateEmployeeDetailsDto.ProviderContact;
                    employee.ProviderAlternateContact = updateEmployeeDetailsDto.ProviderAlternateContact;
                    employee.RegistrationNumber = updateEmployeeDetailsDto.RegistrationNumber;
                    

                    _context.Entry(employee).State = EntityState.Modified;
                    _context.Update(employee).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetEmployeeDetailsDto>(employee);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDetailsDto>> DeleteEmployeeDetails(string empid)
        {
            ServiceResponse<GetEmployeeDetailsDto> serviceResponse = new ServiceResponse<GetEmployeeDetailsDto>();

            EmployeeDetails employee = await _context.EmployeeDetails.FindAsync(empid);
            try
            {
                if (employee != null)
                {
                    employee.ActiveStatus = false;
                    employee.DeleteDate = DateTime.Now;
                    employee.DeleteStatus = true;
                    _context.Entry(employee).State = EntityState.Modified;
                    _context.Update(employee).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetEmployeeDetailsDto>(employee);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDetailsDto>> GetEmployeeDetails(string empid)
        {
            ServiceResponse<GetEmployeeDetailsDto> serviceResponse = new ServiceResponse<GetEmployeeDetailsDto>();
            var employee = await _context.EmployeeDetails.Where(x => x.EmployeeId == empid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (employee != null)
            {
                serviceResponse.Data = _mapper.Map<GetEmployeeDetailsDto>(employee);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        #endregion

        #region 8.Tenant Details
        public async Task<ServiceResponse<List<GetTenantDetailsDto>>> GetAllTenant()
        {
            ServiceResponse<List<GetTenantDetailsDto>> serviceResponse = new ServiceResponse<List<GetTenantDetailsDto>>();
            try
            {
                // var tenants = await _context.TenantDetails.Where(x => x.ActiveStatus == true).ToListAsync();

                var tenants = await (from duplex in _context.DuplexDetails
                                     join tenant in _context.TenantDetails
                                     on duplex.DuplexId equals tenant.DuplexNumber
                                     select new GetTenantDetailsDto
                                     {
                                         Id = tenant.Id,
                                         TenantId = tenant.TenantId,
                                         DuplexNumber = duplex.DuplexNumber,
                                         TenantType = tenant.TenantType,
                                         NoOfMembers = tenant.NoOfMembers,
                                         Name= tenant.Name,
                                         Contact = tenant.Contact,
                                         Occupation=tenant.Occupation,
                                         StayingDate = tenant.StayingDate,
                                         ActiveStatus = tenant.ActiveStatus,
                                         DeleteDate = tenant.DeleteDate,
                                         DeleteStatus = tenant.DeleteStatus,
                                         ApprovalStatus =tenant.ApprovalStatus,
                                         EntryDate = tenant.EntryDate,
                                         LastModifiedDate = tenant.LastModifiedDate
                                         
                                     }).Where(x => x.ActiveStatus == true).ToListAsync();
                if (tenants.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetTenantDetailsDto>>(tenants);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    //serviceResponse.data = _mapper.Map<List<GetCategoryDto>>(category);
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }

            }
            catch
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTenantDetailsDto>> AddTenantDetails(AddTenantDetailsDto addTenantDetailsDto)
        {
            ServiceResponse<GetTenantDetailsDto> serviceResponse = new ServiceResponse<GetTenantDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var tenants = await _context.TenantDetails.ToListAsync();
                    int rowcount = tenants.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        addTenantDetailsDto.TenantId = "TNT00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        addTenantDetailsDto.TenantId = "TNT0" + rowcount;
                    }
                    else
                    {
                        addTenantDetailsDto.TenantId = "TNT" + rowcount;
                    }


                    addTenantDetailsDto.ActiveStatus = true;
                    addTenantDetailsDto.EntryDate = DateTime.Now;
                    addTenantDetailsDto.ApprovalStatus = false;
                    await _context.TenantDetails.AddAsync(_mapper.Map<TenantDetails>(addTenantDetailsDto));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetTenantDetailsDto>(_context.TenantDetails.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTenantDetailsDto>> UpdateTenantDetails(string tenantid, UpdateTenantDetailsDto updateTenantDetailsDto)
        {
            ServiceResponse<GetTenantDetailsDto> serviceResponse = new ServiceResponse<GetTenantDetailsDto>();
            try
            {
                TenantDetails tenant = await _context.TenantDetails.FindAsync(tenantid);
                if (tenant != null)
                {
                    tenant.DuplexNumber = updateTenantDetailsDto.DuplexNumber;
                    tenant.TenantType = updateTenantDetailsDto.TenantType;
                    tenant.NoOfMembers = updateTenantDetailsDto.NoOfMembers;
                    tenant.Name = updateTenantDetailsDto.Name;
                    tenant.Contact = updateTenantDetailsDto.Contact;
                    tenant.Occupation = updateTenantDetailsDto.Occupation;
                    tenant.StayingDate = updateTenantDetailsDto.StayingDate;
                    tenant.ApprovalStatus = updateTenantDetailsDto.ApprovalStatus;
                    tenant.EntryDate = DateTime.Now;
                    tenant.LastModifiedDate = DateTime.Now;
                    _context.Entry(tenant).State = EntityState.Modified;
                    _context.Update(tenant).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetTenantDetailsDto>(tenant);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTenantDetailsDto>> DeleteTenantDetails(string tenantid)
        {
            ServiceResponse<GetTenantDetailsDto> serviceResponse = new ServiceResponse<GetTenantDetailsDto>();

            TenantDetails tenantDetails = await _context.TenantDetails.FindAsync(tenantid);
            try
            {
                if (tenantDetails != null)
                {
                    tenantDetails.ActiveStatus = false;
                    tenantDetails.DeleteDate = DateTime.Now;
                    tenantDetails.DeleteStatus = true;
                    _context.Entry(tenantDetails).State = EntityState.Modified;
                    _context.Update(tenantDetails).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetTenantDetailsDto>(tenantDetails);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTenantDetailsDto>> GetTenantDetailsByTenantId(string tenantid)
        {
            ServiceResponse<GetTenantDetailsDto> serviceResponse = new ServiceResponse<GetTenantDetailsDto>();
            try
            {
                //var tenant = await _context.TenantDetails.Where(x => x.TenantId == tenantid && x.ActiveStatus == true).FirstOrDefaultAsync();
                var tenant = await (from duplex in _context.DuplexDetails
                                    join _tenant in _context.TenantDetails
                                    on duplex.DuplexId equals _tenant.DuplexNumber
                                    select new GetTenantDetailsDto
                                    {
                                        Id = _tenant.Id,
                                        TenantId = _tenant.TenantId,
                                        DuplexNumber = duplex.DuplexNumber,
                                        TenantType = _tenant.TenantType,
                                        NoOfMembers = _tenant.NoOfMembers,
                                        Name = _tenant.Name,
                                        Contact = _tenant.Contact,
                                        Occupation = _tenant.Occupation,
                                        StayingDate = _tenant.StayingDate,
                                        ActiveStatus = _tenant.ActiveStatus,
                                        DeleteStatus = _tenant.DeleteStatus,
                                        ApprovalStatus = _tenant.ApprovalStatus,
                                        EntryDate = _tenant.EntryDate,
                                        LastModifiedDate = _tenant.LastModifiedDate,
                                        DeleteDate = _tenant.DeleteDate

                                    }).Where(x => x.ActiveStatus == true && x.TenantId == tenantid).FirstOrDefaultAsync();
                if (tenant != null)
                {
                    serviceResponse.Data = _mapper.Map<GetTenantDetailsDto>(tenant);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;

                }
                else
                {
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }
            }
            catch(Exception Ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
           
           
            return serviceResponse;
        }
        #endregion

        #region 9. Proprty declaration services
        public async Task<ServiceResponse<List<GetProprtyDetailsDto>>> GetPropertyDetails()
        {
            ServiceResponse<List<GetProprtyDetailsDto>> serviceResponse = new ServiceResponse<List<GetProprtyDetailsDto>>();
            try
            {
                var properties = await _context.PropertyDeclaration.Where(x => x.ActiveStatus == true).ToListAsync();
                if (properties.Count > 0)
                {
                    serviceResponse.Data = _mapper.Map<List<GetProprtyDetailsDto>>(properties);
                    serviceResponse.Message = MessaageType.RecordFound;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Success = true;
                }
                else
                {
                    
                    serviceResponse.Message = MessaageType.NoRecordFound;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                    serviceResponse.Success = false;
                }

            }
            catch
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProprtyDetailsDto>> AddPropertyDetails(AddPropertyDeclarationDto addPropertyDeclarationDto)
        {
            ServiceResponse<GetProprtyDetailsDto> serviceResponse = new ServiceResponse<GetProprtyDetailsDto>();
            try
            {
                if (_context != null)
                {
                    //create the customize the notice id Ex: NOT001
                    var meetings = await _context.PropertyDeclaration.ToListAsync();
                    int rowcount = meetings.Count + 1;
                    if (rowcount.ToString().Length == 1)
                    {
                        addPropertyDeclarationDto.PropertyId = "PROP00" + rowcount;
                    }
                    else if (rowcount.ToString().Length == 2)
                    {
                        addPropertyDeclarationDto.PropertyId = "MT0" + rowcount;
                    }
                    else
                    {
                        addPropertyDeclarationDto.PropertyId = "MT" + rowcount;
                    }

                    addPropertyDeclarationDto.EntryDate = DateTime.Now;
                    addPropertyDeclarationDto.ActiveStatus = true;
                    

                    await _context.PropertyDeclaration.AddAsync(_mapper.Map<PropertyDeclaration>(addPropertyDeclarationDto));
                    await _context.SaveChangesAsync();
                    var data = _mapper.Map<GetProprtyDetailsDto>(_context.PropertyDeclaration.LastOrDefault());
                    serviceResponse.Data = data;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                    serviceResponse.Message = MessaageType.Saved;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnSave;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch
            {
                serviceResponse.Success = true;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
                serviceResponse.Message = MessaageType.FailureOnException;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProprtyDetailsDto>> UpdatePropertyDetails(string propertyid, UpdatePropertyDeclarationDto updatePropertyDeclarationDto)
        {
            ServiceResponse<GetProprtyDetailsDto> serviceResponse = new ServiceResponse<GetProprtyDetailsDto>();
            try
            {
                PropertyDeclaration property = await _context.PropertyDeclaration.FindAsync(propertyid);
                if (property != null)
                {
                    property.NameOfProperty = updatePropertyDeclarationDto.NameOfProperty;
                    property.NoOfCounts = updatePropertyDeclarationDto.NoOfCounts;
                    property.PurchaseDate = updatePropertyDeclarationDto.PurchaseDate;
                    property.PropertyDescription = updatePropertyDeclarationDto.PropertyDescription;
                    property.Price = updatePropertyDeclarationDto.Price;
                    property.ActiveStatus = updatePropertyDeclarationDto.ActiveStatus;
                    _context.Entry(property).State = EntityState.Modified;
                    _context.Update(property).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetProprtyDetailsDto>(property);
                    serviceResponse.Message = MessaageType.Updated;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.FailureOnUpdate;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProprtyDetailsDto>> DeletePropertyDetails(string prpid)
        {
            ServiceResponse<GetProprtyDetailsDto> serviceResponse = new ServiceResponse<GetProprtyDetailsDto>();

            PropertyDeclaration properties = await _context.PropertyDeclaration.FindAsync(prpid);
            try
            {
                if (properties != null)
                {
                    properties.ActiveStatus = false;
                    properties.DeleteDate = DateTime.Now;

                    _context.Entry(properties).State = EntityState.Modified;
                    _context.Update(properties).Property(x => x.Id).IsModified = false;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetProprtyDetailsDto>(properties);
                    serviceResponse.Message = MessaageType.Deleted;
                    serviceResponse.Success = true;
                    serviceResponse.Response = (int)ResponseType.Ok;
                }
                else
                {
                    serviceResponse.Message = MessaageType.DeletionFailed;
                    serviceResponse.Success = false;
                    serviceResponse.Response = (int)ResponseType.NoConnect;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = MessaageType.FailureOnException;
                serviceResponse.Success = false;
                serviceResponse.Response = (int)ResponseType.InternalServerError;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProprtyDetailsDto>> GetPropertydetailsById(string prpid)
        {
            ServiceResponse<GetProprtyDetailsDto> serviceResponse = new ServiceResponse<GetProprtyDetailsDto>();
            var duplex = await _context.PropertyDeclaration.Where(x => x.PropertyId == prpid && x.ActiveStatus == true).FirstOrDefaultAsync();
            if (duplex != null)
            {
                serviceResponse.Data = _mapper.Map<GetProprtyDetailsDto>(duplex);
                serviceResponse.Message = MessaageType.RecordFound;
                serviceResponse.Response = (int)ResponseType.Ok;
                serviceResponse.Success = true;

            }
            else
            {
                serviceResponse.Message = MessaageType.NoRecordFound;
                serviceResponse.Response = (int)ResponseType.NoConnect;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        #endregion

    }
}
