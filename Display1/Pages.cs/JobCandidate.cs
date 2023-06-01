using Display1.Models;

namespace Display1.Pages.cs
{
    public class JobCandidate
    {
        public int JobCandidateId { get; set; }
        public int BusinessEntityId { get; set; }
        public string Resume { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Employee BusinessEntity { get; set; }

        public JobCandidate(int jobCandidateId, int businessEntityId, string resume, DateTime modifiedDate, Employee businessEntity)
        {
            JobCandidateId = jobCandidateId;
            BusinessEntityId = businessEntityId;
            Resume = resume;
            ModifiedDate = modifiedDate;
            BusinessEntity = businessEntity;
        }
    }
}
