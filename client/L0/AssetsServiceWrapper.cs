using Google.Api;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using NLog;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsClient;

public class AssetsServiceWrapper {

    /// <summary>
    /// Получает от брокера список бирж. 
    /// </summary>
    /// <param name="channel">gprc-канал, полученный при подключении к брокера</param>
    /// <returns>
    /// Список бирж от брокера.Если от брокера приходит пустой список, то возвращает пустой список
    /// </returns>
    public static async Task<List<Exchange>> ExchangesAsync(GrpcChannel channel, Metadata headers) {
        return await new AssetsService.AssetsServiceClient(channel).ExchangesAsync(
            new ExchangesRequest { },
            headers
        ) switch {
            var response when response.Exchanges.Count > 0 =>
                response.Exchanges.ToList<Exchange>(),
            _ => new List<Exchange>()
        };
    }


    public static async Task<List<Asset>> AssetsAsync(GrpcChannel channel, Metadata headers) {
        return await new AssetsService.AssetsServiceClient(channel).AssetsAsync(
            new AssetsRequest { },
            headers
        ) switch {
            var response when response.Assets.Count > 0 =>
                response.Assets.ToList<Asset>(),
            _ => new List<Asset>()
        };
    }


    public static async Task<GetAssetResponse> GetAssetAsync(GrpcChannel channel, Metadata headers, string accountId, string symbol) {
        return await new AssetsService.AssetsServiceClient(channel).GetAssetAsync(
            new GetAssetRequest {
                AccountId = accountId,
                Symbol = symbol
            },
            headers
        );
    }


    public static async Task<GetAssetParamsResponse> GetAssetParamsAsync(GrpcChannel channel, Metadata headers, string accountId, string symbol) {
        return await new AssetsService.AssetsServiceClient(channel).GetAssetParamsAsync(
            new GetAssetParamsRequest {
                AccountId = accountId,
                Symbol = symbol
            },
            headers
        );
    }


    public static async Task<OptionsChainResponse> OptionsChainAsync(GrpcChannel channel, Metadata headers, string symbol) {
        return await new AssetsService.AssetsServiceClient(channel).OptionsChainAsync(
            new OptionsChainRequest {
                UnderlyingSymbol = symbol
            }
        );
    }


    public static async Task<ScheduleResponse> ScheduleAsync(GrpcChannel channel, Metadata headers, string symbol) {
        return await new AssetsService.AssetsServiceClient(channel).ScheduleAsync(
            new ScheduleRequest {
                Symbol = symbol
            },
            headers
        );
    }


    public static async Task<ClockResponse> ClockAsync(GrpcChannel channel, Metadata headers) {
        return await new AssetsService.AssetsServiceClient(channel).ClockAsync(
            new ClockRequest { }, headers
        );
    }

}