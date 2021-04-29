namespace EmploymentLibrary
{
    // URL "Static" Globals
    public static class ExternalServiceEndPoints
    {
        public static class UTSServices
        {
            public static readonly string QUERY_URL = "https://careerhub.uts.edu.au/students/jobs/Search";
            public static readonly string REFERER = "https://careerhub.uts.edu.au/";
            public static readonly string HOST = "careerhub.uts.edu.au";

            public static readonly string SAML_REQUEST_REDIRECT = "https://careerhub.uts.edu.au/identity/saml/SsoPostRedirect.aspx?id=0";
            public static readonly string SAML_FORM_POST = "https://aaf-login.uts.edu.au/idp/profile/SAML2/POST/SSO";
            public static readonly string STUDENTS_SIGN_ON_AUTH_URL = "https://aaf-login.uts.edu.au/idp/profile/SAML2/POST/SSO?execution=e1s1";
            public static readonly string SSO_SAML_RESPONSE_POST = "https://careerhub.uts.edu.au/providers/saml/sso";
            public static readonly string SSO_SAML_POST_HOST = "aaf-login.uts.edu.au";
        }
        
        public static class SeekServices
        {
            public static readonly string QUERY_URL = "https://www.seek.com.au/api/chalice-search/search";
            public static readonly string REFERER = string.Empty;
            public static readonly string HOST = string.Empty;
        }
    }
}
