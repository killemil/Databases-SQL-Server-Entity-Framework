using System;
using System.Collections.Generic;
using System.Linq;
using TeamBuilder.Client.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Core.Commands
{
    public class ShowEventCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);

            string eventName = input[0];

            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }


            return PrintEvent(eventName);
        }

        private string PrintEvent(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event currenEvent = context.Events
                    .OrderByDescending(e=> e.StartDate)
                    .FirstOrDefault(e => e.Name == eventName);
                var teams = context.Teams
                    .Where(t => t.Events.Any(e => e.Name == eventName))
                    .Select(t => new
                    {
                        t.Name
                    });

                return $@"Event Name:{currenEvent.Name} 
                          StartDate:{currenEvent.StartDate} 
                          EndDate:{currenEvent.EndDate} 
                          Description:{currenEvent.Description}
                          Teams:
{string.Join("\n", teams)}";
            }
        }
    }
}
