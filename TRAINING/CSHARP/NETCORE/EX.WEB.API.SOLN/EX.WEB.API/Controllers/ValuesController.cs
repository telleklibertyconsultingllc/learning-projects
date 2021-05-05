using AutoMapper;
using DTOM = EX.DTO.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using EX.Repository.Interfaces;
using EX.WEB.API.Binders;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EX.WEB.API.Filters;

namespace EX.WEB.API.Controllers
{
    /// <summary>
    /// Values controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="mapper"></param>
        /// /// <param name="httpClientFactory"></param>
        public ValuesController(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory
        )
        {
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<DTOM.AuthorDto>> Get()
        {
            //var author = _customerRepository.GetAuthors();
            //if (author == null)
            //{
            //    return BadRequest();
            //}
            DTOM.AuthorDto[] model = new DTOM.AuthorDto[1]
            {
                new DTOM.AuthorDto()
                {
                    Age = 24,
                    Description = "Testing",
                    Gender = "F",
                    Id = Guid.NewGuid(),
                    MainCategory = "Something",
                    Name = "Test"
                }
            };
            return Ok(model);
        }

        /// <summary>
        /// Get id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("single/{id}", Name = "GetAuthorSingle")]
        [HttpHead]
        public ActionResult<string> Get(int id)
        {
            return $"{id} value";
        }

        /// <summary>
        /// GetIds
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("author/{ids}", Name = "GetAuthor")]
        [HttpHead("GetHead")]
        public ActionResult<string> GetIds(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids
        )
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var idsAsString = string.Join(",", ids.Select(a => a));
            return CreatedAtRoute("GetAuthors", new { ids = idsAsString }, ids);
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public ActionResult<DTOM.AuthorDto> Post([FromBody] DTOM.AuthorDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            var authorVM = _customerRepository.GetAuthors();
            return CreatedAtRoute("GetRoute", new { id = authorVM.Name }, authorVM);
        }

        /// <summary>
        /// Post Array
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost("create-again")]
        public ActionResult<DTOM.AuthorDto> PostArray([FromBody] IEnumerable<DTOM.AuthorDto> author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            var authorVM = _customerRepository.GetAuthors();
            return CreatedAtRoute("GetRoute", new { id = authorVM.Name }, authorVM);
        }

        /// <summary>
        /// PUT
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{authorId}/course/{id}")]
        public async Task<ActionResult> Put(int? authorId, int? id, [FromBody] string value)
        {
            if (authorId == null || id == null)
            {
                return BadRequest();
            }
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://service.com/{id}");
            if (response.IsSuccessStatusCode)
            {
                var serializedData = JsonSerializer.Deserialize<DTOM.AuthorDto>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
                return Ok(serializedData);
            }
            return Ok(value);
        }

        private async Task<AuthorExtension> GetAuthorsAsync(HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var authorEx = JsonSerializer.Deserialize<AuthorExtension>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return authorEx;
            }
            return null;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{authorId}/course/{id}")]
        [ValueTupleResultFilterAttribute()]
        public async Task<ActionResult> Delete(int? authorId, int? id)
        {
            if (authorId == null || id == null)
            {
                return NotFound();
            }
            // Call multiple calls
            var httpClient = httpClientFactory.CreateClient();
            var authorUrls = new[]
            {
                "http://services.com/1",
                "http://services.com/2",
                "http://services.com/3"
            };
            var authorExQuery =
                from url
                in authorUrls
                select GetAuthorsAsync(httpClient, url);
            var downloadedAuthorEx = authorExQuery.ToList();

            try
            {
                await Task.WhenAll(downloadedAuthorEx);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                /// Log the exception
            }
            catch (Exception ex)
            {
                /// Log ex
            }
            
            var authorResponses = new List<DTOM.AuthorDto>();
            foreach (var url in authorUrls)
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    authorResponses.Add(
                        JsonSerializer.Deserialize<DTOM.AuthorDto>(
                            await response.Content.ReadAsStringAsync(),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }));
                }
            }
            if (authorResponses.Count > 0)
            {
                (int? authorId, IEnumerable<DTOM.AuthorDto> authors) propertyBag = (authorId, authorResponses);
                return Ok((authorIdentifier: propertyBag.authorId, authors: propertyBag.authors));
            }
            return NoContent();
        }

        /// <summary>
        /// Patch
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{courseId}")]
        public ActionResult PatchAuthor(int? courseId, JsonPatchDocument<DTOM.AuthorDto> patchDocument)
        {
            if (courseId == null)
            {
                // Upserting through Patch
                var authorDto = new DTOM.AuthorDto();
                patchDocument.ApplyTo(authorDto, ModelState);
                if (!TryValidateModel(authorDto))
                {
                    return ValidationProblem(ModelState);
                }
                authorDto.Id = Guid.NewGuid();
                return CreatedAtRoute("GetCourseForAuthor", new {
                    courseId = authorDto.Id
                }, authorDto);
            }
            var author = new DTOM.AuthorDto();
            // Add validation just in case something happens
            // Apply patch document with the Model State to do validations
            patchDocument.ApplyTo(author, ModelState);
            if (!TryValidateModel(author))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map<Entities.Author>(author);
            // Save
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public ActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        /// <summary>
        /// This override allows the API to use the ModelBinder Validation 
        /// in the Startup class
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            // Using Dependency Injection,
            // Retrieve the ApiBehaviorOptions Service
            // That will be used to validate the ModelBinder
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
