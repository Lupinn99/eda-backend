﻿using MediatR;
using my_app_backend.Application.Commands;
using my_app_backend.Domain.AggregateModel.BookAggregate;
using my_app_backend.Domain.SeedWork.Models;

namespace my_app_backend.Application.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<Guid>>
    {
        private readonly IBookEventStore _bookEventStore;
        public DeleteBookCommandHandler(IBookEventStore bookEventStore)
        {
            _bookEventStore = bookEventStore;
        }
        public async Task<Result<Guid>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aggregate = await _bookEventStore.Get(request.Id);
                aggregate.Delete(aggregate.Id);
                await _bookEventStore.Save(aggregate);
                return Result<Guid>.Ok(aggregate.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Error($"Exception happened: {ex}");
            }
        }
    }
}
