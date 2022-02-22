using AutoMapper;
using Employees.Api.Models.v1.Employee.Requests;
using Employees.Api.Models.v1.Employee.Responses;
using Employees.Application.Handlers.v1.Commands.Employees;
using Employees.Application.Handlers.v1.Queries.Employees;
using Employees.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EmployeesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Action to see all existing employees.
        /// </summary>
        /// <returns>Returns a page of existing employees</returns>
        /// <response code="200">Returned if the employees were loaded</response>
        /// <response code="400">Returned if the employees couldn't be loaded</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedList<EmployeeDto>>> List([FromQuery] ListEmployee request)
        {
            var (employees, count) = await _mediator.Send(_mapper.Map<GetEmployeesQuery>(request)).ConfigureAwait(false);

            PaginatedList<EmployeeDto> paginatedList = new(employees.Select(_mapper.Map<EmployeeDto>), count, request.Page, request.Limit);

            return Ok(paginatedList);
        }

        /// <summary>
        /// Action to get employee by id.
        /// </summary>
        /// <returns>Returns an employee by id</returns>
        /// <response code="200">Returned if the employee was fetched</response>
        /// <response code="404">Returned if the employees doesn't exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedList<Employee>>> GetById([FromRoute] int id)
        {
            Employee? found = await _mediator.Send(new GetEmployeeByIdQuery { Id = id }).ConfigureAwait(false);

            return Ok(_mapper.Map<EmployeeDto>(found));
        }

        /// <summary>
        /// Action to create an employee.
        /// </summary>
        /// <returns>Creates an employee</returns>
        /// <response code="201">Returned if the employee was created</response>
        /// <response code="400">Returned if the model couldn't be parsed</response>
        /// <response code="409">Returned if the employees exists already</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployee createEmployee)
        {
            Employee created = await _mediator.Send(new CreateEmployeeCommand
            {
                Employee = _mapper.Map<Employee>(createEmployee)
            }).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Action to update an employee.
        /// </summary>
        /// <returns>Updates an employee</returns>
        /// <response code="200">Returned if the employee was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed / couldn't be updated</response>
        /// <response code="404">Returned if the employees doesn't exist</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateEmployee updateEmployee)
        {
            await _mediator.Send(new UpdateEmployeeCommand
            {
                Employee = _mapper.Map<Employee>(updateEmployee)
            }).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        /// Action to delete an employee.
        /// </summary>
        /// <returns>Deletes an employee</returns>
        /// <response code="204">Returned if the employee was deleted</response>
        /// <response code="404">Returned if the employees doesn't exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            await _mediator.Send(new DeleteEmployeeCommand { Id = id }).ConfigureAwait(false);

            return NoContent();
        }
    }
}
