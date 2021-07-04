namespace EmploymentLibrary
{
    public interface IEmploymentDTO : IResponseDTO
    {
        public string PositionTitle { get; set; }
        public string DetailURL { get; set; }
        public string CompanyURL { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string ClosingDate { get; set; }
        public string Summary { get; set; }
    }
}
