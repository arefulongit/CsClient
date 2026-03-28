using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Tradeapi.V1.Accounts;
using Grpc.Tradeapi.V1.Assets;
using Grpc.Tradeapi.V1.Auth;
using Grpc.Tradeapi.V1.Marketdata;
using Grpc.Tradeapi.V1.Orders;
using NLog;
using System.Threading.Tasks;

namespace CsClient;

public class OrdersServiceWrapper {
    
    public static async Task<OrderState> BarsAsync (
        GrpcChannel channel, 
        Metadata headers, 
        Order order
        ) {
        return await new OrdersService.OrdersServiceClient(channel).PlaceOrderAsync(
            order,
            headers
        ) switch {
            var response => response
        };
    }

    public static async Task<OrderState> BarsAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string accountId,
        string orderId
        ) {
        return await new OrdersService.OrdersServiceClient(channel).CancelOrderAsync(
            new CancelOrderRequest {
                AccountId = accountId,
                OrderId = orderId
            },
            headers
        ) switch {
            var response => response
        };
    }

    public static async Task<List<OrderState>> GetOrdersAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string accountId
        ) {
        return await new OrdersService.OrdersServiceClient(channel).GetOrdersAsync(
            new OrdersRequest {
                AccountId = accountId
            },
            headers
        ) switch {
            var response when response.Orders.Count > 0 => response.Orders.ToList<OrderState>(),
            _ => new List<OrderState>()
        };
    }

    public static async Task<OrderState> GetOrderAsync (
        GrpcChannel channel, 
        Metadata headers, 
        string accountId,
        string orderId
        ) {
        return await new OrdersService.OrdersServiceClient(channel).GetOrderAsync(
            new GetOrderRequest {
                AccountId = accountId,
                OrderId = orderId
            },
            headers
        ) switch {
            var response => response
        };
    }


    [Obsolete("SubscribeOrderTrade is deprecated, please use SubscribeOrders, SubscribeOrders instead.")]
    public static IAsyncStreamReader<OrderTradeResponse> SubscribeOrderTrade (
        GrpcChannel channel,
        Metadata headers
    ) {
        return new OrdersService.OrdersServiceClient(channel).SubscribeOrderTrade(
            headers
        ) switch {
            var response => response.ResponseStream
        };
    }

    public static IAsyncStreamReader<SubscribeOrdersResponse> SubscribeOrders (
        GrpcChannel channel,
        Metadata headers,
        string accountId
    ) {
        return new OrdersService.OrdersServiceClient(channel).SubscribeOrders(
            new SubscribeOrdersRequest {
                AccountId = accountId
            }
        ) switch {
            var response => response.ResponseStream
        };
    }

    public static IAsyncStreamReader<SubscribeTradesResponse> SubscribeTrades (
        GrpcChannel channel,
        Metadata headers,
        string accountId
    ) {
        return new OrdersService.OrdersServiceClient(channel).SubscribeTrades(
            new SubscribeTradesRequest {
                AccountId = accountId
            }
        ) switch {
            var response => response.ResponseStream
        };
    }

}