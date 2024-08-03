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
using System.Collections.ObjectModel;

namespace Branch.ViewModels
{
    internal class StudentViewModel : BaseViewModel
    {

        static StudentService StudentService { get; set; }
        public ObservableRangeCollection<Student> Student { get; set; }
        public ObservableRangeCollection<Grouping<string, Student>> StudentGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public ICommand LoadMoreCommand { get; }
        public Student selectedStudent;
        private bool _visible;
        private bool _tutorFilter = true;
        private bool _financeFilter = true;
        private bool _guidanceFilter = true;
        private bool _skillFilter = true;
        int numOfStudents = 0;

        async void LoadMore()
        {
           
            numOfStudents = Student.Count;

            Student.Clear();
            StudentGroups.Clear();

            IsBusy = true;
            await Refresh();

            List<String> tags = await StudentService.GetTag();

            await Task.Delay(2000);

            /* Not Working Yet
            if (tags.Contains("Tutor") && _tutorFilter)
             StudentGroups.Add(new Grouping<string, Student>("Tutor", Student.Where(s => s.Tag == "Tutor"))); 

            if (tags.Contains("Finance") && _financeFilter)
                StudentGroups.Add(new Grouping<string, Student>("Finance", Student.Where(s => s.Tag == "Finance")));

            if (tags.Contains("Guidance") && _guidanceFilter)
                StudentGroups.Add(new Grouping<string, Student>("Guidance", Student.Where(s => s.Tag == "Guidance")));

            if (tags.Contains("Skill") && _skillFilter)
                StudentGroups.Add(new Grouping<string, Student>("Skill", Student.Where(s => s.Tag == "Skill")));


            if (numOfStudents == Student.Count)
            {
                _visible = false;
                return;
            }

            _visible = true;
            */
            IsBusy = false;
        }
        public bool Visible
        {
            get
            { return _visible; }
            set { }
        }
        public bool NoStudent
        {
            get
            { return !(_visible); }
            set { }
        }
        public bool TutorFilter
        {
            get { return _tutorFilter; }
            set { SetProperty(ref _tutorFilter, value); }
        }
        public bool FinanceFilter
        {
            get { return _financeFilter; }
            set { SetProperty(ref _financeFilter, value); }
        }
        public bool GuidanceFilter
        {
            get { return _guidanceFilter; }
            set { SetProperty(ref _guidanceFilter, value); }
        }
        public bool SkillFilter
        {
            get { return _skillFilter; }
            set { SetProperty(ref _skillFilter, value); }
        }
        public StudentViewModel()
        {
            Student = new ObservableRangeCollection<Student>();
            StudentGroups = new ObservableRangeCollection<Grouping<string, Student>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }
        public Student SelectedStudent
        {
            get => selectedStudent;
            set => SetProperty(ref selectedStudent, value);
        }
        async Task Selected(object args)
        {
            var student = args as Student;
            if (student == null)
                return;

            SelectedStudent = null;

            await Application.Current.MainPage.DisplayAlert("Selected", student.Name, "OK");
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Student.Clear();

            var students = await StudentService.GetStudent();

            IsBusy = false;
        }


    }
}
