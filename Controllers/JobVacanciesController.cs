namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {

        private readonly IJobvacancyRepository _repository;
        public JobVacanciesController(IJobvacancyRepository repository)
        {
            _repository = repository;
        }

        // GET api/job-vacancies
        /// <summary>
        /// Recupera todas as vagas de emprego
        /// </summary>
        /// <returns>Lista de vagas</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("GET JobVacancy chamado");

            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        // GET api/job-vacancies/{id}
        /// <summary>
        /// Recupera apenas uma vaga através do identificador
        /// </summary>
        /// <param name="id">Id da vaga.</param>
        /// <returns>Vaga</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Log.Information("GET JobVacancy chamado");

            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <remarks>
        ///{
        ///"title": "Titulo da vaga",
        ///"description": "Descrição da vaga",
        ///"company": "Empresa contratante",
        ///"isRemote": true,
        ///"salaryRange": "3000 - 5000"
        ///}
        /// </remarks>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyModel model)
        {
            Log.Information("POST JobVacancy chamado");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            if(jobVacancy.Title.Length > 30)
                return BadRequest("Título precisa ter menos de 30 caracteres");

            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById", 
                new { id = jobVacancy.Id },
                jobVacancy);
        }

        // PUT api/job-vacancies/{id}
        /// <summary>
        /// Atualiza uma vaga de acordo com o identificador
        /// </summary>
        /// <remarks>
        ///{
        ///"title": "Novo título da vaga",
        ///"description": "nova descrição da vaga",
        ///}
        /// </remarks>
        /// <param name="id">Identificador da vaga.</param>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Sem conteúdo.</response>
        /// <response code="404">Não encontrado.</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            Log.Information("PUT JobVacancy chamado");

            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            
            _repository.Update(jobVacancy);
            
            return NoContent();
        }
    }
}