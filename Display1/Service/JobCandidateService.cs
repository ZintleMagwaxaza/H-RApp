using Display1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
}
