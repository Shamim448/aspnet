﻿// See https://aka.ms/new-console-template for more information
string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
Course course = new Course();
course = new Course { Title = "Demo Course", Fees = 5000, IsActive = false, StartDate = new DateTime(2023, 04,25) };
var orm = new MyORM< int, Course>(DbConnection);
//orm.Insert(course);
orm.GetAll();
