﻿using Library.Interface;
using System.IO;

namespace Library.Class
{
    public class Build : IRepository
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Status { get; set; }

        public Build(int number, string name, string description)
        {
            Number = number;
            Name = name;
            Description = description;
            Status = "Building";
        }

        public void CreateRepository(string path)
        {
            string dir = path + "\\" + Number.ToString();
            Directory.CreateDirectory(dir);
            File.Create(dir+"\\console.log").Close();
        }
    }
}
