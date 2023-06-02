using Display1.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Display1.Service
{
    public class JobCandidateService
    {
        private readonly AdventureWorks2019Context _context;

        public JobCandidateService(AdventureWorks2019Context context)
        {
            _context = context;
        }

        public async Task<List<JobCandidate>> GetJobCandidateAsync()
        {
            return await _context.JobCandidate.ToListAsync();
        }

        public async Task CreateJobCandidateAsync(JobCandidate jobCandidate)
        {
            _context.JobCandidate.Add(jobCandidate);
            await _context.SaveChangesAsync();
        }

        // JobCandidateService.cs

        public async Task<JobCandidate> GetJobCandidateAsync(int jobCandidateId)
        {
            return await _context.JobCandidate.FindAsync(jobCandidateId);
        }



    }
}
