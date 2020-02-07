using System;
using Academy.Lib.Models;
using Common.Lib.Core.Context;

namespace Academy.Lib.Repositories
{
    public interface ISubjectsRepository : IRepository<Subject>
    {
        public Subject FindByName(string name);
    }
}
