using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Models
{
    public record AddJobApplicationInputModel(string ApplicantName, string ApplicantEmail, int IdJobVacancy)
    {
    }
}