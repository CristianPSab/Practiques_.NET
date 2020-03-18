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
    public class StudentSubjectsViewModel : ViewModelBase
    {
        private string _dniS;
        public string DniS
        {
            get
            {
                return _dniS;
            }
            set
            {
                _dniS = value;
                OnPropertyChanged();
            }
        }

        private string _nameS;
        public string NameS
        {
            get
            {
                return _nameS;
            }
            set
            {
                _nameS = value;
                OnPropertyChanged();
            }
        }

        private string _subjectName;
        public string SubjectName
        {
            get
            {

                return _subjectName;
            }
            set
            {
                _subjectName = value;
                OnPropertyChanged();
            }
        }



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
                OnPropertyChanged();
            }
        }

        private StudentSubject _currentStudentSubject;
        public StudentSubject CurrentStudentSubject
        {
            get
            {
                return _currentStudentSubject;
            }
            set
            {
                _currentStudentSubject = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsList;
        public List<Subject> SubjectList
        {
            get
            {
                return _subjectsList;
            }
            set
            {
                _subjectsList = value;
                OnPropertyChanged();
            }
        }


        List<Subject> _subjectsByNameList;
        public List<Subject> SubjectsByNameList
        {
            get
            {
                return _subjectsByNameList;
            }
            set
            {
                _subjectsByNameList = value;

                OnPropertyChanged();
            }
        }



        List<StudentSubject> _subjectsByStudentList;
        public List<StudentSubject> SubjectsByStudentList
        {
            get
            {
                return _subjectsByStudentList;
            }
            set
            {
                _subjectsByStudentList = value;

                OnPropertyChanged();
            }
        }

        public StudentSubjectsViewModel()
        {
            AddSubjectToListVMCommand = new RouteCommand(AddSubjectToListVM);
            FindStudentCommand = new RouteCommand(FindStudent);
            GetSubjectsMGVMCommand = new RouteCommand(GetSubjectsMGVM);
            DelSubjectToListVMCommand = new RouteCommand(DelSubjectToListVM);
            GetSubjectsToStudentVMCommand = new RouteCommand(GetSubjectsToStudent);
        }

        private void GetSubjectsToStudent()
        {
            Student student = new Student();
            StudentSubject studentSubjectMVM = new StudentSubject();

            if (studentSubjectMVM != null)
            {
                student = CurrentStudent;
                studentSubjectMVM.StudentId = student.Id;

                SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);
            }
        }

        private void DelSubjectToListVM()
        {
            StudentSubject studentSubjectMVM = new StudentSubject();

            if (CurrentStudentSubject == null)
            {

                MessageBox.Show("Se tiene que seleccionar una asignatura del estudiante antes de eliminarla");
            }
            else
            {
                MessageBox.Show("La assignatura del estudiante se ha eliminado correctamente");

                studentSubjectMVM = CurrentStudentSubject;
                studentSubjectMVM.Delete();

                SubjectsByStudentList = studentSubjectMVM.StudentBySubjects(studentSubjectMVM.StudentId);


            }

        }

        private void GetSubjectsMGVM()
        {
            var subject = new Subject();

            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();

            SubjectList = repo.QueryAll().ToList();
        }

        private void FindStudent()
        {
            var studentsVM = new StudentsViewModel();
            StudentSubject studentSubjectMVM = new StudentSubject();
            studentsVM.GetStudents();

            CurrentStudent = studentsVM.Students.FirstOrDefault(x => x.Dni == DniS);

            if (CurrentStudent != null)
            {
                DniS = CurrentStudent.Dni;
                NameS = CurrentStudent.Name;

                GetSubjectsToStudent();
            }
            else
            {
                MessageBox.Show("Este estudiante no existe");

                DniS = "";

            }
        }

        private void AddSubjectToListVM()
        {
            Subject subject = new Subject();

            Student student = new Student();

            StudentSubject studentSubjectMVM = new StudentSubject();

            subject = CurrentSubject;

            student = CurrentStudent;


            if (CurrentStudent != null)
            {
                studentSubjectMVM.StudentId = student.Id;

                if (CurrentSubject != null)
                {
                    studentSubjectMVM.SubjectId = subject.Id;
                }
                GetSubjectsToStudent();
            }
            else
            {
                MessageBox.Show("No existe este estudiante");

            }

            studentSubjectMVM.Save();
        }

        public ICommand AddSubjectToListVMCommand { get; set; }
        public ICommand FindStudentCommand { get; set; }
        public ICommand GetSubjectsMGVMCommand { get; set; }
        public ICommand DelSubjectToListVMCommand { get; set; }
        public ICommand GetSubjectsToStudentVMCommand { get; set; }

    }
}
