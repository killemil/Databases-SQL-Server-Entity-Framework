namespace AutoMap.Client
{
    using AutoMap.Data;
    using AutoMap.Models;
    using AutoMap.Models.Dtos;
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    class Startup
    {
        static void Main()
        {


            // Excercise 01 --------------------
            //    ConfigureAutomapping();

            //    Employee employee = new Employee()
            //    {
            //        FirstName = "Emba",
            //        LastName = "Street",
            //        Salary = 200m,
            //        Address = "Tintqva 123",
            //        DataOfBirth = DateTime.Now
            //    };

            //    EmployeeDto dto = Mapper.Map<EmployeeDto>(employee);

            //    Console.WriteLine($"{dto.FirstName} {dto.LastName} {dto.Salary}");


            // Excercise 02 -----------------------

            //ConfigureMapper();
            //List<Employee> employees = CreateEmployees();

            //List<ManagerDto> managersDtos = Mapper.Map<List<Employee>, List<ManagerDto>>(employees);
            //foreach (ManagerDto managerDto in managersDtos)
            //{
            //    Console.WriteLine(managerDto.ToString());
            //}


            // Excercise 03 --------------------------

            InitializeDataBase();
            //SeedDatabase(employees);

            ConfigureMapper();
            using (AutoMapContext dbContext = new AutoMapContext())
            {
                var employees = dbContext.Employees
                    .Where(emp => emp.DataOfBirth.Year < 2018)
                    .OrderByDescending(emp => emp.Salary)
                    .ProjectTo<EmployeeDto>();

                foreach (EmployeeDto employee in employees)
                {
                    Console.WriteLine(employee.ToString());
                }

            }

        }

        //Excercise 03 ---------------------------
        private static void ConfigureMapper()
        {
            Mapper.Initialize(act =>
            {
                act.CreateMap<Employee, EmployeeDto>()
                .ForMember(emp => emp.ManagerLastName, config => config.MapFrom(e => e.Manager.LastName));
                act.CreateMap<Employee, ManagerDto>()
                .ForMember(dto => dto.SubordinatesCount, configure => configure.MapFrom(e => e.Subordinates.Count));
            });
        }

        private static void SeedDatabase(List<Employee> employees)
        {
            using (AutoMapContext dbContext = new AutoMapContext())
            {
                dbContext.Employees.AddRange(employees);
                dbContext.SaveChanges();
            }
        }

        private static void InitializeDataBase()
        {
            using (AutoMapContext context = new AutoMapContext())
            {
                context.Database.Initialize(true);
            }
        }


        //Excercise 02 ------------------------

        //private static void ConfigureMapper()
        //{
        //    Mapper.Initialize(act =>
        //    {
        //        act.CreateMap<Employee, EmployeeDto>();
        //        act.CreateMap<Employee, ManagerDto>()
        //            .ForMember(dto => dto.SubordinatesCount, config => config.MapFrom(e => e.Subordinates.Count));
        //    });
        //}

        private static List<Employee> CreateEmployees()
        {
            var managers = new List<Employee>();

            var manager1 = new Employee()
            {
                FirstName = "georgi",
                LastName = "Trendafilov",
                Address = "Nqma takav adress.com",
                Salary = 300m,
                DataOfBirth = DateTime.Now,
                IsOnHoliday = true
            };
            var manager2 = new Employee()
            {
                FirstName = "Bai",
                LastName = "Ganyo",
                Address = "Bulgaria BG",
                Salary = 350m,
                DataOfBirth = DateTime.Now,
                IsOnHoliday = true
            };

            var employeer1 = new Employee()
            {
                FirstName = "Stoio",
                LastName = "Stoev",
                Salary = 120m,
                IsOnHoliday = false,
                DataOfBirth = DateTime.Now,
                Address = "No address"

            };

            var employeer2 = new Employee()
            {
                FirstName = "Traicho",
                LastName = "Traikov",
                Salary = 120m,
                IsOnHoliday = false,
                DataOfBirth = DateTime.Now,
                Address = "Taralqnkovo 10"
            };

            var employeer3 = new Employee()
            {
                FirstName = "Peyo",
                LastName = "Peev",
                Salary = 130m,
                IsOnHoliday = false,
                DataOfBirth = DateTime.Now,
                Address = "XaHko bpaT 12"
            };

            manager1.Subordinates.Add(employeer1);
            manager1.Subordinates.Add(employeer2);
            manager1.Subordinates.Add(employeer3);
            manager2.Subordinates.Add(manager1);

            managers.Add(manager1);
            managers.Add(manager2);

            return managers;
        }

        //Excercise 01 -------------------------

        //private static void ConfigureAutomapping()
        //{
        //    Mapper.Initialize(act =>
        //    {
        //        act.CreateMap<Employee, EmployeeDto>();
        //    });
        //}

    }

}

