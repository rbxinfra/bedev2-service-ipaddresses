using Grpc.Core;

namespace Roblox.IpAddresses.Service;

using System;
using System.Threading.Tasks;

using Web.Framework.Services.Grpc;

using IpAddresses.V1;

using IpAddressesAPIGrpc = Roblox.IpAddresses.V1.IpAddressesAPI;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Default implementation for <see cref="IpAddressesAPIGrpc.IpAddressesAPIBase"/>
/// </summary>
#if DEBUG
[AllowAnonymous]
#endif
public class IpAddressesAPI : IpAddressesAPIGrpc.IpAddressesAPIBase
{
    private readonly IOperationExecutor _operationExecutor;
    private readonly IIpAddressesOperations _ipAddressesOperations;

    /// <summary>
    /// Construct a new instance of <see cref="IpAddressesAPI"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="ipAddressesOperations">The <see cref="IIpAddressesOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="ipAddressesOperations"/> cannot be null.
    /// </exception>
    public IpAddressesAPI(IOperationExecutor operationExecutor, IIpAddressesOperations ipAddressesOperations)
    {
        _operationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ipAddressesOperations = ipAddressesOperations ?? throw new ArgumentNullException(nameof(ipAddressesOperations));
    }

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserIdCountByIpAddress(V1.GetUserIdCountByIpAddressRequest, ServerCallContext)"/>
    public override Task<GetUserIdCountByIpAddressResponse> GetUserIdCountByIpAddress(GetUserIdCountByIpAddressRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserIdCountByIpAddress, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserIdCountByMacAddress(GetUserIdCountByMacAddressRequest, ServerCallContext)"/>
    public override Task<GetUserIdCountByMacAddressResponse> GetUserIdCountByMacAddress(GetUserIdCountByMacAddressRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserIdCountByMacAddress, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserIpAddressesByUserId(GetUserIpAddressesByUserIdRequest, ServerCallContext)"/>
    public override Task<GetUserIpAddressesByUserIdResponse> GetUserIpAddressesByUserId(GetUserIpAddressesByUserIdRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserIpAddressesByUserId, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserMacAddressesByUserId(GetUserMacAddressesByUserIdRequest, ServerCallContext)"/>
    public override Task<GetUserMacAddressesByUserIdResponse> GetUserMacAddressesByUserId(GetUserMacAddressesByUserIdRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserMacAddressesByUserId, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserMacAddressesByMacAddress(GetUserMacAddressesByMacAddressRequest, ServerCallContext)"/>
    public override Task<GetUserMacAddressesByMacAddressResponse> GetUserMacAddressesByMacAddress(GetUserMacAddressesByMacAddressRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserMacAddressesByMacAddress, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetUserIpAddressCountByUserId(GetUserIpAddressCountByUserIdRequest, ServerCallContext)"/>
    public override Task<GetUserIpAddressCountByUserIdResponse> GetUserIpAddressCountByUserId(GetUserIpAddressCountByUserIdRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetUserIpAddressCountByUserId, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetIpAddressStatus(GetIpAddressStatusRequest, ServerCallContext)"/>
    public override Task<GetIpAddressStatusResponse> GetIpAddressStatus(GetIpAddressStatusRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetIpAddressStatus, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetMacAddressCountByUserId(GetMacAddressCountByUserIdRequest, ServerCallContext)"/>
    public override Task<GetMacAddressCountByUserIdResponse> GetMacAddressCountByUserId(GetMacAddressCountByUserIdRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetMacAddressCountByUserId, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.GetMacAddressStatus(GetMacAddressStatusRequest, ServerCallContext)"/>
    public override Task<GetMacAddressStatusResponse> GetMacAddressStatus(GetMacAddressStatusRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.GetMacAddressStatus, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.SetUserIpAddress(SetUserIpAddressRequest, ServerCallContext)"/>
    public override Task<SetUserIpAddressResponse> SetUserIpAddress(SetUserIpAddressRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.SetUserIpAddress, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.SetUserMacAddress(SetUserMacAddressRequest, ServerCallContext)"/>
    public override Task<SetUserMacAddressResponse> SetUserMacAddress(SetUserMacAddressRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.SetUserMacAddress, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.SetMacAddressState(SetMacAddressStateRequest, ServerCallContext)"/>
    public override Task<SetMacAddressStateResponse> SetMacAddressState(SetMacAddressStateRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.SetMacAddressState, request));

    /// <inheritdoc cref="IpAddressesAPIGrpc.IpAddressesAPIBase.SetIpAddressState(SetIpAddressStateRequest, ServerCallContext)"/>
    public override Task<SetIpAddressStateResponse> SetIpAddressState(SetIpAddressStateRequest request, ServerCallContext context)
        => Task.FromResult(_operationExecutor.Execute(_ipAddressesOperations.SetIpAddressState, request));
}
