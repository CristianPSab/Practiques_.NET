using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Academy.Lib.Models
{
    public class StudentExam : Entity
    {
        public Exam Exam
        {
            get
            {
                var repo = Student.DepCon.Resolve<IRepository<Exam>>();
                var exam = repo.Find(ExamId);
                return exam;
            }
        }
        public Guid ExamId { get; set; }

        public Student Student
        {
            get
            {
                var repo = Student.DepCon.Resolve<IRepository<Student>>();
                var student = repo.Find(StudentId);
                return student;
            }
        }

        public Guid StudentId { get; set; }

        public double Mark { get; set; }

        public bool HasCheated { get; set; }

        public SaveResult<StudentExam> Save()
        {
            var saveResult = base.Save<StudentExam>();

            return saveResult;
        }


        public DeleteResult<StudentExam> Delete()
        {
            var deleteResult = base.Delete<StudentExam>();

            return deleteResult;
        }

        public List<StudentExam> StudentByExams(Guid idStudent)
        {
            var repo = DepCon.Resolve<IRepository<StudentExam>>();
            var entityId = repo.QueryAll().Where(e => e.StudentId == idStudent).ToList();
            return entityId;
        }

        public Exam FindExam(Guid idExam)
        {
            var repo = DepCon.Resolve<IRepository<Exam>>();
            var entity = repo.QueryAll().FirstOrDefault(x => x.Id == idExam);
            return entity;
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateStudentExam(output);
            ValidateStudentSubject(output);
            ValidateMark(output);

            return output;
        }

        public void ValidateMark(ValidationResult validationResult)
        {
            var vr = ValidateMark(this.Mark.ToString(), this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }


        public void ValidateStudentSubject(ValidationResult validationResult)
        {

            var vr = ValidateStudentSubject(this.StudentId, this.ExamId, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }


        public void ValidateStudentExam(ValidationResult validationResult)
        {
            var vr = ValidateStudentExam(this.StudentId, this.ExamId, this.Id);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public ValidationResult<string> ValidateStudentSubject(Guid studentId, Guid examId, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            var studentSubject = new StudentSubject();
            var studentBySubjects = new List<StudentSubject>();
            studentBySubjects = studentSubject.StudentBySubjects(studentId);

            var exam = FindExam(ExamId);

            if (exam == null)
            {
                output.IsSuccess = false;
                output.Errors.Add("No has seleccionado ningún Examen");

            }
            else
            {
                studentSubject = studentBySubjects.FirstOrDefault(x => x.SubjectId == exam.SubjectId);

                //On Delete
                if (currentId != default)
                {
                    output.IsSuccess = true;
                }

                //On Create
                else
                {
                    if (studentId == default)
                    {
                        output.IsSuccess = false;
                        output.Errors.Add("El estudiante no puede estar vacío");

                    }
                    else
                    {
                        if (exam.SubjectId == default)
                        {
                            output.IsSuccess = false;
                            output.Errors.Add("La asignatura no puede estar vacía");
                        }
                        else
                        {
                            if (studentSubject == null)
                            {
                                output.IsSuccess = false;
                                output.Errors.Add("El estudiante no está matriculado de la asignatura");
                            }
                        }
                    }
                }
            }
                return output;
        }

        public ValidationResult<string> ValidateStudentExam(Guid studentId, Guid examId, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            var studentByExams = new List<StudentExam>();
            studentByExams = StudentByExams(studentId);

            //On Delete
            if (currentId != default)
            {
                output.IsSuccess = true;
            }

            //On Create
            else
            {
                if (examId == default)
                {
                    output.IsSuccess = false;
                    output.Errors.Add("El Examen no puede estar vacío");
                }
                else
                {
                    if (ExamId == default)
                    {
                        output.IsSuccess = false;
                        output.Errors.Add("la Asignatura no puede estar vacía");

                    }
                    else
                    {
                        var repo = DepCon.Resolve<IRepository<StudentSubject>>();

                        if (studentByExams != null && studentByExams.Any(x => x.ExamId == examId))
                        {
                            output.IsSuccess = false;
                            output.Errors.Add("Este exámen ya esta registrado.");
                        }
                    }
                }
            }

            return output;
        }

        public static ValidationResult<double> ValidateMark(string markT, Guid currentId = default)
        {
            var output = new ValidationResult<double>()
            {
                IsSuccess = true
            };


            var mark = 0.0;
            var isConversionOk = false;

            #region check null or empty
            if (string.IsNullOrEmpty(markT))
            {
                output.IsSuccess = false;
                output.Errors.Add("La nota no puede estar vacía o nula");
            }
            #endregion


            #region check format conversion

            isConversionOk = double.TryParse(markT.Replace(".", ","), out mark);
            if (!isConversionOk)
            {
                output.IsSuccess = false;
                output.Errors.Add($"no se puede convertir {markT} en número");
            }
            #endregion

            if(output.IsSuccess)
            {
                if(mark >= 0 && mark <= 10)
                {
                    output.ValidatedResult = mark;
                }
                else
                {
                    output.IsSuccess = false;
                    output.Errors.Add("La nota no es correcta");
                }
            }
            

            return output;
        }

        public override T Clone<T>()
        {
            var output = base.Clone<T>() as StudentExam;

            
            output.Mark = this.Mark;

            return output as T;
        }


        public StudentExam Clone()
        {
            return Clone<StudentExam>();
        }


        public StudentExam()
        {

        }
    }
}
