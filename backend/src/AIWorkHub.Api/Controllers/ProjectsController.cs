using AIWorkHub.Application.Features.ProjectMembers.AddMember;
using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.Application.Features.ProjectMembers.GetMembers;
using AIWorkHub.Application.Features.Projects.Archive;
using AIWorkHub.Application.Features.Projects.Create;
using AIWorkHub.Application.Features.Projects.Delete;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.Application.Features.Projects.GetAll;
using AIWorkHub.Application.Features.Projects.GetById;
using AIWorkHub.Application.Features.Projects.Restore;
using AIWorkHub.Application.Features.Projects.ToggleFavorite;
using AIWorkHub.Application.Features.Projects.Update;
using AIWorkHub.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ProjectsController(IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateProjectCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Value?.Id },
            result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        string? search,
        ProjectStatus? status,
        ProjectPriority? priority,
        bool? favorite,
        bool? archived,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new GetAllProjectsQuery(
                search,
                status,
                priority,
                favorite,
                archived,
                page,
                pageSize),
            cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProjectByIdQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateProjectCommand(id, request),
            cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new DeleteProjectCommand(id),
            cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new ArchiveProjectCommand(id), ct);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpPatch("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new RestoreProjectCommand(id), ct);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    [HttpPatch("{id:guid}/favorite")]
    public async Task<IActionResult> Favorite(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new ToggleFavoriteProjectCommand(id), ct);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }

    #region Project Members
    [HttpPost("{projectId:guid}/members")]
    public async Task<IActionResult> AddMember(
        Guid projectId,
        AddProjectMemberRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new AddProjectMemberCommand(projectId, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpGet("{projectId:guid}/members")]
    public async Task<IActionResult> GetMembers(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetProjectMembersQuery(projectId),
            cancellationToken);

        return Ok(result);
    }
    #endregion

}
