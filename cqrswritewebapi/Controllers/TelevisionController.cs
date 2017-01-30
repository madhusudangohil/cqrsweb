using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cqrswritewebapi.commands;
using cqrswritewebapi.Models;

namespace cqrswritewebapi.Controllers
{
    [RoutePrefix("api/Television")]
    public class TelevisionController : ApiController
    {
        [HttpPost]
        [Route("PowerOn")]
        public HttpResponseMessage PowerOn(TelevisionViewModel tvViewModel)
        {
            var commandHandler = new CommandHandler();
            commandHandler.Handle(new PowerOnTvCommand() { Id = tvViewModel.Id, Version=tvViewModel.Version});
            return Request.CreateResponse(HttpStatusCode.OK, "Request Submitted");
        }

        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage RegisterTelevision()
        {
            var commandHandler = new CommandHandler();
            commandHandler.Handle(new InitializeTvCommand() );
            return Request.CreateResponse(HttpStatusCode.OK, "Television Registration Request Submitted");
        }

        [HttpPost]
        [Route("PowerOff")]
        public HttpResponseMessage PowerOff(TelevisionViewModel tvViewModel)
        {
            var commandHandler = new CommandHandler();
            commandHandler.Handle(new PowerOffTvCommand() { Id = tvViewModel.Id, Version = tvViewModel.Version });
            return Request.CreateResponse(HttpStatusCode.OK, "Request Submitted");
        }

        [HttpPost]
        [Route("ChangeChannel")]
        public HttpResponseMessage ChangeChannel(TelevisionViewModel tvViewModel)
        {
            var commandHandler = new CommandHandler();
            commandHandler.Handle(new ChangeChannelCommand() { Id = tvViewModel.Id, Version = tvViewModel.Version, ChannelNumber = tvViewModel.Channel});
            return Request.CreateResponse(HttpStatusCode.OK, "Request Submitted");
        }
    }
}