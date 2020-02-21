using Academy.Lib.DAL.Repositories;
using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using Common.Lib.DAL.EFCore;
using System;
using Academy.Lib.DAL;
using Microsoft.EntityFrameworkCore;
using Academy.App.WPF.DbContextFactory;

namespace Academy.App.WPF
{
    public class Bootstrapper
    {
        public IDependencyContainer Init()
        {
            var depCon = new SimpleDependencyContainer();

            RegisterRepositories(depCon);

            Entity.DepCon = depCon;

            return depCon;
        }

        public void RegisterRepositories(IDependencyContainer depCon)
        {
            var studentRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new StudentsRepository(GetDbConstructor());
            });

            var subjectsRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<Subject>(GetDbConstructor());
            });

            var examsRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<Exam>(GetDbConstructor());
            });

            var studentsSubjectsRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<StudentSubject>(GetDbConstructor());
            });

            var studentsExamsRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<StudentExam>(GetDbConstructor());
            });

            depCon.Register<IRepository<Student>, StudentsRepository>(studentRepoBuilder);
            depCon.Register<IStudentsRepository, StudentsRepository>((parameters) => new StudentsRepository(GetDbConstructor()));


            depCon.Register<IRepository<Subject>, EfCoreRepository<Subject>>(subjectsRepoBuilder);

            depCon.Register<IRepository<Exam>, EfCoreRepository<Exam>>(examsRepoBuilder);


            depCon.Register<IRepository<StudentSubject>, EfCoreRepository<StudentSubject>>(studentsSubjectsRepoBuilder);

            depCon.Register<IRepository<StudentExam>, EfCoreRepository<StudentExam>>(studentsExamsRepoBuilder);


        }

        private static AcademyDbContext GetDbConstructor()
        {
            var factory = new AcademyContextFactory();
            return factory.CreateDbContext(null);
        }
    }
}
