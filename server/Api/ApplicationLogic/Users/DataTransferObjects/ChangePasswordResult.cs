using System.Collections.Generic;

namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class ChangePasswordResult
    {
        public bool Succeeded { get; set; }
        public Dictionary<string, string> Errors { get; set; }


        public ChangePasswordResult(bool succeeded): this(succeeded, new Dictionary<string, string>())
        {
            
        }

        public ChangePasswordResult(bool succeeded, Dictionary<string, string> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
        
    }
}