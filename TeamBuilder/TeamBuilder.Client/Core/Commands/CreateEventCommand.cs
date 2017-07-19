using System;
using System.Globalization;
using TeamBuilder.Client.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Core.Commands
{
    public class CreateEventCommand
    {
        public string Execute(string[] input)
        {
            //< name > < description > < startDate > < endDate >

            Check.CheckLength(6, input);

            if (!AuthenticatedManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            string eventName = input[0];

            if (eventName.Length > 25)
            {
                throw new InvalidOperationException("Eventname should be less than 25 symbols!");
            }

            string eventDescripton = input[1];

            if (eventDescripton.Length > 250)
            {
                throw new InvalidOperationException("Description should be less then 250 symbols!");
            }

            DateTime startDate;
            DateTime endDate;
            string dateFormat = "dd/MM/yyyy HH:mm";
            bool IsStartDateValid = DateTime.TryParseExact(input[2] + " " + input[3], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            bool IsEndDateValid = DateTime.TryParseExact(input[4] + " " + input[5], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            if (!IsStartDateValid || !IsEndDateValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            User user = AuthenticatedManager.GetCurrentUser();

            this.CreateEvent(eventName, eventDescripton, startDate, endDate, user);


            return $"Event {eventName} was created successfully!";
        }

        private void CreateEvent(string eventName , string description, DateTime startDate, DateTime endDate, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event newEvent = new Event()
                {
                    Name = eventName,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatorId = user.Id
                };

                context.Events.Add(newEvent);
                context.SaveChanges();
            }
        }
    }
}
