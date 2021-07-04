namespace EmploymentLibrary
{
    public abstract class DTOBaseClass : IEmploymentDTO
    {
        private int responseID;

        private int GetresponseID()
        {
            return responseID;
        }

        private void SetresponseID(int value)
        {
            responseID = value;
        }

        public int ResponseID { get; set; }
        public string PositionTitle { get; set; }
        public string DetailURL { get; set; }
        public string CompanyURL { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string ClosingDate { get; set; }
        public string Summary { get; set; }

        public DTOBaseClass()
        {
            ResponseID = GetresponseID();
        }

        protected DTOBaseClass(string positionTitle, string detailURL, string companyURL, string company, string location, string closingDate, string summary)
        {
            ResponseID = GetresponseID();
            PositionTitle = positionTitle;
            DetailURL = detailURL;
            CompanyURL = companyURL;
            Company = company;
            Location = location;
            ClosingDate = closingDate;
            Summary = summary;
            
            IncrementResponseID();
        }

        private int IncrementResponseID()
        {
            SetresponseID(GetresponseID() + 1);
            return GetresponseID();
        }
    }
}
