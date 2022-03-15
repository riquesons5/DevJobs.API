namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobvacancyRepository _repository;
        public JobApplicationsController(IJobvacancyRepository repository)
        {
            _repository = repository;
        }

        // POST api/job-vacancies/{id}/Applications
        /// <summary>
        /// Cadastrar um candidato para uma vaga específica
        /// </summary>
        /// <remarks>
        ///{
        ///"applicantName": "Nome do candidato",
        ///"applicantEmail": "E-mail do candidato",
        ///"idJobVacancy": 0,
        ///}
        /// </remarks>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Sem conteúdo.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            Log.Information("GET JobApplication chamado");

            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _repository.AddApplication(application);
            
            return NoContent();
        }
    }
}