using AIWorkHub.Application.Features.Search.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Search.GlobalSearch;

public sealed record SearchQuery(string Query)
    : IRequest<Result<SearchResponse>>;
