﻿using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_UserLogBusiness : BaseBusiness<Base_UserLog>, IBase_UserLogBusiness, ITransientDependency
    {
        public Base_UserLogBusiness(IRepository repository)
            : base(repository)
        {
        }

        public async Task<List<Base_UserLog>> GetLogListAsync(
            Pagination pagination,
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            var whereExp = LinqHelper.True<Base_UserLog>();
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == logType);
            if (!opUserName.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreatorRealName.Contains(opUserName));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.CreateTime <= endTime);

            return await GetIQueryable().Where(whereExp).GetPagination(pagination).ToListAsync();
        }
    }
}