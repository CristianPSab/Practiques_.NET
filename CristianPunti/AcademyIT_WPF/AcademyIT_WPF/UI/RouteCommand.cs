using Academy.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Academy.UI
{
    public class RouteCommand : ICommand
    {
        #region Constructors

        public RouteCommand(Action execute)
            : this(execute, null) { }

        public RouteCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RouteCommand(List<Student> students)
        {
            this.students = students;
        }

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute();
       }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion

        private readonly Action _execute = null;
        private readonly Predicate<object> _canExecute = null;
        private List<Student> students;
    }
}
