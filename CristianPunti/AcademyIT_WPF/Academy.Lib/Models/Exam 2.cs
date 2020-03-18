using Academy.Lib.Repositories;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Linq;

namespace Academy.Lib.Models
{
    public class Exam : Entity
    {

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public Subject Subject
        {
            get
            {
                var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                var subject = repo.Find(SubjectId);
                return subject;
            }
        }

        public Guid SubjectId { get; set; }

        public SaveResult<Exam> Save()
        {
            var saveResult = base.Save<Exam>();

            return saveResult;
        }

        public DeleteResult<Exam> Delete()
        {
            var deleteResult = base.Delete<Exam>();

            return deleteResult;
        }

        #region Static Validations
        public static ValidationResult<string> ValidateTitle(string title, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(title))
            {
                output.IsSuccess = false;
                output.Errors.Add("El título no puede estar vacío");

            }

            #region check duplication

            var repo = Entity.DepCon.Resolve<IRepository<Exam>>();
            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Title == title);

            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("ya existe un examen con este título");
            }
            else if (currentId != default && entityWithDni != null && entityWithDni.Id != currentId)
            {
                if (entityWithDni.Title == title)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("ya existe un examen con este título");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = title;

            return output;
        }

        public static ValidationResult<string> ValidateText(string text, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(text))
            {
                output.IsSuccess = false;
                output.Errors.Add("El texto no puede estar vacío");
            }

            if (output.IsSuccess)
                output.ValidatedResult = text;

            return output;
        }

        public static ValidationResult<Subject> ValidateSubject(Subject subject, Guid currentId = default)
        {
            var output = new ValidationResult<Subject>()
            {
                IsSuccess = true
            };

            if (subject == null)
            {
                output.IsSuccess = false;
                output.Errors.Add("La asignatura no puede estar vacía");
            }

            if (output.IsSuccess)
                output.ValidatedResult = subject;

            return output;
        }


        #endregion

        #region Domain Validations
        public void ValidateTitle(ValidationResult validationResult)
        {
            var vr = ValidateTitle(this.Title, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        public void ValidateText(ValidationResult validationResult)
        {
            var vr = ValidateText(this.Text, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        public void ValidateSubject(ValidationResult validationResult)
        {
            var vr = ValidateSubject(this.Subject, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        #endregion

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateTitle(output);
            ValidateText(output);
            ValidateSubject(output);

            return output;
        }

        public override T Clone<T>()
        {
            var output = base.Clone<T>() as Exam;

            output.Title = this.Title;
            output.Text = this.Text;
            output.Date = this.Date;

            return output as T;
        }


        public Exam Clone()
        {
            return Clone<Exam>();
        }
        public Exam()
        {

        }


    }
}
