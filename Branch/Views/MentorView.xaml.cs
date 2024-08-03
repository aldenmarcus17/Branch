using Branch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Branch.ViewModels;

namespace Branch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MentorView : ContentPage
    {
        public MentorView()
        {
            InitializeComponent();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}