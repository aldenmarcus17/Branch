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

namespace Branch.ViewModels
{
    internal class MentorViewModel : BaseViewModel
    {
        //Collections and Commands
        public ObservableRangeCollection<Mentor> Mentor { get; set; }
        public ObservableRangeCollection<Grouping<string, Mentor>> MentorGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public ICommand LoadMoreCommand { get; }
        public Mentor selectedMentor;

        //Variables/Properties
        private bool _visible;
        private bool _tutorFilter = true;
        private bool _financeFilter = true;
        private bool _guidanceFilter = true;
        private bool _skillFilter = true;
        int numOfMentors = 0;


        //Actual ViewModel Stuff (Tasks and Bindings)
        async void LoadMore()
        {
            numOfMentors = Mentor.Count;

            Mentor.Clear();
            MentorGroups.Clear();

            //var mentors = await MentorService.GetMentor(); 
            //Mentor.AddRange(mentors);

            if (numOfMentors == Mentor.Count)
            {
                _visible = false;
                return;
            }

            //Adding Example People
            Mentor.Add(new Mentor { Name = "Joe", Specialty = "Computer Science", Description = "I love to teach CS!", Email = "joe@gmail.com", Tag = "Tutor" });


            _visible = true;

            //List<String> tags = await MentorService.GetTag();

                //MentorGroups.Add(new Grouping<string, Mentor>("Tutor", Mentor.Where(s => s.Tag == "Tutor")));

                //MentorGroups.Add(new Grouping<string, Mentor>("Finance", Mentor.Where(s => s.Tag == "Finance")));

                //MentorGroups.Add(new Grouping<string, Mentor>("Guidance", Mentor.Where(s => s.Tag == "Guidance")));

                //MentorGroups.Add(new Grouping<string, Mentor>("Skill", Mentor.Where(s => s.Tag == "Skill")));
        }
        public bool Visible
        {
            get
            { return _visible; }
            set { }
        }
        public bool NoMentors
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


        public MentorViewModel()
        {
            Mentor = new ObservableRangeCollection<Mentor>();
            MentorGroups = new ObservableRangeCollection<Grouping<string, Mentor>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }
        public Mentor SelectedMentor
        {
            get => selectedMentor;
            set => SetProperty(ref selectedMentor, value);
        }


        async Task Selected(object args)
        {
            var mentor = args as Mentor;
            if (mentor == null)
                return;

            SelectedMentor = null;

            await Application.Current.MainPage.DisplayAlert("Selected", mentor.Name, "OK");
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Mentor.Clear();

            var mentors = await MentorService.GetMentor();

            Mentor.AddRange(mentors);
            

            IsBusy = false;
        }


    }
}
