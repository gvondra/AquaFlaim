using AquaFlaim.CommonAPI;
using AquaFlaim.CommonCore;
using AquaFlaim.Forms.Framework;
using AquaFlaim.Interface.Forms.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormTypeController : FormsControllerBase
    {
        private readonly IFormTypeFactory _formTypeFactory;
        private readonly IFormTypeSaver _formTypeSaver;

        public FormTypeController(
            IOptions<Settings> settings,
            ISettingsFactory settingsFactory,
            AquaFlaim.Interface.Log.IMetricService metricService,
            AquaFlaim.Interface.Log.IExceptionService exceptionService,
            IFormTypeFactory formTypeFactory,
            IFormTypeSaver formTypeSaver
            )
            : base(settings, 
                  settingsFactory,
                  metricService,
                  exceptionService)
        { 
            _formTypeFactory = formTypeFactory;
            _formTypeSaver = formTypeSaver;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(FormType[]), 200)]
        [Authorize(Constants.POLICY_FORM_TYPE_READ)]
        public async Task<IActionResult> Search()
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                IEnumerable<IFormType> formTypes = await _formTypeFactory.GetAll(_settingsFactory.CreateCore(_settings.Value));
                IMapper mapper = MapperConfiguration.CreateMapper();
                result = Ok(
                    formTypes.Select<IFormType, FormType>(formType => mapper.Map<FormType>(formType))
                    );
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("get-form-type-all", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormType), 200)]
        [Authorize(Constants.POLICY_FORM_TYPE_READ)]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && !id.HasValue)
                    result = BadRequest("Missing id parameter value");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IFormType innerFormType = await _formTypeFactory.Get(settings, id.Value);
                    if (innerFormType == null)
                        result = NotFound();
                    else
                    {
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        result = Ok(await MapFormType(settings, mapper, innerFormType));
                    }
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("get-form-type", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [NonAction]
        private async Task<FormType> MapFormType(ISettings settings, IMapper mapper, IFormType innerFormType)
        {
            FormType formType = mapper.Map<FormType>(innerFormType);
            formType.Sections = new List<FormSectionType>();            
            foreach (IFormSectionType innerSection in await innerFormType.GetFormSections(settings))
            {
                FormSectionType formSectionType = mapper.Map<FormSectionType>(innerSection);
                formSectionType.Questions = new List<FormQuestionType>();
                formType.Sections.Add(formSectionType);
                foreach (IFormQuestionType innerQuestion in await innerSection.GetFormQuestionTypes(settings))
                {
                    formSectionType.Questions.Add(mapper.Map<FormQuestionType>(innerQuestion));
                }
            }
            return formType;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(FormType), 200)]
        [Authorize(Constants.POLICY_FORM_TYPE_READ)]
        public async Task<IActionResult> Create([FromBody] FormType formType)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && formType == null)
                    result = BadRequest("Missing form type data");
                if (result == null && string.IsNullOrEmpty(formType.Title))
                    result = BadRequest("Missing form title value");
                if (result == null && formType.Sections != null && formType.Sections.Where(s => s.Questions != null).SelectMany<FormSectionType, FormQuestionType>(s => s.Questions).Any(q => string.IsNullOrEmpty(q.Code)))
                    result = BadRequest("Missing question code value(s)");
                if (result == null)
                {
                    IFormType innerFormType = _formTypeFactory.Create();
                    IMapper mapper = MapperConfiguration.CreateMapper();
                    mapper.Map(formType, innerFormType);
                    if (formType.Sections != null)
                    {
                        foreach (FormSectionType sectionType in formType.Sections)
                        {
                            IFormSectionType innerSection = innerFormType.CreateSectionType();
                            mapper.Map(sectionType, innerSection);
                            innerFormType.AddSectionType(innerSection);
                            if (sectionType.Questions != null)
                            {
                                foreach (FormQuestionType questionType in sectionType.Questions)
                                {
                                    IFormQuestionType innerQuestion = innerSection.CreateQuestionType(questionType.Code);
                                    mapper.Map(questionType, innerQuestion);
                                    innerSection.AddQuestionType(innerQuestion);
                                }
                            }
                        }
                    }
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    await _formTypeSaver.Create(settings, innerFormType);
                    result = Ok(MapFormType(settings, mapper, innerFormType));
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("create-form-type", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormType), 200)]
        [Authorize(Constants.POLICY_FORM_TYPE_READ)]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromBody] FormType formType)
        {
            DateTime start = DateTime.UtcNow;
            IActionResult result = null;
            try
            {
                if (result == null && formType == null)
                    result = BadRequest("Missing form type data");
                if (result == null && !id.HasValue)
                    result = BadRequest("Missing id parameter value");
                if (result == null && string.IsNullOrEmpty(formType.Title))
                    result = BadRequest("Missing form title value");
                if (result == null && formType.Sections != null && formType.Sections.Where(s => s.Questions != null).SelectMany<FormSectionType, FormQuestionType>(s => s.Questions).Any(q => string.IsNullOrEmpty(q.Code)))
                    result = BadRequest("Missing question code value(s)");
                if (result == null)
                {
                    ISettings settings = _settingsFactory.CreateCore(_settings.Value);
                    IFormType innerFormType = await _formTypeFactory.Get(settings, id.Value);
                    if (innerFormType == null)
                        result = NotFound();
                    else
                    {
                        List<IFormSectionType> innerSections = (await innerFormType.GetFormSections(settings)).ToList();
                        List<IFormQuestionType> innerQuestions;
                        IMapper mapper = MapperConfiguration.CreateMapper();
                        mapper.Map(formType, innerFormType);
                        if (formType.Sections != null)
                        {
                            foreach (FormSectionType sectionType in formType.Sections)
                            {
                                IFormSectionType innerSection = null;
                                if (sectionType.FormSectionTypeId.HasValue)
                                    innerSection = innerSections.FirstOrDefault(s => s.FormSectionTypeId == sectionType.FormSectionTypeId.Value);                                    
                                if (innerSection == null)
                                {
                                    innerSection = innerFormType.CreateSectionType();
                                    innerFormType.AddSectionType(innerSection);
                                    innerQuestions = new List<IFormQuestionType>();
                                }
                                else
                                {
                                    innerQuestions = (await innerSection.GetFormQuestionTypes(settings)).ToList();
                                }
                                mapper.Map(sectionType, innerSection);                                
                                if (sectionType.Questions != null)
                                {
                                    foreach (FormQuestionType questionType in sectionType.Questions)
                                    {
                                        IFormQuestionType innerQuestion = null;
                                        if (questionType.FormQuestionTypeId.HasValue)
                                            innerQuestion = innerQuestions.FirstOrDefault(q => q.FormQuestionTypeId == questionType.FormQuestionTypeId.Value);
                                        if (innerQuestion == null)
                                        {
                                            innerQuestion = innerSection.CreateQuestionType(questionType.Code);
                                            innerSection.AddQuestionType(innerQuestion);
                                        }
                                        mapper.Map(questionType, innerQuestion);
                                    }
                                }
                            }
                        }
                        await _formTypeSaver.Create(settings, innerFormType);
                        result = Ok(MapFormType(settings, mapper, innerFormType));
                    }
                }
            }
            catch (Exception ex)
            {
                await WriteException(ex);
                result = StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _ = WriteMetrics("update-form-type", DateTime.UtcNow.Subtract(start).TotalSeconds);
            }
            return result;
        }
    }
}
