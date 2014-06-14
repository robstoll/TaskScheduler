﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.requests;

namespace ch.tutteli.taskscheduler.triggers
{
    public class TriggerAbstractFactory<T>
    {

    }

    public class TriggerFactory
    {
        private delegate ITrigger Mapper(object obj);

        private static Func<OneTimeTaskRequest, OneTimeTrigger> oneTime = Create;
        private static Func<DailyTaskRequest, DailyTrigger> daily = Create;
        private static Func<WeeklyTaskRequest, WeeklyTrigger> weekly = Create;
        private static Func<MonthlyTaskRequest, MonthlyTrigger> mohtly = Create;
        private static IDictionary<Type, Tuple<Type, object>> mappers = new Dictionary<Type, Tuple<Type, object>>{
            {typeof(OneTimeTaskRequest), Tuple(typeof(OneTimeTrigger), oneTime)},
            {typeof(DailyTaskRequest), Tuple(typeof(DailyTrigger), daily)},
            {typeof(WeeklyTaskRequest), Tuple(typeof(WeeklyTrigger), weekly)},
            {typeof(MonthlyTaskRequest), Tuple(typeof(MonthlyTrigger), mohtly)}
        };

        private static Tuple<Type, object> Tuple(Type t, object m)
        {
            return System.Tuple.Create<Type, object>(t, m);
        }

        public static TTrigger Create<TTrigger, TRequest>(TRequest request)
            where TTrigger : ITrigger
            where TRequest : class, ITaskRequest, new()
        {
            var tRequest = typeof(TRequest);
            var tTrigger = typeof(TTrigger);
            if (mappers.ContainsKey(tRequest) && mappers[tRequest].Item1.Equals(tTrigger))
            {
                Func<TRequest, TTrigger> func = (Func<TRequest, TTrigger>)mappers[tRequest].Item2;
                return func(request);
            }
            else
            {
                throw new ArgumentException("request type " + typeof(TRequest).Name + " is not supported");
            }
        }

        public static object Create<TRequest>(TRequest request)
            where TRequest : class, ITaskRequest, new()
        {
            var tRequest = typeof(TRequest);
            if (mappers.ContainsKey(tRequest))
            {
                Func<TRequest, object> func = (Func<TRequest, object>)mappers[tRequest].Item2;
                return func(request);
            }
            else
            {
                throw new ArgumentException("request type " + typeof(TRequest).Name + " is not supported");
            }
        }

        public static OneTimeTrigger Create(OneTimeTaskRequest request)
        {
            return new OneTimeTrigger(request.Trigger);
        }

        public static DailyTrigger Create(DailyTaskRequest request)
        {
            return new DailyTrigger(request.StartDate, request.EndDate, request.RecursEveryXDays);
        }

        public static WeeklyTrigger Create(WeeklyTaskRequest request)
        {
            return new WeeklyTrigger(
                request.StartDate,
                request.EndDate,
                request.RecursEveryXWeeks,
                request.TriggerWhenDayOfWeek);
        }
        public static MonthlyTrigger Create(MonthlyTaskRequest request)
        {
            var monthlyRecurrence = new MonthlyRecurrence(
                request.RecursOnMonth,
                request.RecursOnDayOfMonth,
                request.RecursOnSpecialDayOfMonth);

            return new MonthlyTrigger(
                 request.StartDate,
                 request.EndDate,
                 monthlyRecurrence);
        }
    }
}