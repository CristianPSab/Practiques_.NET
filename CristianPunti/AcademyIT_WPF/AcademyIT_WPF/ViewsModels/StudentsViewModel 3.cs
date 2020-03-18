using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

using Academy.Lib.Models;
using Academy.UI;
using Academy.Lib.Repositories;
using Academy.Lib.DAL.Repositories;
using Academy.Lib.DAL;
using Common.Lib.Core;
using Common.Lib.Infrastructure;
using Common.Lib.Core.Context;

namespace Academy.App.WPF.ViewsModels
{
    public class StudentsViewModel : ViewModelBase
    {
        public List<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
        List<Student> _students;

        public string Dni
        {
            get
            {
                return _dni;
            }
            set
            {
                _dni = value;
                OnPropertyChanged();
            }
        }
        string _dni;

        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }
        string _nombre;

        public string Silla
        {
            get
            {
                return _silla;
            }
            set
            {
                _silla = value;
                OnPropertyChanged();
            }
        }

        private Guid _id = default;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        string _silla;

        public string Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }
        string _mensaje;

        private Student _currentStudent;
        public Student CurrentStudent
        {
            get
            {
                return _currentStudent;
            }
            set
            {
                _currentStudent = value;

                if (CurrentStudent != null)
                {
                    this.Dni = CurrentStudent.Dni;
                    this.Nombre = CurrentStudent.Name;
                    this.Silla = Convert.ToString(CurrentStudent.ChairNumber);
                }

                OnPropertyChanged();
            }
        }


        public StudentsViewModel()
        {
            AddStudentCommand = new RouteCommand(SaveStudent);
            UpdateStudentCommand = new RouteCommand(UpdateStudent);
            GetStudentsCommand = new RouteCommand(GetStudents);
            DeleteStudentCommand = new RouteCommand(DeleteStudents);
        }

        private void Clear()
        {
            this.Nombre = null;
            this.Dni = null;
            this.Silla = null;
            this.Id = default;
        }

        public void SaveStudent()
        {
            ValidationResult<string> vrDni;
            ValidationResult<string> vrName;
            ValidationResult<int> vrChair;

            if (!(vrDni = Student.ValidateDni(this.Dni)).IsSuccess)
            {
                MessageBox.Show(vrDni.AllErrors);
            }

            if (!(vrName = Student.ValidateName(this.Nombre)).IsSuccess)
            {
                MessageBox.Show(vrName.AllErrors);
            }

            if (!(vrChair = Student.ValidateChairNumber(this.Silla)).IsSuccess)
            {
                MessageBox.Show(vrChair.AllErrors);
            }

            if (vrDni.IsSuccess && vrName.IsSuccess && vrChair.IsSuccess)
            {
                Student studentVM = new Student
                {
                    Dni = vrDni.ValidatedResult,
                    Name = vrName.ValidatedResult,
                    ChairNumber = vrChair.ValidatedResult
                };

                var sr = studentVM.Save();

                if (sr.IsSuccess)
                {
                    MessageBox.Show($"alumno guardado correctamente");
                }
                else
                {

                    MessageBox.Show($"uno o más errores han ocurrido y el alumnno no se ha guardado correctamente: {sr.AllErrors}");

                }
            }

            Clear();
            GetStudents();
        }

        public void UpdateStudent()
        {

            if (CurrentStudent != null)
            {
                ValidationResult<string> vrDni;
                ValidationResult<string> vrName;
                ValidationResult<int> vrChair;

                if (!(vrDni = Student.ValidateDni(this.Dni, CurrentStudent.Id)).IsSuccess)
                {
                    MessageBox.Show(vrDni.AllErrors);
                }

                if (!(vrName = Student.ValidateName(this.Nombre)).IsSuccess)
                {
                    MessageBox.Show(vrName.AllErrors);
                }

                if (!(vrChair = Student.ValidateChairNumber(this.Silla, CurrentStudent.Id)).IsSuccess)
                {
                    MessageBox.Show(vrChair.AllErrors);
                }

                if (vrDni.IsSuccess && vrName.IsSuccess && vrChair.IsSuccess)
                {
                    var studentToUpdate = CurrentStudent.Clone();

                    studentToUpdate.Dni = vrDni.ValidatedResult;
                    studentToUpdate.Name = vrName.ValidatedResult;
                    studentToUpdate.ChairNumber = vrChair.ValidatedResult;

                    var sr = studentToUpdate.Save();

                    if (sr.IsSuccess)
                    {
                        MessageBox.Show($"alumno editado correctamente");
                        CurrentStudent = sr.Entity;
                    }
                    else
                    {
                        MessageBox.Show($"uno o más errores han ocurrido y el alumno no se ha editar correctamente: {sr.AllErrors}");
                    }
                }
                Clear();
                GetStudents();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar un estudiante antes de editarlo");
            }
        }

        public void GetStudents()
        {
            var repo = Entity.DepCon.Resolve<IRepository<Student>>();
            Students = repo.QueryAll().ToList();
        }

        public void DeleteStudents()
        {
            if (CurrentStudent != null)
            {
                var repo = Entity.DepCon.Resolve<IStudentsRepository>();
                var s = repo.Find(CurrentStudent.Id);

                if (s != null)
                {
                    var dr = s.Delete();
                    if (dr.IsSuccess)
                        MessageBox.Show($"alumno eliminado correctamente");
                    else
                        MessageBox.Show($"uno o más errores han ocurrido y el alumno no se ha eliminado correctamente: {dr.AllErrors}");
                }


                GetStudents();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar un estudiante antes de eliminarlo");
            }
        }

        #region Commands
        public ICommand AddStudentCommand { get; set; }
        public ICommand UpdateStudentCommand { get; set; }
        public ICommand GetStudentsCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        #endregion

    }
}
