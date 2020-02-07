using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.Infrastructure;
using Common.Lib.DAL.EFCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Academy.Lib.DAL.Repositories
{
    public class ExamsRepository : EfCoreRepository<Exam>, IExamsRepository
    {
        static Dictionary<string, Exam> ExamsByTitle { get; set; } = new Dictionary<string, Exam>();

        public ExamsRepository()
        {

        }

        public ExamsRepository(AcademyDbContext dbcontext)
            : base(dbcontext)
        {

        }

        public override SaveResult<Exam> Add(Exam entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                ExamsByTitle.Add(entity.Title, entity);
            }

            return output;
        }

        public override SaveResult<Exam> Update(Exam entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                ExamsByTitle[entity.Title] = entity;
            }

            return output;
        }

        public override DeleteResult<Exam> Delete(Exam entity)
        {
            var output = base.Delete(entity);

            if (output.IsSuccess)
            {
                ExamsByTitle.Remove(entity.Title);
            }

            return output;
        }

        public Exam FindByTitle(string title)
        {
            if (ExamsByTitle.ContainsKey(title))
                return ExamsByTitle[title];

            return null;
        }

    }
}
