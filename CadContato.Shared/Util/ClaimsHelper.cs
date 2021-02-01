using System.Security.Claims;

namespace CadContato.Shared.Util
{
    public class ClaimsHelper
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Picture { get; private set; }

        public ClaimsHelper()
        {

        }

        public ClaimsHelper(ClaimsPrincipal claims)
        {
            PopulateFromPrincipal(claims);
        }

        public void PopulateFromPrincipal(ClaimsPrincipal claims)
        {
            foreach (var claim in claims.Claims)
            {
                if (claim.Type.EndsWith("emailaddress"))
                    Email = claim.Value;

                if (claim.Type.EndsWith("/name"))
                    Name = claim.Value;

                if (claim.Type.EndsWith("picture"))
                    Picture = claim.Value;
            }
        }

    }
}
