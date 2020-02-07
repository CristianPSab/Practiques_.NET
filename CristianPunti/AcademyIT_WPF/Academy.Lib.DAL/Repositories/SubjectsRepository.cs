using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.Infrastructure;
using Common.Lib.DAL.EFCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Academy.Lib.DAL.Repositories
{
   public class SubjectsRepository : EfCoreRepository<Subject>, ISubjectsRepository
    {
        static Dictionary<string, Subject> SubjectsByName { get; set; } = new Dictionary<string, Subject>();

        public SubjectsRepository()
        {

        }

        public SubjectsRepository(AcademyDbContext dbcontext)
            : base(dbcontext)
        {

        }

        public override SaveResult<Subject> Add(Subject entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                SubjectsByName.Add(entity.Name, entity);
            }

            return output;
        }

        public override SaveResult<Subject> Update(Subject entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                SubjectsByName[entity.Name] = entity;
            }

            return output;
        }

        public override DeleteResult<Subject> Delete(Subject entity)
        {
            var output = base.Delete(entity);

            if (output.IsSuccess)
            {
                SubjectsByName.Remove(entity.Name);
            }

            return output;
        }

        public Subject FindByName(string name)
        {
            if (SubjectsByName.ContainsKey(name))
                return SubjectsByName[name];

            return null;
        }
    }
}
