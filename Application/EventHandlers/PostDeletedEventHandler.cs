using MediatR;
using my_app_backend.Application.QueryRepositories;
using my_app_backend.Domain.AggregateModel.BookAggregate.Events;
using my_app_backend.Models;
using Newtonsoft.Json;

namespace my_app_backend.Application.EventHandlers
{
    public class PostDeletedEventHandler : INotificationHandler<BookDeletedEvent>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<PostDeletedEventHandler> _logger;
        public PostDeletedEventHandler(IBookRepository bookRepository, ILogger<PostDeletedEventHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _bookRepository.Delete(notification.AggregateId);
            }
            catch (Exception ex)
            {
                _logger.Equals($"Exception happened: sync to read repository fail for BookCreatedEvent: {JsonConvert.SerializeObject(notification)}, ex: {ex}");
            }
        }
    }
}
