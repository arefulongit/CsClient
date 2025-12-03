using Google.Api;
using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using Grpc.Tradeapi.V1.Marketdata;
using NLog;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsClient;

public class AccountsServiceWrapper {


    static public async Task<GetAccountResponse> GetAccountAsync(
        GrpcChannel channel,
        Metadata headers,
        string accountId
        ) {
        return await new AccountsService.AccountsServiceClient(channel).GetAccountAsync(
            new GetAccountRequest {
                AccountId = accountId
            },
            headers
        );
    }


    static public async Task<List<AccountTrade>> TradesAsync(
        GrpcChannel channel,
        Metadata headers,
        string accountId,
        Interval interval,
        int limit
        ) {
        return await new AccountsService.AccountsServiceClient(channel).TradesAsync(
            new TradesRequest {
                AccountId = accountId,
                Interval = interval,
                Limit = limit
            },
            headers
        ) switch {
            var response when response.Trades.Count > 0 => 
                response.Trades.ToList<AccountTrade>(),
            _ => new List<AccountTrade>()
        };
    }


    static public async Task<List<Transaction>> TransactionsAsync(
        GrpcChannel channel,
        Metadata headers,
        string accountId,
        Interval interval,
        int limit
        ) {
        return await new AccountsService.AccountsServiceClient(channel).TransactionsAsync(
            new TransactionsRequest {
                AccountId = accountId,
                Interval = interval,
                Limit = limit
            },
            headers
        ) switch {
            var response when response.Transactions.Count > 0 => 
                response.Transactions.ToList<Transaction>(),
            _ => new List<Transaction>()
        };
    }


}
