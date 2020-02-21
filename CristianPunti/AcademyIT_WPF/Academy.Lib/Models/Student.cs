using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Common.Lib.Core.Context;
using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System.Text.RegularExpressions;
using Academy.Lib.Repositories;
using System.Collections.ObjectModel;

namespace Academy.Lib.Models
{


    public class Student : Entity
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public int ChairNumber { get; set; }

        #region Static Validations

        public static ValidationResult<string> ValidateDni(string dni, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };


            Regex regex = new Regex(@"/ ^[a - zA - Z0 - 9]{ 5,10}$/");

            Match match = regex.Match(Convert.ToString(string.IsNullOrEmpty(dni)));

            if (string.IsNullOrEmpty(dni) || match.Success)
            {
                output.IsSuccess = false;
                output.Errors.Add("el dni está en formato incorrecto, vuelva a escribirlo");
            }

            #region check duplication

            var repo = Entity.DepCon.Resolve<IRepository<Student>>();
            var entityWithDni = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);

            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add("ya existe un alumno con ese dni");
            }
            else if (currentId != default && entityWithDni != null && entityWithDni.Id != currentId)
            {
                if (entityWithDni.Dni == dni)
                {
                    // on update
                    output.IsSuccess = false;
                    output.Errors.Add("ya existe un alumno con ese dni");
                }

            }
            #endregion

            if (output.IsSuccess)
            {
                output.ValidatedResult = dni;
            }

            return output;
        }

        public static ValidationResult<string> ValidateName(string name)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("el nombre del alumno no puede estar vacío");
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = name;
            }
            return output;
        }
        public static ValidationResult<int> ValidateChairNumber(string chairNumberText, Guid currentId = default)
        {
            var output = new ValidationResult<int>()
            {
                IsSuccess = true
            };

            var chairNumber = 0;
            var isConversionOk = false;

            #region check null or empty
            if (string.IsNullOrEmpty(chairNumberText))
            {
                output.IsSuccess = false;
                output.Errors.Add("el número de la silla no puede estar vacío o nulo");
            }
            #endregion

            #region check format conversion

            isConversionOk = int.TryParse(chairNumberText, out chairNumber);

            if (!isConversionOk)
            {
                output.IsSuccess = false;
                output.Errors.Add($"no se puede convertir {chairNumber} en número");
            }

            #endregion

            #region check if the char is already in use

            if (isConversionOk)
            {
                var repoStudents = Entity.DepCon.Resolve<IRepository<Student>>();

                var currentStudentInChair = repoStudents.QueryAll().FirstOrDefault(s => s.ChairNumber == chairNumber);

                if (currentId == default && currentStudentInChair != null)
                {
                    //Guardar
                    output.IsSuccess = false;
                    output.Errors.Add($"ya hay un alumno {currentStudentInChair.Name} en la silla {chairNumber}");
                }
                else if (currentId != default && currentStudentInChair != null && currentStudentInChair.Id != currentId)
                {
                    //Editar
                    if (currentStudentInChair.ChairNumber == chairNumber)
                    {

                        output.IsSuccess = false;
                        output.Errors.Add($"ya hay un alumno {currentStudentInChair.Name} en la silla {chairNumber}");
                    }
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = chairNumber;

            return output;
        }

        #endregion
 

        #region Domain Validations

        public void ValidateName(ValidationResult validationResult)
        {
            var validateNameResult = ValidateName(this.Name);
            if (!validateNameResult.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add(Convert.ToString(validateNameResult.Errors));
            }
        }

        public void ValidateDni(ValidationResult validationResult)
        {
            var vr = ValidateDni(this.Dni, this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add(Convert.ToString(vr.Errors));
            }
        }

        public void ValidateChairNumber(ValidationResult validationResult)
        {
            var vr = ValidateChairNumber(this.ChairNumber.ToString(), this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add(Convert.ToString(vr.Errors));
            }
        }

        #endregion

        public SaveResult<Student> Save()
        {
            return base.Save<Student>();
        }

        public DeleteResult<Student> Delete()
        {
            return base.Delete<Student>();
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            // cambiar ValidateName para que sea igual que ValidateDni
            ValidateName(output);
            ValidateDni(output);
            ValidateChairNumber(output);

            return output;
        }


        public override T Clone<T>()
        {
            var output = base.Clone<T>() as Student;

            output.Dni = this.Dni;
            output.Name = this.Name;
            output.ChairNumber = this.ChairNumber;

            return output as T;
        }


        public Student Clone()
        {
            return Clone<Student>();
        }

        public Student()
        {

        }
    }
}
