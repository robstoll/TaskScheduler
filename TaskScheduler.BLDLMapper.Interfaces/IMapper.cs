using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Interfaces;

namespace CH.Tutteli.TaskScheduler.BLDLMapper.Interfaces
{
    public interface IMapper
    {
        TRequestBL ToBL<TDtoDL,TRequestBL>(TDtoDL dto) 
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, ITaskDto, new();

        TDtoDL ToDL<TRequestBL, TDtoDL>(TRequestBL request)
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, ITaskDto, new();

        IList<TRequestBL> ToBL<TDtoDL, TRequestBL>(IEnumerable<TDtoDL> dto)
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, ITaskDto, new();

        IList<TDtoDL> ToDL<TRequestBL, TDtoDL>(IEnumerable<TRequestBL> request)
            where TRequestBL : class, ITaskRequest, new()
            where TDtoDL : class, ITaskDto, new();
    }
}
