using Academy.App.WPF.ViewsModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Academy.App.WPF.Views
{
    /// <summary>
    /// Lógica de interacción para StudentSubjectsView.xaml
    /// </summary>
    public partial class StudentSubjectsView : UserControl
    {
        public StudentSubjectsView()
        {
            InitializeComponent();

            var vm = new StudentSubjectsViewModel();
            this.DataContext = vm;
        }
    }
}
