namespace Export.JSON
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using WeddingPlanner.Data;
    using WeddingPlanner.Models.Enums;

    public class Startup
    {
        public static void Main()
        {
            //ExportOrderedAgencies();
            ExportGuestList();
        }

        private static void ExportGuestList()
        {
            using (WeddingContext context = new WeddingContext())
            {
                var guestList = context.Weddings
                    .Select(w => new
                    {
                        bride = w.Bride.FirstName + " " + w.Bride.MiddleNameInitial + " " + w.Bride.LastName,
                        bridegroom = w.Bridegroom.FirstName + " " + w.Bridegroom.MiddleNameInitial + " " + w.Bridegroom.LastName,
                        agency = new
                        {
                            name = w.Agency.Name,
                            town = w.Agency.Town
                        },
                        invitedGuests = w.Invitations.Count(),
                        brideGuests = w.Invitations.Count(i => i.Family == Family.Bride),
                        bridegroomGuests = w.Invitations.Count(i => i.Family == Family.Bridegroom),
                        attendingGuests = w.Invitations.Count(i => i.IsAtteding == true),
                        guests = w.Invitations.Where(i => i.IsAtteding == true).Select(i => i.Guest.FirstName + " " + i.Guest.MiddleNameInitial + " " + i.Guest.LastName)

                    })
                    .OrderByDescending(w => w.invitedGuests)
                    .ThenBy(w => w.attendingGuests);

                var json = JsonConvert.SerializeObject(guestList, Formatting.Indented);

                File.WriteAllText("../../../Export/guests.json", json);
            }
        }

        private static void ExportOrderedAgencies()
        {
            using (WeddingContext context = new WeddingContext())
            {
                var agencies = context.Agencies
                    .OrderByDescending(a => a.EmployeesCount)
                    .ThenBy(a => a.Name)
                    .Select(a => new
                    {
                        name = a.Name,
                        count = a.EmployeesCount,
                        town = a.Town
                    });

                var json = JsonConvert.SerializeObject(agencies, Formatting.Indented);

                File.WriteAllText("../../../Export/agencies-ordered.json", json);
            }
        }
    }
}
