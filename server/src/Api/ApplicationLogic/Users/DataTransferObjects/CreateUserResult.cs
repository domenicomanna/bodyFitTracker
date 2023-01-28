using System.Collections.Generic;

namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class CreateUserResult
    {
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public bool Succeeded { get; set; }
        public string Token { get; set; } = "";
    }
}
