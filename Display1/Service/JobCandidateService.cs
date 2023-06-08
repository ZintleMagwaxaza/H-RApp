using Display1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Display1.Service
{
    public class JobCandidateService
    {
        private readonly AdventureWorks2019Context _context;
        private readonly NavigationManager _navigationManager;

        public JobCandidateService(AdventureWorks2019Context context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public List<JobCandidate> JobCandidates { get; private set; }

        public async Task<List<JobCandidate>> GetJobCandidatesAsync()
        {
            return await _context.JobCandidate.ToListAsync();
        }
        public async Task CreateJobCandidateAsync(JobCandidate jobCandidate)
        {
            _context.JobCandidate.Add(jobCandidate);
            await _context.SaveChangesAsync();
        }


        public async Task LoadJobCandidatesAsync()
        {
            await LoadJobCandidates();
            _navigationManager.NavigateTo("/jobcandidate");
        }

        public async Task<JobCandidate> GetJobCandidateAsync(int jobCandidateId)
        {
            return await _context.JobCandidate.FindAsync(jobCandidateId);
        }

        private async Task LoadJobCandidates()
        {

            JobCandidates = await GetJobCandidatesAsync();
        }

        public int GenerateJobCandidateId()
        {

            var newId = Guid.NewGuid();
            return Math.Abs(newId.GetHashCode());
        }



    }
}
