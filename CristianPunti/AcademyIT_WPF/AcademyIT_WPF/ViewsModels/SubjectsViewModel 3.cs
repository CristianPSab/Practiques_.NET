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
    public class SubjectsViewModel : ViewModelBase
    {
        private string _profesor;
        public string Profesor
        {
            get
            {
                return _profesor;
            }
            set
            {
                _profesor = value;
                OnPropertyChanged();
            }
        }

        private string _nombreAsignatura;
        public string NombreAsignatura
        {
            get
            {
                return _nombreAsignatura;
            }
            set
            {
                _nombreAsignatura = value;
                OnPropertyChanged();
            }
        }

        private Subject _currentSubject;
        public Subject CurrentSubject
        {
            get
            {
                return _currentSubject;
            }
            set
            {
                _currentSubject = value;

                if (CurrentSubject != null)
                {
                    this.NombreAsignatura = CurrentSubject.Name;
                    this.Profesor = CurrentSubject.Teacher;
                }

                OnPropertyChanged();
            }
        }

        List<Subject> _subjects;
        public List<Subject> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
                OnPropertyChanged();
            }
        }

        public SubjectsViewModel()
        {
            AddSubjectCommand = new RouteCommand(SaveSubject);
            GetSubjectsCommand = new RouteCommand(GetSubjects);
            DelSubjectCommand = new RouteCommand(DeleteSubject);
            EditSubjectCommand = new RouteCommand(EditSubject);
        }

        public void SaveSubject()
        {
            ValidationResult<string> vrNameSubject;
            ValidationResult<string> vrTeacher;

            if (!(vrNameSubject = Subject.ValidateName(this.NombreAsignatura)).IsSuccess)
            {
                MessageBox.Show(vrNameSubject.AllErrors);
            }

            if (!(vrTeacher = Subject.ValidateTeacher(this.Profesor)).IsSuccess)
            {
                MessageBox.Show(vrTeacher.AllErrors);
            }

            if (vrNameSubject.IsSuccess && vrTeacher.IsSuccess)
            {
                Subject subjectVM = new Subject
                {
                    Name = vrNameSubject.ValidatedResult,
                    Teacher = vrTeacher.ValidatedResult
                };

                var sr = subjectVM.Save();

                if (sr.IsSuccess)
                {
                    MessageBox.Show($"Asignatura guardada correctamente");
                }
                else
                {
                    MessageBox.Show($"uno o más errores han ocurrido y la asignatura no se ha guardado correctamente: {sr.AllErrors}");
                }
            }

            Clear();
            GetSubjects();
        }

        private void Clear()
        {
            this.NombreAsignatura = null;
            this.Profesor = null;
        }

        private void EditSubject()
        {
            if (CurrentSubject != null)
            {
                ValidationResult<string> vrNameSubject;
                ValidationResult<string> vrTeacher;

                if (!(vrNameSubject = Subject.ValidateName(this.NombreAsignatura, CurrentSubject.Id)).IsSuccess)
                {
                    MessageBox.Show(vrNameSubject.AllErrors);
                }

                if (!(vrTeacher = Subject.ValidateTeacher(this.Profesor)).IsSuccess)
                {
                    MessageBox.Show(vrTeacher.AllErrors);
                }


                if (vrNameSubject.IsSuccess && vrTeacher.IsSuccess)
                {
                    var subjectToUpdate = CurrentSubject.Clone();

                    subjectToUpdate.Name = vrNameSubject.ValidatedResult;
                    subjectToUpdate.Teacher = vrTeacher.ValidatedResult;

                    var sr = subjectToUpdate.Save();

                    if (sr.IsSuccess)
                    {
                        MessageBox.Show($"Asignatura editada correctamente");
                        CurrentSubject = sr.Entity;
                    }
                    else
                    {
                        MessageBox.Show($"Uno o más errores han ocurrido y la asignatura no se ha editar correctamente: {sr.AllErrors}");
                    }
                }

                Clear();
                GetSubjects();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar una asignatura antes de editarla");
            }
        }

        private void DeleteSubject()
        {
            if (CurrentSubject != null)
            {
                var repo = Entity.DepCon.Resolve<IRepository<Subject>>();

                var s = repo.Find(CurrentSubject.Id);

                if (s != null)
                {
                    var dr = s.Delete();
                    if (dr.IsSuccess)
                        MessageBox.Show($"Asignatura eliminada correctamente");
                    else
                        MessageBox.Show($"uno o más errores han ocurrido y la asignatura no se ha eliminado correctamente: {dr.AllErrors}");
                }

                GetSubjects();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar una asignatura antes de eliminarla");
            }
        }

        private void GetSubjects()
        {
            var repo = Entity.DepCon.Resolve<IRepository<Subject>>();
            Subjects = repo.QueryAll().ToList();
        }
        #region Commands
        public ICommand AddSubjectCommand { get; set; }
        public ICommand GetSubjectsCommand { get; set; }
        public ICommand DelSubjectCommand { get; set; }
        public ICommand EditSubjectCommand { get; set; }

        #endregion

    }
}
