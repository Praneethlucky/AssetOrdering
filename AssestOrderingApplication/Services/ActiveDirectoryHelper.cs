namespace AssestOrderingApplication.Services
{
    using System;
    using System.DirectoryServices;

    public class ActiveDirectoryHelper
    {
        public static string GetManagerName(string username)
        {
            // Define the LDAP path (replace with your domain)
            string ldapPath = "LDAP://DC=INSIGHTSOFTWARE.LAN";

            try
            {
                // Create a DirectoryEntry object for the directory
                DirectoryEntry entry = new DirectoryEntry(ldapPath);

                // Create a DirectorySearcher to search for the user
                DirectorySearcher searcher = new DirectorySearcher(entry)
                {
                    Filter = $"(sAMAccountName={username})"
                };

                // Specify which properties to load (in this case, we want the manager)
                searcher.PropertiesToLoad.Add("manager");

                // Perform the search
                SearchResult result = searcher.FindOne();

                if (result != null && result.Properties.Contains("manager"))
                {
                    // Get the Distinguished Name (DN) of the manager
                    string managerDn = result.Properties["manager"][0].ToString();

                    // Optionally, you can query Active Directory again to get the manager's details
                    // Here, we extract just the manager's common name (CN) from the DN
                    string managerName = managerDn.Split(',')[0].Replace("CN=", "");
                    return managerName;
                }

                return "Manager not found";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                return $"Error: {ex.Message}";
            }
        }
    }

}
