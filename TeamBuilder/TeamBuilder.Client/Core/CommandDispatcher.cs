
namespace TeamBuilder.Client.Core
{
    using Commands;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "ExportTeam":
                    ExportTeamCommand exportTeam = new ExportTeamCommand();
                    result = exportTeam.Execute(inputArgs);
                    break;
                case "ImportTeams":
                    ImportTeamsCommand importTeams = new ImportTeamsCommand();
                    result = importTeams.Execute(inputArgs);
                    break;
                case "ImportUsers":
                    ImportUsersCommand importUsers = new ImportUsersCommand();
                    result = importUsers.Execute(inputArgs);
                    break;
                case "ShowTeam":
                    ShowTeamCommand showTeam = new ShowTeamCommand();
                    result = showTeam.Execute(inputArgs);
                    break;
                case "ShowEvent":
                    ShowEventCommand showEvent = new ShowEventCommand();
                    result = showEvent.Execute(inputArgs);
                    break;
                case "AddTeamTo":
                    AddTeamToCommand addTeam = new AddTeamToCommand();
                    result = addTeam.Execute(inputArgs);
                    break;
                case "Disband":
                    DisbandCommand disband = new DisbandCommand();
                    result = disband.Execute(inputArgs);
                    break;
                case "KickMember":
                    KickMemberCommand kickMember = new KickMemberCommand();
                    result = kickMember.Execute(inputArgs);
                    break;
                case "DeclineInvite":
                    DeclineInviteCommand declineInvite = new DeclineInviteCommand();
                    result = declineInvite.Execute(inputArgs);
                    break;
                case "AcceptInvite":
                    AcceptInviteCommand acceptInvite = new AcceptInviteCommand();
                    result = acceptInvite.Execute(inputArgs);
                    break;
                case "InviteToTeam":
                    InviteToTeamCommand inviteTo = new InviteToTeamCommand();
                    result = inviteTo.Execute(inputArgs);
                    break;
                case "CreateTeam":
                    CreateTeamCommand createTeam = new CreateTeamCommand();
                    result = createTeam.Execute(inputArgs);
                    break;
                case "CreateEvent":
                    CreateEventCommand createEvent = new CreateEventCommand();
                    result = createEvent.Execute(inputArgs);
                    break;
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand();
                    result = deleteUser.Execute(inputArgs);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(inputArgs);
                    break;
                case "Login":
                    LoginCommand login = new LoginCommand();
                    result = login.Execute(inputArgs);
                    break;
                case "RegisterUser":
                    RegisterUserCommand register = new RegisterUserCommand();
                    result = register.Execute(inputArgs);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute(inputArgs);
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }

            return result;
        }
    }
}
