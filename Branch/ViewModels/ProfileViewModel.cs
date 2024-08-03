using MvvmHelpers;
using MvvmHelpers.Commands;
using Branch.Models;
using Branch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;

namespace Branch.ViewModels
{
    internal class ProfileViewModel : BaseViewModel
    {
        //Variables
        private string _name;
        private string _description;
        private string _topic;
        private string _email;
        private string _tag;

        public string selectedTag;
        private bool _isStudent;

        //Commands
        public AsyncCommand<object> SaveCommand { get; }

        //ViewModel
        public ProfileViewModel()
        {
            SaveCommand = new AsyncCommand<object>(SavePerson);
        }

        //Methods
        public bool IsStudent
        {
            get { return _isStudent; }
            set { SetProperty(ref _isStudent, value); }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value) _name = value;
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value) _description = value;
            }
        }
        public string Topic
        {
            get => _topic;
            set
            {
                if (_topic != value) _topic = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value) _email = value;
            }
        }
        public string Tag
        {
            get => _tag;
            set
            {
                if (_tag != value) _tag = value;
            }
        }

        public string SelectedTag
        {
            get => selectedTag;
            set => SetProperty(ref selectedTag, value);
        }
        async Task SavePerson(object args)
        {
            if (IsStudent) 
            { 
                Student student = args as Student;
                await StudentService.AddStudent(_name, _topic, _description, _email, _tag);
                await Application.Current.MainPage.DisplayAlert("Completed Student Profile", "Your profile has been added!", "OK");

            }
            else 
            { 
                Mentor mentor = args as Mentor;
                await MentorService.AddMentor(_name, _topic, _description, _email, _tag);
                await Application.Current.MainPage.DisplayAlert("Completed Mentor Profile", "Your profile has been added!", "OK");
            }
            
        }
    }
}
