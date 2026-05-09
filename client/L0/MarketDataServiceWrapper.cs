using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using Grpc.Tradeapi.V1.Marketdata;
using NLog;
using System.Threading.Tasks;

namespace CsClient;

public class MarketDataServiceWrapper {

    /// <summary>
    /// Получение свечей по инструменту <br />
    /// Запрос-Ответ
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="headers"></param>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="timeFrame"></param>
    /// <returns></returns>
    public static async Task<List<Bar>> BarsAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string symbol,
        Interval interval,
        TimeFrame timeFrame
        ) {
        return await new MarketDataService.MarketDataServiceClient(channel).BarsAsync(
            new BarsRequest {
                Interval = interval,
                Symbol = symbol,
                Timeframe = timeFrame
            },
            headers
        ) switch {
            var response when response.Bars.Count > 0 => response.Bars.ToList<Bar>(),
            _ => new List<Bar>()
        };
    }


    public static async Task<Quote> LastQuoteAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string symbol
        ) {
        return await new MarketDataService.MarketDataServiceClient(channel).LastQuoteAsync(
            new QuoteRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response => response.Quote
        };
    }

    public static async Task<OrderBook> OrderBookAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string symbol
        ) {
        return await new MarketDataService.MarketDataServiceClient(channel).OrderBookAsync(
            new OrderBookRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response => response.Orderbook
        };
    }

    public static async Task<List<Trade>> LatestTradesAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string symbol
        ) {
        return await new MarketDataService.MarketDataServiceClient(channel).LatestTradesAsync(
            new LatestTradesRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response when response.Trades.Count > 0 => response.Trades.ToList<Trade>(),
            _ => new List<Trade>()
        };
    }


    /// <summary>
    /// Получение свечей по инструменту <br />
    /// Запрос/Поток ответов
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="headers"></param>
    /// <param name="symbol"></param>
    /// <param name="timeFrame"></param>
    /// <returns></returns>
    public static async Task<IAsyncStreamReader<SubscribeBarsResponse>> SubscribeBars(
        GrpcChannel channel,
        Metadata headers,
        string symbol,
        TimeFrame timeFrame
        ) {
        return new MarketDataService.MarketDataServiceClient(channel).SubscribeBars(
            new SubscribeBarsRequest {
                Symbol = symbol,
                Timeframe = timeFrame
            },
            headers
        ) switch {
            var response => response.ResponseStream
        };
    }

    public static async Task<IAsyncStreamReader<SubscribeLatestTradesResponse>> SubscribeLatestTrades(
        GrpcChannel channel,
        Metadata headers,
        string symbol
        ) {
        return new MarketDataService.MarketDataServiceClient(channel).SubscribeLatestTrades(
            new SubscribeLatestTradesRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response => response.ResponseStream
        };
    }

    public static async Task<IAsyncStreamReader<SubscribeOrderBookResponse>> SubscribeOrderBook(
        GrpcChannel channel,
        Metadata headers,
        string symbol
        ) {
        return new MarketDataService.MarketDataServiceClient(channel).SubscribeOrderBook(
            new SubscribeOrderBookRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response => response.ResponseStream
        };
    }

    [Obsolete("Экспериментальный метод. Не использовать")]
    public static IAsyncEnumerable<SubscribeOrderBookResponse> SubscribeOrderBook1(
        GrpcChannel channel,
        Metadata headers,
        string symbol
        ) {
        return new MarketDataService.MarketDataServiceClient(channel).SubscribeOrderBook(
            new SubscribeOrderBookRequest {
                Symbol = symbol
            },
            headers
        ) switch {
            var response => response.ResponseStream.ReadAllAsync()
        };
    }

    public static async Task<IAsyncStreamReader<SubscribeQuoteResponse>> SubscribeQuote(
        GrpcChannel channel,
        Metadata headers,
        string[] symbols
        ) {
        return new MarketDataService.MarketDataServiceClient(channel).SubscribeQuote(
            new SubscribeQuoteRequest {
            },
            headers
        ) switch {
            var response => response.ResponseStream
        };
    }

}