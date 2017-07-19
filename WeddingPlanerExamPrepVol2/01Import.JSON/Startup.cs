namespace _01Import.JSON
{
    using _01Import.JSON.DTO;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WeddingPlanner.Data;
    using WeddingPlanner.Models;
    using WeddingPlanner.Models.Enums;
    using WeddingPlanner.Utilities;
    public class Startup
    {
        public static void Main()
        {
            //ImportAgencies();
            //ImportPeople();
            //ImportWeddingAndInvitations();
        }

        private static void ImportWeddingAndInvitations()
        {
            string json = File.ReadAllText(Constants.WeddingsPath);
            List<WeddingDto> weddings = JsonConvert.DeserializeObject<List<WeddingDto>>(json);

            using (WeddingContext context = new WeddingContext())
            {
                foreach (var w in weddings)
                {
                    if (w.Bride == null || w.BrideGroom == null ||
                        w.Agency == null || w.Date == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Person bride = context.People.FirstOrDefault(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.Bride);
                    Person bridegroom = context.People.FirstOrDefault(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.BrideGroom);
                    Agency agency = context.Agencies.FirstOrDefault(a => a.Name == w.Agency);

                    if (bride == null || bridegroom == null || agency == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Wedding wedding = new Wedding
                    {
                        Bride = bride,
                        Bridegroom = bridegroom,
                        Agency = agency,
                        Date = w.Date
                    };

                    foreach (var g in w.Guests)
                    {
                        Person guest = context.People.FirstOrDefault(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == g.Name);
                        if (guest == null)
                        {
                            continue;
                        }

                        Invitation invitation = new Invitation
                        {
                            Guest = guest,
                            Family = g.Family,
                            IsAtteding = g.RSVP
                        };
                        wedding.Invitations.Add(invitation);
                    }

                    try
                    {
                        context.Weddings.Add(wedding);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported wedding of {bride.FirstName} and {bridegroom.FirstName}");
                    }
                    catch (Exception)
                    {
                        context.Weddings.Remove(wedding);
                        Console.WriteLine(Messages.ErrorMessage);
                    }

                }

            }
        }

        private static void ImportPeople()
        {
            string json = File.ReadAllText(Constants.PeoplePath);
            List<PersonDto> people = JsonConvert.DeserializeObject<List<PersonDto>>(json);

            using (WeddingContext context = new WeddingContext())
            {
                foreach (var p in people)
                {
                    if (p.FirstName == null || p.LastName == null ||
                        p.MiddleInitial == null || p.MiddleInitial.Length != 1)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Gender gender;
                    bool IsGenderValid = Enum.TryParse(p.Gender.ToString(), true, out gender);
                    if (!IsGenderValid)
                    {
                        gender = Gender.NotSpecified;
                    }

                    Person person = new Person
                    {
                        FirstName = p.FirstName,
                        MiddleNameInitial = p.MiddleInitial,
                        LastName = p.LastName,
                        BirthDate = p.Birthday,
                        Gender = gender,
                        Email = p.Email,
                        Phone = p.Phone
                    };

                    try
                    {
                        context.People.Add(person);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported {person.FullName}");
                    }
                    catch (Exception)
                    {
                        context.People.Remove(person);
                        Console.WriteLine(Messages.ErrorMessage);
                    }
                }
            }
        }

        private static void ImportAgencies()
        {
            string json = File.ReadAllText(Constants.AgenciesPath);

            List<AgencyDTO> agencies = JsonConvert.DeserializeObject<List<AgencyDTO>>(json);
            using (WeddingContext context = new WeddingContext())
            {
                foreach (var a in agencies)
                {
                    if (a.Name == null || a.Town == null || a.EmployeesCount == 0)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Agency agency = new Agency
                    {
                        Name = a.Name,
                        EmployeesCount = a.EmployeesCount,
                        Town = a.Town
                    };

                    context.Agencies.Add(agency);
                    context.SaveChanges();

                    Console.WriteLine($"Successfully imported {a.Name}");
                }
            }
        }
    }
}
