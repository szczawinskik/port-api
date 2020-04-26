using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Validation.Interfaces;
using Web.Validation.Messages;
using Web.ViewModels;

namespace Web.Validation.Validators
{
    public class RemoteServiceAddressValidator : IValidator<RemoteServiceAddressViewModel>
    {
        public List<string> ErrorList { get; set; }

        public bool IsValid(RemoteServiceAddressViewModel item)
        {
            ErrorList = new List<string>();
            if (!Uri.IsWellFormedUriString(item.Value, UriKind.Absolute))
            {
                ErrorList.Add(RemoteServiceIpAddressViewModelMessages.InvalidUri);
            }
            return !ErrorList.Any();
        }
    }
}
