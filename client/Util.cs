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

static public class Utils {

    static public GrpcChannel GrpcChannelCreate(string apiAddr, int apiPort) {
        return GrpcChannel.ForAddress("https://" + apiAddr + ":" + apiPort);
    }

    static public Metadata MetadataCreate(string key, string value) {
        var r = new Metadata();
        r.Add(key, value);
        return r;
    }

}