using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using Ws_BancoTabajara.Api.Exceptions;
using Ws_BancoTabajara.Api.Models;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Domain.Exceptions;

namespace Ws_BancoTabajara.Api.Controllers.Common
{
    public class ApiControllerBase : ApiController
    {
        protected IHttpActionResult HandleCallback<TSucess>(Func<TSucess> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception e)
            {
                return HandleFailure(e);
            }
        }

        protected IHttpActionResult HandleQuery<TResult>(IQueryable<TResult> query)
        {
            return Ok(query.ToList());
        }

        protected IHttpActionResult HandleQueryable<TSource>(IQueryable<TSource> query)
        {
            return Ok(query.ToList());
        }

        protected IHttpActionResult HandleQueryable1<TSource, TDestination>(IQueryable<TSource> query)
        {
            List<TSource> list = query.ToList();
            var newQuery = Mapper.Map<IList<TSource>, IList<TDestination>>(list);
            return Ok(newQuery.ToList());
        }

        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is BusinessException ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }

        protected IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return Content(HttpStatusCode.BadRequest, validationFailure);
        }
    }
}