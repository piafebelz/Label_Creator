using System.Collections.Generic;
using OplogIdentity.Admin.Configuration.Identity;

namespace OplogIdentity.Admin.Configuration.IdentityServer
{
    public class Client : global::IdentityServer4.Models.Client
    {
        public List<Claim> ClientClaims { get; set; } = new List<Claim>();
    }
}






