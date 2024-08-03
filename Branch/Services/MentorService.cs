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
    internal class MentorService
    {
        static SQLiteAsyncConnection dbMentor;

        static async Task Init()
        {
            if (dbMentor != null)
                return;

            //create database and tables
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Mentor.db");

            dbMentor = new SQLiteAsyncConnection(databasePath);

            await dbMentor.CreateTableAsync<Mentor>();
        }

        public static async Task AddMentor(string name, string specialty, string description, string email, string tag)
        {
            await Init();
            var mentor = new Mentor
            {
                Name = name,
                Description = description,
                Specialty = specialty,
                Email = email,
                Tag = tag
            };

            var id = await dbMentor.InsertAsync(mentor); //reminder!! returns integer of ID location
        }

        public static async Task RemoveMentor(int id)
        {
            await Init();

            await dbMentor.DeleteAsync<Mentor>(id);
        }

        public static async Task<IEnumerable<Mentor>> GetMentor()
        {
            await Init();

            var mentor = await dbMentor.Table<Mentor>().ToListAsync();
            return mentor;
        }

        public static async Task<List<string>> GetTag()
        {
            await Init();

            List<Mentor> mentor = await dbMentor.Table<Mentor>().ToListAsync();
            List<String> tags = mentor.Select(z => z.Tag).ToList();

            return tags;
        }
    }
}
