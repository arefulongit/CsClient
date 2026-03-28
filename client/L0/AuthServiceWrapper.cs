using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using NLog;
using System.Threading.Tasks;

namespace CsClient;

public class AuthServiceWrapper {

    public static async Task<string> AuthAsync(GrpcChannel channel, string apiKey) {
        return await new AuthService.AuthServiceClient(channel).AuthAsync(
            new AuthRequest {
                Secret = apiKey
            }
        ) switch {
            var response => response.Token
        };
    }


    public static async Task<TokenDetailsResponse> TokenDetailsAsync(GrpcChannel channel, string token) {
        return await new AuthService.AuthServiceClient(channel).TokenDetailsAsync(
            new TokenDetailsRequest {
                Token = token
            }            
        );
    }

    public static IAsyncStreamReader<SubscribeJwtRenewalResponse> SubscribeJwtRenewal(GrpcChannel channel, string apiKey) {
        return new AuthService.AuthServiceClient(channel).SubscribeJwtRenewal(
            new SubscribeJwtRenewalRequest {
                Secret = apiKey
            }
        ) switch {
            var response => response.ResponseStream
        };
    }


}