﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Interfaces;
using ToDoList.Models.Entities;
using ToDoList.Services.Infrastructure;
using ToDoList.Services.Extensions;


namespace ToDoList.Services.Handlers
{
    public class CustomAuthorizationHandler : AuthorizationHandler<AuthorizationRequirment>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ApplicationUserClaim> _userClaimRepo;
        private readonly IRepository<ApplicationUserRole> _userRoleRepo;


        public CustomAuthorizationHandler(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _userRoleRepo = _unitOfWork.GetRepository<ApplicationUserRole>();
            _userClaimRepo = _unitOfWork.GetRepository<ApplicationUserClaim>();

        }


        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirment requirement)
        {
            if (string.IsNullOrEmpty(context.User.Identity?.Name))
            {
                return Task.CompletedTask;
            }

            string? userId = context.User.GetUserId();
            context.Succeed(requirement);
            return Task.CompletedTask;
            //Endpoint? endpoint = _contextAccessor.HttpContext?.GetEndpoint();
            //string? endpointName = endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
            //string? routeClaim = endpointName;

            //IEnumerable<ApplicationUserRole> userRoles = await _userRoleRepo.GetQueryable().Include(x => x.Role)
            //    .ThenInclude(x => x.RoleClaims).Where(r => r.UserId == userId).ToListAsync();

            //IEnumerable<ApplicationUserClaim> userClaims = await _userClaimRepo.GetByAsync(r => r.UserId == userId);

            //bool userRoleHasClaim = userRoles.Any(x =>
            //    x.Role.Active && x.Role.RoleClaims.Any(r => r.Active && r.ClaimValue == routeClaim));

            //bool? userClaimHasClaim = userClaims.Any(x => x.ClaimValue == routeClaim);

            //if (userRoleHasClaim || userClaimHasClaim.GetValueOrDefault())
            //{
            //    context.Succeed(requirement);
            //}


            //return Task.CompletedTask;
        }
    }
}
