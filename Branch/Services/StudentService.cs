using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Branch.Models;
using System.Dynamic;
using System.Linq;

namespace Branch.Services
{
    //Not Working Yet But In Place For Database System
    internal class StudentService
    {
        static SQLiteAsyncConnection dbStudent;

        static async Task Init()
        {
            if (dbStudent != null)
                return;

            //create database and tables
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Student.db");

            dbStudent = new SQLiteAsyncConnection(databasePath);

            await dbStudent.CreateTableAsync<Student>();
        }

        public static async Task AddStudent(string name, string specialty, string description, string email, string tag)
        {
            await Init();
            var student = new Student
            {
                Name = name,
                Description = description,
                Specialty = specialty,
                Email = email,
                Tag = tag
            };

            var id = await dbStudent.InsertAsync(student); //reminder!! returns integer of ID location
        }

        public static async Task RemoveStudent(int id)
        {
            await Init();

            await dbStudent.DeleteAsync<Student>(id);
        }

        public static async Task<IEnumerable<Student>> GetStudent()
        {
            await Init();

            var student = await dbStudent.Table<Student>().ToListAsync();
            return student;
        }

        public static async Task<List<string>> GetTag()
        {
            await Init();

            List<Student> student = await dbStudent.Table<Student>().ToListAsync();
            List<String> tags = student.Select(z => z.Tag).ToList();

            return tags;
        }
    }
}

