using Branch.ViewModels;
using Branch.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Branch
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MentorView), typeof(MentorView));
            Routing.RegisterRoute(nameof(StudentView), typeof(StudentView));
            Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
        }

    }
}
