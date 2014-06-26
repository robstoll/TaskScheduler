using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BLDLMapper.Interfaces;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Dtos;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.Requests;

namespace CH.Tutteli.TaskScheduler.BLDLMapper
{
    public class Mapper : IMapper
    {


        private static Func<OneTimeTaskRequest, OneTimeTaskDto> oneTimeBLToDL = ToDL;
        private static Func<DailyTaskRequest, DailyTaskDto> dailyBLToDL = ToDL;
        private static Func<WeeklyTaskRequest, WeeklyTaskDto> weeklyBLToDL = ToDL;
        private static Func<MonthlyTaskRequest, MonthlyTaskDto> mohtlyBLToDL = ToDL;
        private static IDictionary<Type, Tuple<Type, object>> blToDLMappers = new Dictionary<Type, Tuple<Type, object>>
        {
            {typeof(OneTimeTaskRequest), Tuple(typeof(OneTimeTaskDto), oneTimeBLToDL)},
            {typeof(DailyTaskRequest), Tuple(typeof(DailyTaskDto), dailyBLToDL)},
            {typeof(WeeklyTaskRequest), Tuple(typeof(WeeklyTaskDto), weeklyBLToDL)},
            {typeof(MonthlyTaskRequest), Tuple(typeof(MonthlyTaskDto), mohtlyBLToDL)}
        };

        private static Func<OneTimeTaskDto, OneTimeTaskRequest> oneTimeDLToBL = ToBL;
        private static Func<DailyTaskDto, DailyTaskRequest> dailyDLToBL = ToBL;
        private static Func<WeeklyTaskDto, WeeklyTaskRequest> weeklyDLToBL = ToBL;
        private static Func<MonthlyTaskDto, MonthlyTaskRequest> mohtlyDLToBL = ToBL;
        private static IDictionary<Type, Tuple<Type, object>> dlToBlMappers = new Dictionary<Type, Tuple<Type, object>>
        {
            {typeof(OneTimeTaskDto), Tuple(typeof(OneTimeTaskRequest), oneTimeDLToBL)},
            {typeof(DailyTaskDto), Tuple(typeof(DailyTaskRequest), dailyDLToBL)},
            {typeof(WeeklyTaskDto), Tuple(typeof(WeeklyTaskRequest), weeklyDLToBL)},
            {typeof(MonthlyTaskDto), Tuple(typeof(MonthlyTaskRequest), mohtlyDLToBL)}
        };

        private static Tuple<Type, object> Tuple(Type t, object m)
        {
            return System.Tuple.Create<Type, object>(t, m);
        }

        public TRequestBL ToBL<TDtoDL, TRequestBL>(TDtoDL dto)
            where TDtoDL : class, DL.Interfaces.ITaskDto, new()
            where TRequestBL : class, ITaskRequest, new()
        {
            var tDto = typeof(TDtoDL);
            var tRequest = typeof(TRequestBL);
            if (dlToBlMappers.ContainsKey(tDto) && dlToBlMappers[tDto].Item1.Equals(tRequest))
            {
                Func<TDtoDL, TRequestBL> func = (Func<TDtoDL, TRequestBL>)dlToBlMappers[tDto].Item2;
                return func(dto);
            }
            throw new ArgumentException("Mapping type " + tDto.FullName + " to " + tRequest.FullName + " is not supported by the mapper");
        }

        public TDtoDL ToDL<TRequestBL, TDtoDL>(TRequestBL request)
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, DL.Interfaces.ITaskDto, new()
        {
            var tRequest = typeof(TRequestBL);
            var tDto = typeof(TDtoDL);
            if (blToDLMappers.ContainsKey(tRequest) && blToDLMappers[tRequest].Item1.Equals(tDto))
            {
                Func<TRequestBL, TDtoDL> func = (Func<TRequestBL, TDtoDL>)blToDLMappers[tRequest].Item2;
                return func(request);
            }
            throw new ArgumentException("Mapping type " + tRequest.FullName + " to " + tDto.FullName + " is not supported by the mapper");
        }

