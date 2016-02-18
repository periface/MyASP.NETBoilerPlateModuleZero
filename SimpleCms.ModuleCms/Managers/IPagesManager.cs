using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleCms.Entities;

namespace SimpleCms.ModuleCms.Managers
{
    public interface IPagesManager : IDomainService
    {
        Task<int> CreatePageAsync(Page input);
        Task<Page> GetPage(int idPage);
        Task UpdatePageAsync(Page page);
        Task<IEnumerable<Page>> GetPagesAsync(Expression<Func<Page,bool>> predicate);
        Task DeletePageAsync(Page page);
    }
}
