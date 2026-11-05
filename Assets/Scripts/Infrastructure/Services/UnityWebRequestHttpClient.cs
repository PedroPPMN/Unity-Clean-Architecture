using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Portfolio.CleanArchitecture.Infrastructure.Services
{
    public sealed class UnityWebRequestHttpClient : ISimpleHttpClient
    {
        public async Task<string> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            using var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new InvalidOperationException($"HTTP GET failed ({request.responseCode}): {request.error}");
            }

            return request.downloadHandler.text;
        }
    }
}