        public IList<TRequestBL> ToBL<TDtoDL, TRequestBL>(IEnumerable<TDtoDL> dtos)
            where TDtoDL : class, DL.Interfaces.ITaskDto, new()
            where TRequestBL : class, ITaskRequest, new()
        {
            var requests = new List<TRequestBL>();
            foreach (var dto in dtos)
            {
                requests.Add(ToBL<TDtoDL, TRequestBL>(dto));
            }
            return requests;
        }

        public IList<TDtoDL> ToDL<TRequestBL, TDtoDL>(IEnumerable<TRequestBL> requests)
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, DL.Interfaces.ITaskDto, new()
        {
            var dtos = new List<TDtoDL>();
            foreach (var request in requests)
            {
                dtos.Add(ToDL<TRequestBL, TDtoDL>(request));
            }
            return dtos;
        }

        private static Requests.OneTimeTaskRequest ToBL(DL.Dtos.OneTimeTaskDto request)
        {
            var result = new Requests.OneTimeTaskRequest();
            MapBasicTaskRequestPropertiesFromTo(request, result);
            result.Trigger = request.Trigger;
            return result;
        }

        private static DL.Dtos.OneTimeTaskDto ToDL(Requests.OneTimeTaskRequest request)
        {
            var result = new DL.Dtos.OneTimeTaskDto();
            MapBasicTaskRequestPropertiesFromTo(request, result);
            result.Trigger = request.Trigger;
            return result;
        }

        private static void MapBasicTaskRequestPropertiesFromTo(ITaskRequest request, ITaskRequest result)
        {
            result.Id = request.Id;
            result.Name = request.Name;
            result.Description = request.Description;
            result.CallbackUrl = request.CallbackUrl;
            result.DateCreated = request.DateCreated;
            result.DateUpdated = request.DateUpdated;
        }

        private static Requests.DailyTaskRequest ToBL(DL.Dtos.DailyTaskDto request)
        {
            var result = new Requests.DailyTaskRequest();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursEveryXDays = request.RecursEveryXDays;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }

        private static DL.Dtos.DailyTaskDto ToDL(Requests.DailyTaskRequest request)
        {
            var result = new DL.Dtos.DailyTaskDto();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursEveryXDays = request.RecursEveryXDays;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }

        private static void MapReccuringTaskRequestPropertiesFromTo(IRecurringTaskRequest request, IRecurringTaskRequest result)
        {
            MapBasicTaskRequestPropertiesFromTo(request, result);
            result.StartDate = request.StartDate;
            result.EndDate = request.EndDate;
        }

        private static Requests.WeeklyTaskRequest ToBL(DL.Dtos.WeeklyTaskDto request)
        {
            var result = new Requests.WeeklyTaskRequest();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursEveryXWeeks = request.RecursEveryXWeeks;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }

        private static DL.Dtos.WeeklyTaskDto ToDL(Requests.WeeklyTaskRequest request)
        {
            var result = new DL.Dtos.WeeklyTaskDto();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursEveryXWeeks = request.RecursEveryXWeeks;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }

        private static Requests.MonthlyTaskRequest ToBL(DL.Dtos.MonthlyTaskDto request)
        {
            var result = new Requests.MonthlyTaskRequest();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursOnMonth = request.RecursOnMonth;
            result.RecursOnDayOfMonth = request.RecursOnDayOfMonth;
            result.RecursOnSpecialDayOfMonth = request.RecursOnSpecialDayOfMonth;
            return result;
        }

        private static DL.Dtos.MonthlyTaskDto ToDL(Requests.MonthlyTaskRequest request)
        {
            var result = new DL.Dtos.MonthlyTaskDto();
            MapReccuringTaskRequestPropertiesFromTo(request, result);
            result.RecursOnMonth = request.RecursOnMonth;
            result.RecursOnDayOfMonth = request.RecursOnDayOfMonth;
            result.RecursOnSpecialDayOfMonth = request.RecursOnSpecialDayOfMonth;
            return result;
        }
    }
}
