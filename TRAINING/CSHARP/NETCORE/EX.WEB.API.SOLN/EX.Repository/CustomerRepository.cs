using AutoMapper;
using EX.Repository.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DTOM = EX.DTO.Model;
using ENT = EX.Entities;

namespace EX.Repository
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly IMapper _mapper;
        private CancellationTokenSource cancellationTokenSource;
        private bool disposedValue;

        public CustomerRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DTOM.AuthorDto GetAuthors()
        {
            var model = new ENT.Author()
            {
                FirstName = "Tellek",
                LastName = "Liberty",
                DateOfBirth = new DateTimeOffset(DateTime.Now.AddYears(-45)),
                Gender = "M",
                MainCategory = "Social"
            };
            return _mapper.Map<DTOM.AuthorDto>(model);
        }

        private async Task GetAuthorAsync(HttpClient httpClient, CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync("", cancellationToken);
            //cancellationTokenSource.IsCancellationRequested
            //cancellationTokenSource.Token.ThrowIfCancellationRequested();

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CustomerRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
