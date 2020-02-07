using System;
using Academy.Lib.Models;
using Common.Lib.Core.Context;

namespace Academy.Lib.Repositories
{
    public interface IExamsRepository : IRepository<Exam>
    {
        public Exam FindByTitle(string title);
    }
}
