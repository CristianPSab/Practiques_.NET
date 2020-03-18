using Academy.Lib.Repositories;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Teacher { get; set; }

        #region Static Validations
        public static ValidationResult<string> ValidateName(string name, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("el nombre de la asignatura no puede estar vacío");
            }

            #region check duplication

            var repo = Entity.DepCon.Resolve<IRepository<Subject>>();
            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Name == name);

            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("ya existe un alumno con ese nombre");
            }
            else if (currentId != default && entityWithDni != null && entityWithDni.Id != currentId)
            {
                if (entityWithDni.Name == name)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("ya existe un alumno con ese nombre");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }


        public static ValidationResult<string> ValidateTeacher(string name)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("el nombre del Profesor no puede estar vacío");
            }

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }
        #endregion

  

        #region Domain Validations

        public void ValidateName(ValidationResult validationResult)
        {
            var vr = ValidateName(this.Name, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateTeacher(ValidationResult validationResult)
        {
            var vr = ValidateTeacher(this.Teacher);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        #endregion

        public SaveResult<Subject> Save()
        {
            return base.Save<Subject>();
        }

        public DeleteResult<Subject> Delete()
        {
            return base.Delete<Subject>();
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            // cambiar ValidateName para que sea igual que ValidateDni
            ValidateName(output);
            ValidateTeacher(output);

            return output;
        }
        public Subject Clone()
        {
            return Clone<Subject>();
        }

        public override T Clone<T>()
        {
            var output = base.Clone<T>() as Subject;

            output.Name = this.Name;
            output.Teacher = this.Teacher;

            return output as T;
        }


        public Subject()
        {

        }
    }
}
