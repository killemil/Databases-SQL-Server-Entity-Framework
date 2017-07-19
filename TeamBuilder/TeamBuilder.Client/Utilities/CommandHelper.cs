namespace TeamBuilder.Client.Utilities
{
    using Models;
    using System.Linq;
    using TeamBuilder.Data;

    public static class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Username == username && u.IsDeleted == false);
            }
        }

        public static bool IsEventHasTeam(string eventName, string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName && e.Teams.Any(t => t.Name == teamName));
            }
        }

        public static bool IsInviteExisting (string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context
                    .Invitations
                    .Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive == true);
            }
        }

        public static User GetCreatorOfTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Teams.FirstOrDefault(t => t.Name == teamName).Creator;

                return user;
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName , User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName && t.CreatorId == user.Id);
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName && e.CreatorId == user.Id);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName && t.Members.Any(m => m.Username == username));
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }
    }
}
