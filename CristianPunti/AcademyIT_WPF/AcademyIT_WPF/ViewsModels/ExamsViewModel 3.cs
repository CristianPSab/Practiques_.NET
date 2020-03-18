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
    public class ExamsViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
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

        private Subject _currentSubjectEVM;
        public Subject CurrentSubjectEVM
        {
            get
            {
                return _currentSubjectEVM;
            }
            set
            {
                _currentSubjectEVM = value;
                OnPropertyChanged();
            }
        }

        private Exam _currentExamEV;
        public Exam CurrentExamEV
        {
            get
            {
                return _currentExamEV;
            }
            set
            {
                _currentExamEV = value;

                if (CurrentExamEV != null)
                {
                    this.Title = CurrentExamEV.Title;
                    this.Text = CurrentExamEV.Text;
                }

                OnPropertyChanged();
            }
        }

        List<Subject> _subjectsListEV;
        public List<Subject> SubjectsListEV
        {
            get
            {
                return _subjectsListEV;
            }
            set
            {
                _subjectsListEV = value;
                OnPropertyChanged();
            }
        }

        List<Subject> _subjectsNameList;
        public List<Subject> SubjectsNameList
        {
            get
            {
                return _subjectsNameList;
            }
            set
            {
                _subjectsNameList = value;
                OnPropertyChanged();
            }
        }

        List<Exam> _examsListEV;
        public List<Exam> ExamsListEV
        {
            get
            {
                return _examsListEV;
            }
            set
            {
                _examsListEV = value;
                OnPropertyChanged();
            }
        }

        public ExamsViewModel()
        {
            GetSubjectsEVCommand = new RouteCommand(GetSubjectsEV);
            GetSubjectsNameEVCommand = new RouteCommand(GetSubjectsNameEV);
            AddExamCommand = new RouteCommand(SaveExamEV);
            GetExamsCommand = new RouteCommand(GetExamsEV);
            DeleteExamCommand = new RouteCommand(DelExamEV);
            UpdateExamCommand = new RouteCommand(EditExamEV);

            Date = DateTime.Now;

        }
        private void Clear()
        {
            this.Title = null;
            this.Text = null;
            this.Date = DateTime.Now;
            this.CurrentSubjectEVM = default;
            this.Id = default;

        }
        private void EditExamEV()
        {
            ValidationResult<string> vrTitle;
            ValidationResult<string> vrText;
            ValidationResult vrSubject;

            if (CurrentExamEV != null)
            {
                if (!(vrTitle = Exam.ValidateTitle(this.Title, CurrentExamEV.Id)).IsSuccess)
                {
                    MessageBox.Show(vrTitle.AllErrors);
                }

                if (!(vrText = Exam.ValidateText(this.Text)).IsSuccess)
                {
                    MessageBox.Show(vrText.AllErrors);
                }

                if (!(vrSubject = Exam.ValidateSubject(this.CurrentSubjectEVM)).IsSuccess)
                {
                    MessageBox.Show(vrSubject.AllErrors);
                }

                if (vrTitle.IsSuccess && vrText.IsSuccess && vrSubject.IsSuccess)
                {
                    var examToUpdate = CurrentExamEV.Clone();

                    examToUpdate.Title = vrTitle.ValidatedResult;
                    examToUpdate.Text = vrText.ValidatedResult;
                    examToUpdate.Date = this.Date;
                    examToUpdate.SubjectId = this.CurrentSubjectEVM.Id;

                    var sr = examToUpdate.Save();

                    if (sr.IsSuccess)
                    {
                        MessageBox.Show($"El examen se ha editado correctamente");
                        CurrentExamEV = sr.Entity;
                    }
                    else
                    {
                        MessageBox.Show($"Uno o más errores han ocurrido y el exámen no se ha editado correctamente: {sr.AllErrors}");
                    }
                }

                Clear();
                GetExamsEV();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar un examen antes de editarlo");
            }

        }

        private void DelExamEV()
        {
            if (CurrentExamEV != null)
            {
                var repo = Entity.DepCon.Resolve<IRepository<Exam>>();

                var s = repo.Find(CurrentExamEV.Id);

                if (s != null)
                {
                    var dr = s.Delete();
                    if (dr.IsSuccess)
                        MessageBox.Show($"Examen eliminado correctamente");
                    else
                        MessageBox.Show($"Uno o más errores han ocurrido y la asignatura no se ha eliminado correctamente: {dr.AllErrors}");

                }

                GetExamsEV();

            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar un examen antes de eliminarlo");
            }
        }

        private void GetExamsEV()
        {
            var repo = Entity.DepCon.Resolve<IRepository<Exam>>();
            ExamsListEV = repo.QueryAll().ToList();
        }

        private void SaveExamEV()
        {
            ValidationResult<string> vrTitle;
            ValidationResult<string> vrText;
            ValidationResult vrSubject;


            if (!(vrTitle = Exam.ValidateTitle(this.Title)).IsSuccess)
            {
                MessageBox.Show(vrTitle.AllErrors);
            }

            if (!(vrText = Exam.ValidateText(this.Text)).IsSuccess)
            {
                MessageBox.Show(vrText.AllErrors);
            }

            if (!(vrSubject = Exam.ValidateSubject(this.CurrentSubjectEVM)).IsSuccess)
            {
                MessageBox.Show(vrSubject.AllErrors);
            }

            if (vrTitle.IsSuccess && vrText.IsSuccess && vrSubject.IsSuccess)
            {
                Exam examVM = new Exam
                {
                    Title = vrTitle.ValidatedResult,
                    Text = vrText.ValidatedResult,
                    Id = this.Id,
                    Date = this.Date,
                    SubjectId = this.CurrentSubjectEVM.Id
                };

                var sr = examVM.Save();

                if (sr.IsSuccess)
                {
                    MessageBox.Show($"El examen se ha guardado correctamente");
                }
                else
                {
                    MessageBox.Show($"Uno o más errores han ocurrido y el exámen no se ha guardado correctamente: {sr.AllErrors}");
                }
            }

            Clear();
            GetExamsEV();
        }


        private void GetSubjectsNameEV()
        {
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectsNameList = repo.QueryAll().ToList();
        }

        private void GetSubjectsEV()
        {
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            SubjectsListEV = repo.QueryAll().ToList();
        }

        public RouteCommand GetSubjectsEVCommand { get; set; }
        public RouteCommand GetSubjectsNameEVCommand { get; set; }
        public RouteCommand AddExamCommand { get; set; }
        public RouteCommand GetExamsCommand { get; set; }
        public RouteCommand DeleteExamCommand { get; set; }
        public RouteCommand UpdateExamCommand { get; set; }
    }
}
