using Google.Api;
using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using NLog;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsClient;

static public class Utils {

    public static GrpcChannel GrpcChannelCreate(string apiAddr, int apiPort) {
        return GrpcChannel.ForAddress("https://" + apiAddr + ":" + apiPort);
    }

    public static Metadata MetadataCreate(string key, string value) {
        var r = new Metadata {
            { key, value }
        };
        return r;
    }

    
    /// <summary>
    /// На основе двух дат создаёт интервал.
    /// Goole предусмотрительно сделал расширения для стандарнтых дат по конвертации их в 
    /// гугловый Timestamp
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static Interval CreateInterval(System.DateTime startDate, System.DateTime endDate) {
        var retInterval = new Interval {
            StartTime = startDate.ToUniversalTime().ToTimestamp(),
            EndTime = endDate.ToUniversalTime().ToTimestamp()
        };
        return retInterval;
    }

    public static System.DateTime GetQuarterStartDate(System.DateTime dateTime) {
        return(dateTime.Month) switch {
            > 0 and < 3 => new System.DateTime(dateTime.Year,1,1,0,0,0),
            > 3 and < 7 => new System.DateTime(dateTime.Year,4,1,0,0,0),
            > 7 and < 10 => new System.DateTime(dateTime.Year,7,1,0,0,0),
            > 10 and < 13 => new System.DateTime(dateTime.Year,10,1,0,0,0),
            _ => throw new Exception("Quartet number error!")
        };
    }
    public static System.DateTime GetQuarterEndDate(System.DateTime dateTime) {
        return(dateTime.Month) switch {
            > 0 and < 3 => new System.DateTime(dateTime.Year,3,1,0,0,0),
            > 3 and < 7 => new System.DateTime(dateTime.Year,6,1,0,0,0),
            > 7 and < 10 => new System.DateTime(dateTime.Year,9,1,0,0,0),
            > 10 and < 13 => new System.DateTime(dateTime.Year,12,1,0,0,0),
            _ => throw new Exception("Quartet number error!")
        };
    }

    public static double ToDouble(this string str) {
        return double.Parse(str.Replace('E','e'), NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture );
    }
}