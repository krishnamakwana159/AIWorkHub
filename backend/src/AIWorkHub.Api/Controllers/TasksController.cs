using AIWorkHub.Application.Common.Models;
using AIWorkHub.Application.Features.Tasks.Assign;
using AIWorkHub.Application.Features.Tasks.Attachments.Delete;
using AIWorkHub.Application.Features.Tasks.Attachments.Download;
using AIWorkHub.Application.Features.Tasks.Attachments.GetAll;
using AIWorkHub.Application.Features.Tasks.Attachments.Upload;
using AIWorkHub.Application.Features.Tasks.Comments.CreateComment;
using AIWorkHub.Application.Features.Tasks.Comments.DeleteComment;
using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Features.Tasks.Comments.GetAllComment;
using AIWorkHub.Application.Features.Tasks.Comments.UpdateComment;
using AIWorkHub.Application.Features.Tasks.CreateTask;
using AIWorkHub.Application.Features.Tasks.DeleteTask;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Features.Tasks.GetAllTasks;
using AIWorkHub.Application.Features.Tasks.GetTaskById;
using AIWorkHub.Application.Features.Tasks.UpdateStatus;
using AIWorkHub.Application.Features.Tasks.UpdateTask;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TasksController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new CreateTaskCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTasksQuery(projectId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTaskByIdQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateTaskCommand(id, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteTaskCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid id,
        AssignTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new AssignTaskCommand(id, request.UserId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateTaskStatusRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateTaskStatusCommand(id, request.Status),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    #region TaskComment

    [HttpPost("{taskId:guid}/comments")]
    public async Task<IActionResult> AddComment(
        Guid taskId,
        CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new CreateCommentCommand(taskId, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{taskId:guid}/comments")]
    public async Task<IActionResult> GetComments(
        Guid taskId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetCommentsQuery(taskId),
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("comments/{commentId:guid}")]
    public async Task<IActionResult> UpdateComment(
        Guid commentId,
        UpdateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateCommentCommand(commentId, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("comments/{commentId:guid}")]
    public async Task<IActionResult> DeleteComment(
        Guid commentId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteCommentCommand(commentId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
    #endregion

    #region TaskAttachment

    [HttpPost("{taskId:guid}/attachments")]
    public async Task<IActionResult> UploadAttachment(
        Guid taskId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var dto = new FileUploadDto
        {
            Content = stream,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Length = file.Length
        };

        var result = await sender.Send(
            new UploadAttachmentCommand(taskId, dto),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("{taskId:guid}/attachments")]
    public async Task<IActionResult> GetAttachments(
        Guid taskId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTaskAttachmentsQuery(taskId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("attachments/{attachmentId:guid}/download")]
    public async Task<IActionResult> DownloadAttachment(
        Guid attachmentId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DownloadAttachmentQuery(attachmentId),
            cancellationToken);

        if (result.IsFailure || result.Value?.Stream == null)
            return NotFound(result);

        var stream = result.Value.Stream;

        return File(
            stream,
            result.Value.ContentType,
            result.Value.FileName);
    }

    [HttpDelete("attachments/{attachmentId:guid}")]
    public async Task<IActionResult> DeleteAttachment(
        Guid attachmentId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteAttachmentCommand(attachmentId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    } 
    #endregion

}
