using System;
using Academy.Lib.Models;
using Common.Lib.Core.Context;

namespace Academy.Lib.Repositories
{
    public interface IStudentsRepository : IRepository<Student>
    {
        public Student FindByDni(string dni);
    }
}
